using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float movePower = 75f;
    public float jumpPower = 3000f;
    public float velocityLimit = 20f;
    public Transform cameraArm;

    public MeshRenderer meshRenderer;


    // Movement
    private Rigidbody rb;
    private Vector2 moveAmount;
    private float sqrVelocityLimitInverse;

    // Jump
    private int contactCount = 0;
    private bool onGround = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sqrVelocityLimitInverse = 1 / (velocityLimit * velocityLimit);
    }

    private void Update()
    {
        DebugCanJump();
    }

    private void FixedUpdate()
    {
        // 제한 속도에 대한 플레이어의 현재 속도 비율을 movePower에 곱하여
        // 현재 속도가 빠를수록 이동할 때 주는 힘이 작아지도록 설정
        float power = movePower * GetVelocityRatio();

        // 카메라가 회전해도 앞, 뒤, 왼쪽, 오른쪽 조작이 동일하도록
        // 카메라의 회전값을 힘의 방향에 더해줌
        Vector3 cameraForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 cameraRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

        Vector3 force = cameraRight * moveAmount.x + cameraForward * moveAmount.y;
        Vector3 torque = cameraRight * moveAmount.y - cameraForward * moveAmount.x;
        force.y = -0.2f;

        rb.AddForce(force * power);
        rb.AddTorque(torque * power);
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
            return 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ++contactCount;
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);

        // 중심보다 낮은 접촉 지점이 하나라도 있는지 체크
        onGround = false;
        for (int i = 0; i < contacts.Length; ++i)
        {
            if (contacts[i].point.y + 0.2f <= transform.position.y)
            {
                onGround = true;
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        --contactCount;
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
        if (context.performed && CanJump())
        {
            rb.AddForce(Vector3.up * jumpPower);
        }
    }
}
