using UnityEngine;
using UnityEngine.EventSystems;


public class UIManager : Singleton<UIManager>
{
    public GameObject menuCanvas;
    public GameObject menu;
    public GameObject settings;


    public void OpenMenu()
    {
        menuCanvas.SetActive(true);
        SetMenuState(true);

        GameManager.Instance.ShowCursor();
        GameManager.Instance.PauseGame();
    }

    public void CloseMenu()
    {
        menuCanvas.SetActive(false);
        SetMenuState(false);

        GameManager.Instance.HideCursor();
        GameManager.Instance.ResumeGame();
    }

    public void AddButtonListener(GameObject button, UnityEngine.Events.UnityAction<BaseEventData> call)
    {
        EventTrigger.Entry buttonEntry = new() {
            eventID = EventTriggerType.PointerClick
        };
        buttonEntry.callback.AddListener(call);

        if (button.GetComponent<EventTrigger>() == null)
        {
            button.AddComponent<EventTrigger>().triggers.Add(buttonEntry);
        }
        else
        {
            button.GetComponent<EventTrigger>().triggers.Add(buttonEntry);
        }
    }

    public void SetMenuState(bool state)
    {
        menu.SetActive(state);
    }

    public void SetSettingsState(bool state)
    {
        settings.SetActive(state);
    }
}
