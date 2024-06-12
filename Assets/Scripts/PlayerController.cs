using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static event EventHandler<OnInventoryChangedArgs> OnInventoryChanged;
    public class OnInventoryChangedArgs : EventArgs
    {
        public BaseItem[] inventory;
    }


    public Rigidbody rb;
    public Transform cameraArm;
    public MeshRenderer meshRenderer;


    // Player Input Action
    private PlayerInput playerInput;
    private readonly string gamePlayActionMap = "GamePlay";
    private readonly string menuActionMap = "Menu";

    // Movement
    public float MovePower { get; set; } = 20f;
    public float Mass
    {
        get => rb.mass;
        set => rb.mass = value;
    }
    public float Drag
    {
        get => rb.drag;
        set => rb.drag = value;
    }
    private float _velocityLimit = 25f;
    public float VelocityLimit
    {
        get => _velocityLimit;
        set
        {
            _velocityLimit = value;
            sqrVelocityLimitInverse = 1 / (VelocityLimit * VelocityLimit);
        }
    }
    private float sqrVelocityLimitInverse;
    private Vector2 moveAmount;

    // Jump
    public float JumpPower { get; set; } = 30f;
    private int contactCount = 0;
    private bool onGround = false;
    private readonly float contactPointHeightLimit = 0.2f;
    private bool isJumpKeyPressed = false;

    // Slow Motion
    private bool inSlowMotion = false;
    public float slowMotionGauge = 10f;
    private readonly float maxSlowMotionGauge = 10f;
    private float gaugeRecoveryAmount = 1.0f;
    private float gaugeConsumptionAmount = 2.0f;

    // Item
    public BaseItem[] Inventory { get; private set; } = { null, null, null };
    [SerializeField] private int selectedItemIndex = 0;
    public float Money { get; set; } = 0f;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions.FindActionMap(gamePlayActionMap).FindAction("Open Menu").performed += ctx => { UIManager.Instance.OpenMenu(); };
        playerInput.actions.FindActionMap(menuActionMap).FindAction("Close Menu").performed += ctx => { UIManager.Instance.CloseMenu(); };
    }

    private void Start()
    {
        sqrVelocityLimitInverse = 1 / (VelocityLimit * VelocityLimit);
    }

    private void Update()
    {
        UpdateSlowMotionGauge();

        DebugCanJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        // 제한 속도에 대한 플레이어의 현재 속도 비율을 movePower에 곱하여
        // 현재 속도가 빠를수록 이동할 때 주는 힘이 작아지도록 설정
        float power = MovePower * GetVelocityRatio();

        // 카메라가 회전해도 앞, 뒤, 왼쪽, 오른쪽 조작이 동일하도록
        // 카메라의 회전값을 힘의 방향에 더해줌
        Vector3 cameraForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 cameraRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

        Vector3 force = cameraRight * moveAmount.x + cameraForward * moveAmount.y;
        Vector3 torque = cameraRight * moveAmount.y - cameraForward * moveAmount.x;
        // force.y = -0.2f;

        if (inSlowMotion)
        {
            force *= 2f;
            torque *= 2f;
        }

        rb.AddForce(force * power);
        rb.AddTorque(torque * power);
    }

    private void HandleJump()
    {
        if (isJumpKeyPressed && CanJump())
        {
            // 힘이 가해지기 전에 OnCollisionStay()가 한 번 더 실행되어
            // 점프가 연속으로 시전되는 것을 막기위해 미리 플레이어를 살짝 띄움
            transform.position = new(transform.position.x, transform.position.y + 0.1f, transform.position.z);

            rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            onGround = false;
        }
    }

    /// <summary>
    /// 제한 속도에 대한 플레이어의 현재 속도 비율을 반환하는 함수
    /// </summary>
    private float GetVelocityRatio()
    {
        float velocityRatio = 1f - rb.velocity.sqrMagnitude * sqrVelocityLimitInverse;

        if (velocityRatio > 0f)
        {
            return velocityRatio;
        }
        else
        {
            return 0.1f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        ++contactCount;

        SoundManager.Instance.PlayCollisionEffect();
    }

    private void OnCollisionStay(Collision other)
    {
        // 중심보다 낮은 접촉 지점이 있는지 체크
        for (int i = 0; i < other.contactCount; ++i)
        {
            if (other.GetContact(i).point.y + contactPointHeightLimit <= transform.position.y)
            {
                onGround = true;
                break;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        --contactCount;
        onGround = false;
    }

    private bool CanJump()
    {
        return contactCount > 0 && onGround;
    }

    private void DebugCanJump()
    {
        if (CanJump())
        {
            meshRenderer.material.color = Color.green;
        }
        else
        {
            meshRenderer.material.color = Color.red;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJumpKeyPressed = true;
        }
        else if (context.canceled)
        {
            isJumpKeyPressed = false;
        }
    }

    public void OnSlowMotion(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inSlowMotion = true;

            GameManager.Instance.SlowGame();
        }
        else if (context.canceled)
        {
            inSlowMotion = false;

            if (!GameManager.Instance.IsGamePaused)
            {
                GameManager.Instance.ResumeGame();
            }
        }
    }

    private void UpdateSlowMotionGauge()
    {
        if (inSlowMotion)
        {
            ConsumeSlowMotionGauge();

            if (slowMotionGauge <= 0f)
            {
                inSlowMotion = false;
                GameManager.Instance.ResumeGame();
            }
        }
        else
        {
            RecoverSlowMotionGauge();
        }
    }

    private void RecoverSlowMotionGauge()
    {
        if (slowMotionGauge < maxSlowMotionGauge)
        {
            slowMotionGauge += Time.deltaTime * gaugeRecoveryAmount;
        }
        else if (slowMotionGauge > maxSlowMotionGauge)
        {
            slowMotionGauge = maxSlowMotionGauge;
        }
    }

    private void ConsumeSlowMotionGauge()
    {
        if (slowMotionGauge > 0f)
        {
            slowMotionGauge -= Time.deltaTime * 2.0f * gaugeConsumptionAmount;
        }
        else if (slowMotionGauge < 0f)
        {
            slowMotionGauge = 0f;
        }
    }

    public void OnUseItem_1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UseItemAt(0);
        }
    }

    public void OnUseItem_2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UseItemAt(1);
        }
    }

    public void OnUseItem_3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UseItemAt(2);
        }
    }

    private void UseItemAt(int index)
    {
        if (Inventory[index] == null)
        {
            Debug.Log($"PlayerController.UseItemAt() : No item at inventory {index}");
            return;
        }

        Debug.Log($"PlayerController.UseItemAt({index}) : Use {Inventory[index].ItemName})");

        Inventory[index].UseItem(this);
        Inventory[index] = null;

        OnInventoryChanged?.Invoke(this, new OnInventoryChangedArgs { inventory = Inventory });
    }

    public void BuyItem(BaseItem item)
    {
        int emptyInventoryIndex = GetEmptyInventoryIndex();

        if (emptyInventoryIndex == -1)
        {
            Debug.Log("Can't buy item");
            return;
        }

        Debug.Log($"PlayerController.BuyItem({item.ItemName}) at {emptyInventoryIndex}");

        Inventory[emptyInventoryIndex] = item;

        OnInventoryChanged?.Invoke(this, new OnInventoryChangedArgs { inventory = Inventory });
    }

    private int GetEmptyInventoryIndex()
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            int idx = (selectedItemIndex + i) % Inventory.Length;

            if (Inventory[idx] == null)
            {
                return idx;
            }
        }

        // Inventory Full
        return -1;
    }

    public void SwitchToGamePlayActionMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerInput.SwitchCurrentActionMap(gamePlayActionMap);
        }
    }

    public void SwitchToGamePlayActionMap()
    {
        playerInput.SwitchCurrentActionMap(gamePlayActionMap);
    }

    public void SwitchToMenuActionMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerInput.SwitchCurrentActionMap(menuActionMap);
        }
    }

    public void SwitchToMenuActionMap()
    {
        playerInput.SwitchCurrentActionMap(menuActionMap);
    }

    public void EnableGamePlayInputActionMap()
    {
        playerInput.actions.FindActionMap(gamePlayActionMap).Enable();
    }

    public void DisableGamePlayInputActionMap()
    {
        playerInput.actions.FindActionMap(gamePlayActionMap).Disable();
    }
}
