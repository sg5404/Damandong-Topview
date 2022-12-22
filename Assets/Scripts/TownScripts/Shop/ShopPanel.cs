using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    [SerializeField]
    private Image itemIcon = null;
    [SerializeField]
    private TextMeshProUGUI itemName = null;
    [SerializeField]
    private TextMeshProUGUI itemPriceTMP = null;
    [SerializeField]
    private long itemPrice;
    [SerializeField]
    private TextMeshProUGUI itemDiscription = null;
    [SerializeField]
    private long itemUpgradeValue;
    [SerializeField]
    private Button itemPurchaseBtn = null;
    [SerializeField]
    private Sprite[] itemSprites;

    private ShopItem shopItem = null;
    private DungeonItem dShopItem = null;

    public void SetValue(ShopItem shopItem)
    {
        this.shopItem = shopItem;
        UpdateValues(this.shopItem);
    }

    public void SetValue(DungeonItem shopItem)
    {
        dShopItem = shopItem;
        UpdateValues(dShopItem);
    }

    public void UpdateValues(ShopItem shopItem)
    {
        itemIcon.sprite = itemSprites[shopItem.itemNumber];
        itemName.text = shopItem.itemName;
        itemDiscription.text = shopItem.itemDiscription;
        itemPriceTMP.text = string.Format("{0}$", shopItem.price);
        itemUpgradeValue = shopItem.upgradeValue;
        SaveManager.Instance.SaveToJson();
    }

    public void UpdateValues(DungeonItem shopItem)
    {
        //itemIcon.sprite = shopItem.itemIcon;
        itemName.text = shopItem.itemName;
        itemDiscription.text = shopItem.itemDiscription;
        itemPriceTMP.text = string.Format("{0}$", shopItem.price);
        itemUpgradeValue = shopItem.upgradeValue;
        PlayerBase.Instance.GetItem();
        SaveManager.Instance.SaveToJson();
    }

    public void SPurchaseItem()
    {
        //Debug.Log("Purchase");
        if (SaveManager.Instance.CurrentUser.money < shopItem.price)
        {
            return;
        }

        SaveManager.Instance.CurrentUser.money -= (int)shopItem.price;
        Debug.Log("���� : " + shopItem.itemName);
        shopItem.upgradeValue++;
        UpdateValues(shopItem);
    }

    public void DPurchaseItem()
    {
        if (SaveManager.Instance.CurrentUser.money < dShopItem.price || dShopItem.upgradeValue>0)
        {
            return;
        }

        SaveManager.Instance.CurrentUser.money -= (int)dShopItem.price;
        Debug.Log("���� : " + dShopItem.itemName);
        dShopItem.upgradeValue++;
        UpdateValues(dShopItem);
    }
}
