using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI[] itemTexts;


    void Start()
    {
        PlayerController.OnInventoryChanged += PlayerController_OnInventoryChanged;

        UpdateInventory(new BaseItem[3] { null, null, null });
    }

    private void PlayerController_OnInventoryChanged(object sender, PlayerController.OnInventoryChangedArgs e)
    {
        UpdateInventory(e.inventory);
    }

    public void UpdateInventory(BaseItem[] inventory)
    {
        for (int i = 0; i < 3; i++)
        {
            itemTexts[i].text = inventory[i] != null ? inventory[i].ItemName : "";
        }
    }
}
