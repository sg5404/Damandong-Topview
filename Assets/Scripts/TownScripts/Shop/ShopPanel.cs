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
        itemImg.sprite = itemSprite[shopItem.itemNumber];
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
        if (SaveManager.Instance.CurrentUser.money < shopItem.price)
        {
            return;
        }

        SaveManager.Instance.CurrentUser.money -= (int)shopItem.price;
        Debug.Log("���� : " + shopItem.itemName);
        shopItem.upgradeValue++;
        shopItem.price *= 2;
        UpdateValues(shopItem);
    }

    public void DPurchaseItem()
    {
        if (SaveManager.Instance.CurrentUser.experience < dShopItem.price || dShopItem.isBuyit)
        {
            Debug.Log("맇헌");
            return;
        }

        SaveManager.Instance.CurrentUser.experience -= (int)dShopItem.price;
        Debug.Log("���� : " + dShopItem.itemName);
        dShopItem.isBuyit = true;
        dShopItem.upgradeValue++;
        UpdateValues(dShopItem);
        PlayerController.Instance.UpdateDUpgrade();
        Destroy(gameObject);
    }

    public void DPurchaseItemMul()
    {
        if (SaveManager.Instance.CurrentUser.experience < dShopItem.price)
        {
            Debug.Log("돈없음");
            return;
        }

        SaveManager.Instance.CurrentUser.experience -= (int)dShopItem.price;
        Debug.Log("���� : " + dShopItem.itemName);
        // TODO : ������ ������ ���� ��ġ ���
        dShopItem.upgradeValue++;
        // TODO : ������ ���� �� ������ ���ź�� ���� ����
        if (dShopItem.itemNumber != 0 && dShopItem.itemNumber != 1 && dShopItem.itemNumber != 2 && dShopItem.itemNumber != 3)
        {
            if (dShopItem.itemNumber == 4)
                dShopItem.price += 50;
            dShopItem.price *= 2;
        }

        UpdateValues(dShopItem);
        PlayerController.Instance.UpdateDUpgrade();
    }
}
