using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    [SerializeField]
    private Image itemImg = null;
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
    private Sprite[] itemSprite = null;

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
        //itemImg.sprite = itemSprite[shopItem.itemNumber];
        itemName.text = shopItem.itemName;
        itemDiscription.text = shopItem.itemDiscription;
        itemPriceTMP.text = string.Format("{0}$", shopItem.price);
        itemUpgradeValue = shopItem.upgradeValue;
    }

    public void UpdateValues(DungeonItem shopItem)
    {
        itemImg.sprite = shopItem.itemImage;
        itemName.text = shopItem.itemName;
        itemDiscription.text = shopItem.itemDiscription;
        itemPriceTMP.text = string.Format("{0}$", shopItem.price);
        itemUpgradeValue = shopItem.upgradeValue;
    }

    public void SPurchaseItem()
    {
        if (SaveManager.Instance.CurrentUser.money < shopItem.price)
        {
            return;
        }

        SaveManager.Instance.CurrentUser.money -= (int)shopItem.price;
        Debug.Log("구매 : " + shopItem.itemName);
        // TODO : 아이템 종류에 따라 수치 상승
        shopItem.upgradeValue++;
        // TODO : 아이템 구매 시 아이템 구매비용 증가 수열
        UpdateValues(shopItem);
    }

    public void DPurchaseItem()
    {
        if (SaveManager.Instance.CurrentUser.money < dShopItem.price)
        {
            return;
        }

        SaveManager.Instance.CurrentUser.money -= (int)dShopItem.price;
        Debug.Log("구매 : " + dShopItem.itemName);
        // TODO : 아이템 종류에 따라 수치 상승
        dShopItem.upgradeValue++;
        // TODO : 아이템 구매 시 아이템 구매비용 증가 수열
        UpdateValues(dShopItem);
    }
}
