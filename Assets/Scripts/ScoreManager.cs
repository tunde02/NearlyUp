using UnityEngine;


[System.Serializable]
public struct PlayerScore
{
    public int[] stageStars;
}


public class ScoreManager : Singleton<ScoreManager>
{
    private PlayerScore playerScore;


    private void Start()
    {
        playerScore = SaveManager.Instance.Data.score;
        Trophy.OnLevelClear += Trophy_OnLevelClear;
    }

    private void Trophy_OnLevelClear(object sender, Trophy.OnLevelClearArgs args)
    {
        playerScore.stageStars[args.level - 1] = Mathf.Max(playerScore.stageStars[args.level - 1], args.tier);
    }

    public PlayerScore GetPlayerScore()
    {
        return playerScore;
    }
}
