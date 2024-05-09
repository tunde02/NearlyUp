using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ReturnButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Title");
    }
}
