public struct PlayerScore
{
    public int[] stageStars;
}


public class ScoreManager : Singleton<ScoreManager>
{
    private PlayerScore playerScore = new() {
        stageStars = new int[5] { 0, 0, 0, 0, 0 }
    };


    private void Start()
    {
        Trophy.OnLevelClear += Trophy_OnLevelClear;
    }

    private void Trophy_OnLevelClear(object sender, Trophy.OnLevelClearArgs args)
    {
        playerScore.stageStars[args.level - 1] = args.tier;
    }

    public PlayerScore GetPlayerScore()
    {
        return playerScore;
    }
}
