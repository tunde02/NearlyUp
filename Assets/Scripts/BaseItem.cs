using UnityEngine;
using TMPro;


public class BaseItem : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI itemDescriptionText;

    public string ItemName { get; protected set; } = "BaseItem";
    public float ItemPrice { get; protected set; } = 100f;
    public string ItemDescription { get; protected set; } = "This is BaseItem.";


    private void Start()
    {
        UpdateItemText();
    }

    private void UpdateItemText()
    {
        itemNameText.text = ItemName;
        itemPriceText.text = $"{Mathf.Floor(ItemPrice):0} $";
        itemDescriptionText.text = ItemDescription;
    }

    public virtual void UseItem(PlayerController player)
    {
        Debug.Log("BaseItem.UseItem()");
    }
}
