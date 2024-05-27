using UnityEngine;
using TMPro;


public class LevelStarText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starText;
    [SerializeField] private int level;



    private void Start()
    {
        SetStarText();
    }

    private void SetStarText()
    {
        int tier = ScoreManager.Instance.GetPlayerScore().stageStars[level - 1];
        string[] stars = { "☆", "☆", "☆" };

        for (int i = 0; i < tier; i++)
        {
            stars[i] = "★";
        }

        starText.text = string.Join(" ", stars);
    }
}
