using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] private GameObject Inventory;
    [SerializeField] private bool isInventory = false;
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject Shop;
    [SerializeField] private bool isShop = false;
    [SerializeField] private bool isSell = false;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
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

        ColorBlock colorBlock = buyButton.colors;
        colorBlock.normalColor = new Color(0.682353f/1, 0.4588236f/1, 0.2980392f/1, 0);
        buyButton.colors = colorBlock;
        buyButton.GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 0);

        colorBlock = sellButton.colors;
        colorBlock.normalColor = new Color(0.682353f / 1, 0.4588236f / 1, 0.2980392f / 1, 1);
        sellButton.colors = colorBlock;
        sellButton.GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 1);

        isSell = false;
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
        ColorBlock colorBlock = buyButton.colors;
        colorBlock.normalColor = new Color(0.682353f / 1, 0.4588236f / 1, 0.2980392f / 1, 0);
        buyButton.colors = colorBlock;
        buyButton.GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 0);

        colorBlock = sellButton.colors;
        colorBlock.normalColor = new Color(0.682353f / 1, 0.4588236f / 1, 0.2980392f / 1, 1);
        sellButton.colors = colorBlock;
        sellButton.GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 1);
    }

    void SellButtonClick()
    {
        ColorBlock colorBlock = buyButton.colors;
        colorBlock.normalColor = new Color(0.682353f / 1, 0.4588236f / 1, 0.2980392f / 1, 1);
        buyButton.colors = colorBlock;
        buyButton.GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 1);

        colorBlock = sellButton.colors;
        colorBlock.normalColor = new Color(0.682353f / 1, 0.4588236f / 1, 0.2980392f / 1, 0);
        sellButton.colors = colorBlock;
        sellButton.GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 0);
    }
}
