using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{
    public UIDocument menuUIDocument;


    private VisualElement root;
    private Dictionary<string, (float initialValue, Slider slider, FloatField field)> parameters;


    private void Start()
    {
        root = menuUIDocument.rootVisualElement;

        parameters = new()
        {
            {
                "movePower",
                (GameManager.Instance.GetPlayerMovePower(), root.Q<Slider>("movePowerSlider"), root.Q<FloatField>("movePowerField"))
            },
            {
                "jumpPower",
                (GameManager.Instance.GetPlayerJumpPower(), root.Q<Slider>("jumpPowerSlider"), root.Q<FloatField>("jumpPowerField"))
            },
            {
                "mass",
                (GameManager.Instance.GetPlayerMass(), root.Q<Slider>("massSlider"), root.Q<FloatField>("massField"))
            },
            {
                "velocityLimit",
                (GameManager.Instance.GetPlayerVelocityLimit(), root.Q<Slider>("velocityLimitSlider"), root.Q<FloatField>("velocityLimitField"))
            },
            {
                "maxCameraDistance",
                (GameManager.Instance.GetMaxCameraDistance(), root.Q<Slider>("maxCameraDistanceSlider"), root.Q<FloatField>("maxCameraDistanceField"))
            },
            {
                "sensitivity",
                (GameManager.Instance.GetSensitivity(), root.Q<Slider>("sensitivitySlider"), root.Q<FloatField>("sensitivityField"))
            },
            {
                "dynamicFriction",
                (GameManager.Instance.GetDynamicFriction(), root.Q<Slider>("dynamicFrictionSlider"), root.Q<FloatField>("dynamicFrictionField"))
            },
            {
                "staticFriction",
                (GameManager.Instance.GetStaticFriction(), root.Q<Slider>("staticFrictionSlider"), root.Q<FloatField>("staticFrictionField"))
            },
            {
                "bounciness",
                (GameManager.Instance.GetBounciness(), root.Q<Slider>("bouncinessSlider"), root.Q<FloatField>("bouncinessField"))
            },
            {
                "gravity",
                (GameManager.Instance.GetGravity(), root.Q<Slider>("gravitySlider"), root.Q<FloatField>("gravityField"))
            },
        };

        parameters["movePower"].slider.RegisterValueChangedCallback(OnMovePowerSliderChanged);
        parameters["jumpPower"].slider.RegisterValueChangedCallback(OnJumpPowerSliderChanged);
        parameters["mass"].slider.RegisterValueChangedCallback(OnMassSliderChanged);
        parameters["velocityLimit"].slider.RegisterValueChangedCallback(OnVelocityLimitSliderChanged);
        parameters["maxCameraDistance"].slider.RegisterValueChangedCallback(OnMaxCameraDistanceliderChanged);
        parameters["sensitivity"].slider.RegisterValueChangedCallback(OnSensitivitySliderChanged);
        parameters["dynamicFriction"].slider.RegisterValueChangedCallback(OnDynamicFrictionSliderChanged);
        parameters["staticFriction"].slider.RegisterValueChangedCallback(OnStaticFrictionSliderChanged);
        parameters["bounciness"].slider.RegisterValueChangedCallback(OnBouncinessSliderChanged);
        parameters["gravity"].slider.RegisterValueChangedCallback(OnGravitySliderChanged);

        parameters["movePower"].slider.value = GameManager.Instance.GetPlayerMovePower();
        parameters["jumpPower"].slider.value = GameManager.Instance.GetPlayerJumpPower();
        parameters["mass"].slider.value = GameManager.Instance.GetPlayerMass();
        parameters["velocityLimit"].slider.value = GameManager.Instance.GetPlayerVelocityLimit();
        parameters["maxCameraDistance"].slider.value = GameManager.Instance.GetMaxCameraDistance();
        parameters["sensitivity"].slider.value = GameManager.Instance.GetSensitivity();
        parameters["dynamicFriction"].slider.value = GameManager.Instance.GetDynamicFriction();
        parameters["staticFriction"].slider.value = GameManager.Instance.GetStaticFriction();
        parameters["bounciness"].slider.value = GameManager.Instance.GetBounciness();
        parameters["gravity"].slider.value = GameManager.Instance.GetGravity();

        root.Q<Button>("resetButton").RegisterCallback<ClickEvent>(OnResetButtonClicked);
        root.Q<Button>("reloadButton").RegisterCallback<ClickEvent>(OnReloadButtonClicked);
        root.Q<Button>("quitButton").RegisterCallback<ClickEvent>(OnQuitButtonClicked);

        root.visible = false;
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            root.visible = true;

            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;

            GameManager.Instance.PauseGame();
        }
    }

    public void CloseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            root.visible = false;

            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;

            GameManager.Instance.ResumeGame();
        }
    }

    public void OnResetButtonClicked(ClickEvent evt)
    {
        ResetParameters();
    }

    public void OnReloadButtonClicked(ClickEvent evt)
    {
        ResetParameters();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButtonClicked(ClickEvent evt)
    {
        // GameManager.Instance.QuitGame();
        SceneManager.LoadScene("Level List");
    }

    private void ResetParameters()
    {
        parameters["movePower"].slider.value = parameters["movePower"].initialValue;
        parameters["jumpPower"].slider.value = parameters["jumpPower"].initialValue;
        parameters["mass"].slider.value = parameters["mass"].initialValue;
        parameters["velocityLimit"].slider.value = parameters["velocityLimit"].initialValue;
        parameters["maxCameraDistance"].slider.value = parameters["maxCameraDistance"].initialValue;
        parameters["sensitivity"].slider.value = parameters["sensitivity"].initialValue;
        parameters["dynamicFriction"].slider.value = parameters["dynamicFriction"].initialValue;
        parameters["staticFriction"].slider.value = parameters["staticFriction"].initialValue;
        parameters["bounciness"].slider.value = parameters["bounciness"].initialValue;
        parameters["gravity"].slider.value = parameters["gravity"].initialValue;
    }

    private void OnApplicationQuit()
    {
        // Physic Material은 게임 진행 중 변경된 값이 그대로 저장되기 때문에
        // 게임 시작 전 저장해둔 값으로 복구
        GameManager.Instance.SetDynamicFriction(parameters["dynamicFriction"].initialValue);
        GameManager.Instance.SetStaticFriction(parameters["staticFriction"].initialValue);
        GameManager.Instance.SetBounciness(parameters["bounciness"].initialValue);
    }

    private void OnMovePowerSliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetPlayerMovePower(evt.newValue);
        parameters["movePower"].field.value = evt.newValue;
    }

    private void OnJumpPowerSliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetPlayerJumpPower(evt.newValue);
        parameters["jumpPower"].field.value = evt.newValue;
    }

    private void OnMassSliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetPlayerMass(evt.newValue);
        parameters["mass"].field.value = evt.newValue;
    }

    private void OnVelocityLimitSliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetPlayerVelocityLimit(evt.newValue);
        parameters["velocityLimit"].field.value = evt.newValue;
    }

    private void OnMaxCameraDistanceliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetMaxCameraDistance(evt.newValue);
        parameters["maxCameraDistance"].field.value = evt.newValue;
    }

    private void OnSensitivitySliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetSensitivity(evt.newValue);
        parameters["sensitivity"].field.value = evt.newValue;
    }

    private void OnDynamicFrictionSliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetDynamicFriction(evt.newValue);
        parameters["dynamicFriction"].field.value = evt.newValue;
    }

    private void OnStaticFrictionSliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetStaticFriction(evt.newValue);
        parameters["staticFriction"].field.value = evt.newValue;
    }

    private void OnBouncinessSliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetBounciness(evt.newValue);
        parameters["bounciness"].field.value = evt.newValue;
    }

    private void OnGravitySliderChanged(ChangeEvent<float> evt)
    {
        GameManager.Instance.SetGravity(evt.newValue);
        parameters["gravity"].field.value = evt.newValue;
    }
}
