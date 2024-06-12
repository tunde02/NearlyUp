using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class LevelClearCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] tierStarTexts;
    [SerializeField] private GameObject toLevelListButton;
    [SerializeField] private GameObject toTitleButton;


    private void Awake()
    {
        Trophy.OnLevelClear += Trophy_OnLevelClear;
    }

    private void Start()
    {
        UIManager.Instance.AddButtonListener(
            toLevelListButton,
            (data) => { OnToLevelListButtonClicked((PointerEventData)data); }
        );
        UIManager.Instance.AddButtonListener(
            toTitleButton,
            (data) => { OnToTitleButtonClicked((PointerEventData)data); }
        );

        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        Trophy.OnLevelClear -= Trophy_OnLevelClear;
    }

    private void Trophy_OnLevelClear(object sender, Trophy.OnLevelClearArgs args)
    {
        for (int i = 0; i < args.tier; i++)
        {
            tierStarTexts[i].text = "★";
        }

        gameObject.SetActive(true);
    }

    private void OnToLevelListButtonClicked(PointerEventData data)
    {
        Trophy.OnLevelClear -= Trophy_OnLevelClear;
        GameManager.Instance.ResumeGame();
        LevelManager.Instance.LoadLevelListScene();
    }

    private void OnToTitleButtonClicked(PointerEventData data)
    {
        Trophy.OnLevelClear -= Trophy_OnLevelClear;
        GameManager.Instance.ResumeGame();
        LevelManager.Instance.LoadTitleScene();
    }
}
