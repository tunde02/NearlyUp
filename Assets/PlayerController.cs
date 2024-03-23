using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float movePower = 75f;
    public float jumpPower = 3000f;
    public float velocityLimit = 20f;
    public Transform cameraArm;


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
        // ���� �ӵ��� ���� �÷��̾��� ���� �ӵ� ������ movePower�� ���Ͽ�
        // ���� �ӵ��� �������� �̵��� �� �ִ� ���� �۾������� ����
        float power = movePower * GetVelocityRatio();
        
        // ī�޶� ȸ���ص� ��, ��, ����, ������ ������ �����ϵ���
        // ī�޶��� ȸ������ ���� ���⿡ ������
        Vector3 cameraForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 cameraRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
        
        Vector3 force = cameraRight * moveAmount.x + cameraForward * moveAmount.y;
        Vector3 torque = cameraRight * moveAmount.y - cameraForward * moveAmount.x;
        force.y = -0.05f;

        rb.AddForce(force * power);
        rb.AddTorque(torque * power);
    }

    /// <summary>
    /// ���� �ӵ��� ���� �÷��̾��� ���� �ӵ� ������ ��ȯ�ϴ� �Լ�
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
