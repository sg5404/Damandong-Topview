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
    [SerializeField]
    private ShopItem shopItem = null;
    [SerializeField]
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
        SaveManager.Instance.SaveToJson();
    }

    public void UpdateValues(DungeonItem shopItem)
    {
        itemImg.sprite = shopItem.itemImage;
        itemName.text = shopItem.itemName;
        itemDiscription.text = shopItem.itemDiscription;
        itemPriceTMP.text = string.Format("{0}$", shopItem.price);
        itemUpgradeValue = shopItem.upgradeValue;
        SaveManager.Instance.SaveToJson();
    }

    public void SPurchaseItem()
    {
        //Debug.Log("Purchase");
        //if (SaveManager.Instance.CurrentUser.money < shopItem.price)
        //{
        //    return;
        //}

        SaveManager.Instance.CurrentUser.money -= (int)shopItem.price;
        Debug.Log("���� : " + shopItem.itemName);
        shopItem.upgradeValue++;
        UpdateValues(shopItem);
    }

    public void DPurchaseItem()
    {
        if (SaveManager.Instance.CurrentUser.money < dShopItem.price || dShopItem.isBuyit)
        {
            Debug.Log("맇헌");
            return;
        }

        SaveManager.Instance.CurrentUser.money -= (int)dShopItem.price;
        Debug.Log("���� : " + dShopItem.itemName);
        dShopItem.isBuyit = true;
        // TODO : ������ ������ ���� ��ġ ���
        dShopItem.upgradeValue++;
        // TODO : ������ ���� �� ������ ���ź�� ���� ����
        UpdateValues(dShopItem);
        PlayerController.Instance.UpdateDUpgrade();
    }

    public void DPurchaseItemMul()
    {
        if (SaveManager.Instance.CurrentUser.money < dShopItem.price)
        {
            Debug.Log("ㄹ잍언");
            return;
        }

        SaveManager.Instance.CurrentUser.money -= (int)dShopItem.price;
        Debug.Log("���� : " + dShopItem.itemName);
        // TODO : ������ ������ ���� ��ġ ���
        dShopItem.upgradeValue++;
        // TODO : ������ ���� �� ������ ���ź�� ���� ����
        UpdateValues(dShopItem);
        PlayerController.Instance.UpdateDUpgrade();
    }
}
