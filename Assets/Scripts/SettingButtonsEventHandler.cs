using UnityEngine;
using UnityEngine.EventSystems;


public class SettingButtonsEventHandler : MonoBehaviour
{
    [SerializeField] private SettingSlider movePowerSlider;
    [SerializeField] private SettingSlider jumpPowerSlider;
    [SerializeField] private SettingSlider massSlider;
    [SerializeField] private SettingSlider gravitySlider;
    [SerializeField] private SettingSlider dragSlider;
    [SerializeField] private SettingSlider cameraDistanceSlider;
    [SerializeField] private SettingSlider cameraSensitivitySlider;
    [SerializeField] private SettingSlider BouncinessSlider;
    [SerializeField] private SettingSlider velocityLimitSlider;
    [SerializeField] private GameObject applyButton;
    [SerializeField] private GameObject cancelButton;


    private void Start()
    {
        UIManager.Instance.AddButtonListener(
            applyButton,
            (data) => { OnApplyButtonClick((PointerEventData)data); }
        );
        UIManager.Instance.AddButtonListener(
            cancelButton,
            (data) => { OnCancelButtonClick((PointerEventData)data); }
        );
    }

    private void OnEnable()
    {
        ResetSettingValues();
    }

    private void OnApplyButtonClick(PointerEventData data)
    {
        GameManager.Instance.SetPlayerMovePower(movePowerSlider.GetSettingValue());
        GameManager.Instance.SetPlayerJumpPower(jumpPowerSlider.GetSettingValue());
        GameManager.Instance.SetPlayerMass(massSlider.GetSettingValue());
        GameManager.Instance.SetGravity(gravitySlider.GetSettingValue());
        GameManager.Instance.SetPlayerDrag(dragSlider.GetSettingValue());
        GameManager.Instance.SetMaxCameraDistance(cameraDistanceSlider.GetSettingValue());
        GameManager.Instance.SetCameraSensitivity(cameraSensitivitySlider.GetSettingValue());
        GameManager.Instance.SetPlayerBounciness(BouncinessSlider.GetSettingValue());
        GameManager.Instance.SetPlayerVelocityLimit(velocityLimitSlider.GetSettingValue());
    }

    private void OnCancelButtonClick(PointerEventData data)
    {
        ResetSettingValues();
    }

    private void ResetSettingValues()
    {
        movePowerSlider.SetSettingValue(GameManager.Instance.GetPlayerMovePower());
        jumpPowerSlider.SetSettingValue(GameManager.Instance.GetPlayerJumpPower());
        massSlider.SetSettingValue(GameManager.Instance.GetPlayerMass());
        gravitySlider.SetSettingValue(GameManager.Instance.GetGravity());
        dragSlider.SetSettingValue(GameManager.Instance.GetPlayerDrag());
        cameraDistanceSlider.SetSettingValue(GameManager.Instance.GetMaxCameraDistance());
        cameraSensitivitySlider.SetSettingValue(GameManager.Instance.GetCameraSensitivity());
        BouncinessSlider.SetSettingValue(GameManager.Instance.GetPlayerBounciness());
        velocityLimitSlider.SetSettingValue(GameManager.Instance.GetPlayerVelocityLimit());
    }
}
