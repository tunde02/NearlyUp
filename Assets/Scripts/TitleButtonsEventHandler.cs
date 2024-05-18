using UnityEngine;
using UnityEngine.EventSystems;


public class TitleButtonsEventHandler : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject exitButton;


    private void Start()
    {
        UIManager.Instance.AddButtonListener(
            startButton,
            (data) => { OnStartButtonClick((PointerEventData)data); }
        );
        UIManager.Instance.AddButtonListener(
            settingsButton,
            (data) => { OnSettingsButtonClick((PointerEventData)data); }
        );
        UIManager.Instance.AddButtonListener(
            exitButton,
            (data) => { OnExitButtonClick((PointerEventData)data); }
        );
    }

    private void OnStartButtonClick(PointerEventData data)
    {
        LevelManager.Instance.LoadLevelListScene();
    }

    private void OnSettingsButtonClick(PointerEventData data)
    {
    }

    private void OnExitButtonClick(PointerEventData data)
    {
        GameManager.Instance.QuitGame();
    }
}
