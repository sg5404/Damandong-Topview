using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoSingleton<InventoryScript>
{
    [Header("Inventory")]
    [SerializeField] private GameObject Inventory;
    [SerializeField] private bool isInventory = false;
    [SerializeField] private GameObject Content;

    [Header("Shop")]
    [SerializeField] private GameObject Shop;
    [SerializeField] private bool isSell = false;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private GameObject buyWindow;
    [SerializeField] private GameObject sellWindow;
    public bool isShop { get; private set; } = false;


    void Start()
    {
        Inventory.SetActive(false);
        Shop.SetActive(false);
        ResetUI();
        SetUI();
    }

    void SetUI()
    {
        buyButton.onClick.AddListener(BuyButtonClick);
        sellButton.onClick.AddListener(SellButtonClick);

        SellButtonClick();
        BuyButtonClick();
    }

    void ResetUI()
    {
        buyButton.onClick.RemoveAllListeners();
        sellButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            isInventory = !isInventory;
            Inventory.SetActive(isInventory);
            Content.transform.localPosition = new Vector3(0, 0, 0);
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            isShop = !isShop;
            isInventory = isShop;
            Inventory.SetActive(isInventory);
            Shop.SetActive(isShop);
            Content.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    void BuyButtonClick()
    {
        ColorChange(buyButton, 0);
        ColorChange(sellButton, 1);
        sellWindow.SetActive(false);
        buyWindow.SetActive(true);
    }

    void SellButtonClick()
    {
        ColorChange(buyButton, 1);
        ColorChange(sellButton, 0);
        sellWindow.SetActive(true);
        buyWindow.SetActive(false);
    }

    void ColorChange(Button button, int a)
    {
        ColorBlock colorBlock = button.colors;
        colorBlock.normalColor = new Color(0.682353f / 1, 0.4588236f / 1, 0.2980392f / 1, a);
        button.colors = colorBlock;
        button.GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, a);
    }

}
