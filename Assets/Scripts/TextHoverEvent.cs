using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class TextHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI text;


    private string initialText;


    private void Awake()
    {
        initialText = text.text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.text = $"<color=white>{initialText}</color>";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.text = $"<color=black>{initialText}</color>";
    }

    private void OnDisable()
    {
        text.text = $"<color=black>{initialText}</color>";
    }
}
