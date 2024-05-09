using UnityEngine;
using UnityEngine.EventSystems;


public class TitleButtonsEventHandler : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject settingButton;
    [SerializeField] private GameObject exitButton;


    private void Start()
    {
        EventTrigger.Entry startButtonEntry = new() {
            eventID = EventTriggerType.PointerClick
        };
        startButtonEntry.callback.AddListener((data) => { OnStartButtonClick((PointerEventData)data); });
        startButton.AddComponent<EventTrigger>().triggers.Add(startButtonEntry);

        EventTrigger.Entry settingButtonEntry = new() {
            eventID = EventTriggerType.PointerClick
        };
        settingButtonEntry.callback.AddListener((data) => { OnSettingButtonClick((PointerEventData)data); });
        settingButton.AddComponent<EventTrigger>().triggers.Add(settingButtonEntry);

        EventTrigger.Entry exitButtonEntry = new() {
            eventID = EventTriggerType.PointerClick
        };
        exitButtonEntry.callback.AddListener((data) => { OnExitButtonClick((PointerEventData)data); });
        exitButton.AddComponent<EventTrigger>().triggers.Add(exitButtonEntry);
    }

    private void OnStartButtonClick(PointerEventData data)
    {
        Debug.Log("Start Button Clicked");
        LevelManager.Instance.LoadLevelListScene();
    }

    private void OnSettingButtonClick(PointerEventData data)
    {
        // TODO
        Debug.Log("Setting Button Clicked");
    }

    private void OnExitButtonClick(PointerEventData data)
    {
        Debug.Log("Exit Button Clicked");
        GameManager.Instance.QuitGame();
    }
}
