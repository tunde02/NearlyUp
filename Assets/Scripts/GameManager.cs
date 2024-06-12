using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public struct SettingValues
{
    public float movePower;
    public float jumpPower;
    public float mass;
    public float gravity;
    public float drag;
    public float cameraDistance;
    public float cameraSensitivity;
    public float bounciness;
    public float velocityLimit;
}


public class GameManager : Singleton<GameManager>
{
    public PlayerController playerController;
    public CameraController cameraController;
    public PhysicMaterial physicMaterial;


    public bool IsGamePaused { get; set; } = false;


    private SettingValues settingValues;
    private SettingValues initialSettingValues;


    private void Start()
    {
        settingValues = SaveManager.Instance.Data.settingValues;
        initialSettingValues = settingValues;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        cameraController = FindObjectOfType(typeof(CameraController)) as CameraController;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void SlowGame()
    {
        Time.timeScale = 0.5f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        IsGamePaused = false;
    }

    public void QuitGame()
    {
        SaveManager.Instance.Data.score = ScoreManager.Instance.GetPlayerScore();
        SaveManager.Instance.Data.settingValues = settingValues;
        SaveManager.Instance.SavePlayerData();
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        // Physic Material은 게임 진행 중 변경된 값이 그대로 저장되기 때문에
        // 게임 시작 전 저장해둔 값으로 복구
        SetPlayerBounciness(initialSettingValues.bounciness);
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public float GetPlayerMovePower()
    {
        return settingValues.movePower;
    }

    public void SetPlayerMovePower(float newMovePower)
    {
        settingValues.movePower = newMovePower;

        if (playerController != null)
        {
            playerController.MovePower = settingValues.movePower;
        }
    }

    public float GetPlayerJumpPower()
    {
        return settingValues.jumpPower;
    }

    public void SetPlayerJumpPower(float newJumpPower)
    {
        settingValues.jumpPower = newJumpPower;

        if (playerController != null)
        {
            playerController.JumpPower = settingValues.jumpPower;
        }
    }

    public float GetPlayerMass()
    {
        return settingValues.mass;
    }

    public void SetPlayerMass(float newMass)
    {
        settingValues.mass = newMass;

        if (playerController != null)
        {
            playerController.Mass = settingValues.mass;
        }
    }

    public float GetGravity()
    {
        return settingValues.gravity;
    }

    public void SetGravity(float newGravity)
    {
        settingValues.gravity = newGravity;

        Physics.gravity = Vector3.down * settingValues.gravity;
    }

    public float GetPlayerDrag()
    {
        return settingValues.drag;
    }

    public void SetPlayerDrag(float newPlayerDrag)
    {
        settingValues.drag = newPlayerDrag;

        if (playerController != null)
        {
            playerController.Drag = settingValues.drag;
        }
    }

    public float GetMaxCameraDistance()
    {
        return settingValues.cameraDistance;
    }

    public void SetMaxCameraDistance(float newMaxCameraDistance)
    {
        settingValues.cameraDistance = newMaxCameraDistance;

        if (cameraController != null)
        {
            cameraController.MaxCameraDistance = settingValues.cameraDistance;
        }
    }

    public float GetCameraSensitivity()
    {
        return settingValues.cameraSensitivity;
    }

    public void SetCameraSensitivity(float newCameraSensitivity)
    {
        settingValues.cameraSensitivity = newCameraSensitivity;

        if (cameraController != null)
        {
            cameraController.Sensitivity = settingValues.cameraSensitivity;
        }
    }

    public float GetPlayerBounciness()
    {
        return settingValues.bounciness;
    }

    public void SetPlayerBounciness(float newPlayerBounciness)
    {
        settingValues.bounciness = newPlayerBounciness;

        if (physicMaterial != null)
        {
            physicMaterial.bounciness = settingValues.bounciness;
        }
    }

    public float GetPlayerVelocityLimit()
    {
        return settingValues.velocityLimit;
    }

    public void SetPlayerVelocityLimit(float newVelocityLimit)
    {
        settingValues.velocityLimit = newVelocityLimit;

        if (playerController != null)
        {
            playerController.VelocityLimit = settingValues.velocityLimit;
        }
    }

    public void EnableGamePlayInputActionMap()
    {
        playerController.EnableGamePlayInputActionMap();
    }

    public void DisableGamePlayInputActionMap()
    {
        playerController.DisableGamePlayInputActionMap();
    }
}
