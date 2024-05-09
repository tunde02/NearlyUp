using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class TextHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI text;


    private string initialText;


    private void Start()
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
}
