using UnityEngine;


public class BaseItem : MonoBehaviour
{
    public string ItemName { get; protected set; } = "BaseItem";
    public float ItemPrice { get; protected set; } = 100f;
    public string ItemDescription { get; protected set; } = "This is BaseItem.";


    public virtual void UseItem(PlayerController player)
    {
        Debug.Log("BaseItem.UseItem()");
    }
}
