using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : Singleton<LevelManager>
{
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void LoadLevelListScene()
    {
        SceneManager.LoadScene("Level List");
    }

    public void LoadLevel(int level)
    {
        Debug.LogWarning($"Load Scene - Level {level}");
        SceneManager.LoadScene($"Level {level}");
    }
}
