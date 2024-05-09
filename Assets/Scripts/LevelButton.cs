using UnityEngine;
using UnityEngine.EventSystems;


public class LevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int targetLevel = 0;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (targetLevel == 0)
        {
            Debug.LogError("LevelButton.OnPointerClick() - targetLevel didn't assigned");
            return;
        }

        LevelManager.Instance.LoadLevel(targetLevel);
    }
}
