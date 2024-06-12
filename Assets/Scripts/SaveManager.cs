using System.IO;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public PlayerScore score;
    public SettingValues settingValues;

    public PlayerData()
    {
        score = new PlayerScore() {
            stageStars = new int[3] { 0, 0, 0 }
        };
        settingValues = new SettingValues() {
            movePower = 20f,
            jumpPower = 30f,
            mass = 5f,
            gravity = 6.8f,
            drag = 0.1f,
            cameraDistance = 7f,
            cameraSensitivity = 15f,
            bounciness = 0.05f,
            velocityLimit = 25f
        };
    }
}


public class SaveManager : Singleton<SaveManager>
{
    private readonly string saveFileName = "PlayerData.json";
    private string filePath;


    private PlayerData data;
    public PlayerData Data
    {
        get
        {
            if (data == null)
            {
                LoadPlayerData();
            }

            return data;
        }
    }


    private void Start()
    {
        filePath = $"{Application.persistentDataPath}/{saveFileName}";
        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        if (HasPlayerData())
        {
            Debug.Log("SaveManager.LoadPlayerData()");
            string fromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<PlayerData>(fromJsonData);
        }
        else
        {
            Debug.Log("불러올 파일이 없으므로 새로운 PlayerData.json 파일 생성");
            data = new PlayerData();
        }
    }

    public bool HasPlayerData()
    {
        return File.Exists(filePath);
    }

    public void SavePlayerData()
    {
        string toJsonData = JsonUtility.ToJson(data);

        File.WriteAllText(filePath, toJsonData);
    }
}
