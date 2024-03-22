using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float movePower = 35f;
    public float jumpPower = 2500f;
    public float velocityLimit = 5f;


    private Rigidbody rb;
    private Vector2 moveAmount;
    private float sqrVelocityLimitInverse;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sqrVelocityLimitInverse = 1 / (velocityLimit * velocityLimit);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // 제한 속도에 대한 플레이어의 현재 속도 비율을 movePower에 곱하여
        // 현재 속도가 빠를수록 이동할 때 주는 힘이 작아지도록 설정
        float power = movePower * GetVelocityRatio();

        rb.AddForce(new Vector3(moveAmount.x, -0.05f, moveAmount.y) * power);
        rb.AddTorque(new Vector3(moveAmount.y, 0, -moveAmount.x) * power);
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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveAmount = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.AddForce(Vector3.up * jumpPower);
        }
    }
}
