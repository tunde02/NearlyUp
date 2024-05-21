using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject titleButton;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UIManager.Instance.AddButtonListener(
            resumeButton,
            (data) => { OnResumeButtonClick((PointerEventData)data); }
        );
        UIManager.Instance.AddButtonListener(
            settingsButton,
            (data) => { OnSettingsButtonClick((PointerEventData)data); }
        );
        UIManager.Instance.AddButtonListener(
            titleButton,
            (data) => { OnTitleButtonClick((PointerEventData)data); }
        );

        gameObject.SetActive(false);
    }

    private void OnResumeButtonClick(PointerEventData data)
    {
        UIManager.Instance.CloseMenu();
        GameManager.Instance.playerController.SwitchToGamePlayActionMap();
    }

    private void OnSettingsButtonClick(PointerEventData data)
    {
        UIManager.Instance.SetMenuState(false);
        UIManager.Instance.SetSettingsState(true);
    }

    private void OnTitleButtonClick(PointerEventData data)
    {
        // TODO: Popup
        UIManager.Instance.CloseMenu();
        GameManager.Instance.ShowCursor();
        LevelManager.Instance.LoadTitleScene();
    }
}
