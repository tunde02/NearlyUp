using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public PlayerController playerController;
    public CameraController cameraController;
    public PhysicMaterial physicMaterial;


    public bool IsGamePaused { get; set; } = false;


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
        Application.Quit();
    }

    public float GetPlayerMovePower()
    {
        return playerController.MovePower;
    }

    public void SetPlayerMovePower(float newMovePower)
    {
        playerController.MovePower = newMovePower;
    }

    public float GetPlayerJumpPower()
    {
        return playerController.JumpPower;
    }

    public void SetPlayerJumpPower(float newJumpPower)
    {
        playerController.JumpPower = newJumpPower;
    }

    public float GetPlayerMass()
    {
        return playerController.Mass;
    }

    public void SetPlayerMass(float newMass)
    {
        playerController.Mass = newMass;
    }

    public float GetPlayerVelocityLimit()
    {
        return playerController.VelocityLimit;
    }

    public void SetPlayerVelocityLimit(float newVelocityLimit)
    {
        playerController.VelocityLimit = newVelocityLimit;
    }

    public float GetMaxCameraDistance()
    {
        return cameraController.MaxCameraDistance;
    }

    public void SetMaxCameraDistance(float newMaxCameraDistance)
    {
        cameraController.MaxCameraDistance = newMaxCameraDistance;
    }

    public float GetSensitivity()
    {
        return cameraController.Sensitivity;
    }

    public void SetSensitivity(float newSensitivity)
    {
        cameraController.Sensitivity = newSensitivity;
    }

    public float GetDynamicFriction()
    {
        return physicMaterial.dynamicFriction;
    }

    public void SetDynamicFriction(float newDynamicFriction)
    {
        physicMaterial.dynamicFriction = newDynamicFriction;
    }

    public float GetStaticFriction()
    {
        return physicMaterial.staticFriction;
    }

    public void SetStaticFriction(float newStaticFriction)
    {
        physicMaterial.staticFriction = newStaticFriction;
    }

    public float GetBounciness()
    {
        return physicMaterial.bounciness;
    }

    public void SetBounciness(float newBounciness)
    {
        physicMaterial.bounciness = newBounciness;
    }

    public float GetGravity()
    {
        return -Physics.gravity.y;
    }

    public void SetGravity(float newGravity)
    {
        Physics.gravity = Vector3.down * newGravity;
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
