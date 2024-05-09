using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;


public class Shop : MonoBehaviour
{
    public Canvas shopCanvas;


    public List<BaseItem> Inventory;


    void Awake()
    {
    }

    void Start()
    {
        shopCanvas.enabled = false;

        PlayerController.OnInventoryChanged += PlayerController_OnInventoryChanged;
    }

    void OnDisable()
    {
        PlayerController.OnInventoryChanged -= PlayerController_OnInventoryChanged;
    }

    private void OnTriggerEnter(Collider other)
    {
        OpenShopUI();
    }

    public void OpenShopUI()
    {
        shopCanvas.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameManager.Instance.DisableGamePlayInputActionMap();
        GameManager.Instance.PauseGame();
    }

    public void CloseShopUI()
    {
        shopCanvas.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameManager.Instance.EnableGamePlayInputActionMap();
        GameManager.Instance.ResumeGame();
    }

    public void PlayerController_OnInventoryChanged(object sender, PlayerController.OnInventoryChangedArgs eventArgs)
    {
        foreach (var i in eventArgs.inventory)
        {
            if (i != null)
                Debug.Log(i.ItemName);
            else
                Debug.Log("EMPTY");
        }
    }
}
