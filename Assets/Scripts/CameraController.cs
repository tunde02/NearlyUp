using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    public Transform realCamera;
    public Transform followCamera;
    public LayerMask playerLayerMask;


    private readonly float cameraAngleDeadzone = 10f;
    private readonly float minCameraDistance = 3f;
    public float MaxCameraDistance { get; set; } = 14f;
    public float Sensitivity { get; set; } = 15f;
    private readonly float smoothness = 100f;

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
        float rotationX = cameraAngle.x - mouseAxisY * Sensitivity * Time.deltaTime;
        float rotationY = cameraAngle.y + mouseAxisX * Sensitivity * Time.deltaTime;

        // 카메라가 뒤집어지지 않도록 회전 각도 제한
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
        Vector3 realCameraWorldPosition = transform.TransformPoint(realCameraNormal * MaxCameraDistance);
        float cameraDistance = MaxCameraDistance;

        // 플레이어와 카메라 사이에 장애물이 있다면, Linecast를 통해
        // 플레이어가 보이는 최소 거리를 계산하여 카메라의 위치를 변경
        if (Physics.Linecast(transform.position, realCameraWorldPosition, out RaycastHit hit, ~playerLayerMask))
        {
            cameraDistance = Mathf.Clamp(hit.distance, minCameraDistance, MaxCameraDistance);
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
