using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    public Transform realCamera;
    public Transform followCamera;
    public LayerMask playerLayerMask;
    public float cameraAngleDeadzone = 10f;
    public float minCameraDistance = 3f;
    public float maxCameraDistance = 15f;
    public float sensitivity = 10f;
    public float smoothness = 100f;


    private float mouseAxisX;
    private float mouseAxisY;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 cameraAngle = transform.localRotation.eulerAngles;
        float rotationX = cameraAngle.x - mouseAxisY * sensitivity * Time.deltaTime;
        float rotationY = cameraAngle.y + mouseAxisX * sensitivity * Time.deltaTime;

        // ī�޶� ���������� �ʵ��� ȸ�� ���� ����
        if (-cameraAngleDeadzone < rotationX - 90f && rotationX - 90f < 0f)
        {
            rotationX = 90f - cameraAngleDeadzone;
        }
        else if (0f < rotationX - 90f && rotationX - 90f < cameraAngleDeadzone)
        {
            rotationX = 90f + cameraAngleDeadzone;
        }
        else if (-cameraAngleDeadzone < rotationX - 270f && rotationX - 270f < 0f)
        {
            rotationX = 270f - cameraAngleDeadzone;
        }
        else if (0f < rotationX - 270f && rotationX - 270f < cameraAngleDeadzone)
        {
            rotationX = 270f + cameraAngleDeadzone;
        }

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }

    private void LateUpdate()
    {
        Vector3 realCameraNormal = realCamera.localPosition.normalized;
        Vector3 realCameraWorldPosition = transform.TransformPoint(realCameraNormal * maxCameraDistance);
        float cameraDistance = maxCameraDistance;

        // �÷��̾�� ī�޶� ���̿� ��ֹ��� �ִٸ�, Linecast�� ����
        // �÷��̾ ���̴� �ּ� �Ÿ��� ����Ͽ� ī�޶��� ��ġ�� ����
        if (Physics.Linecast(transform.position, realCameraWorldPosition, out RaycastHit hit, ~playerLayerMask))
        {
            cameraDistance = Mathf.Clamp(hit.distance, minCameraDistance, maxCameraDistance);
            Debug.DrawLine(followCamera.position, hit.point, Color.red);
        }

        transform.position = Vector3.MoveTowards(transform.position, followCamera.position, smoothness * Time.deltaTime);
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, realCameraNormal * cameraDistance, smoothness * Time.deltaTime);
    }

    public void OnMouseAxisX(InputAction.CallbackContext context)
    {
        mouseAxisX = context.ReadValue<float>();
    }

    public void OnMouseAxisY(InputAction.CallbackContext context)
    {
        mouseAxisY = context.ReadValue<float>();
    }
}
