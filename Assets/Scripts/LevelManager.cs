using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : Singleton<LevelManager>
{
    private int currentLevel = 0;


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

        currentLevel = level;
        SceneManager.LoadScene($"Level {level}");
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
