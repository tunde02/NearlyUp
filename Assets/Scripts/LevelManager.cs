using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : Singleton<LevelManager>
{
    public void LoadLevelListScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(int level)
    {
        Debug.LogWarning($"Load Scene - Level {level}");
        SceneManager.LoadScene($"Level {level}");
    }
}
