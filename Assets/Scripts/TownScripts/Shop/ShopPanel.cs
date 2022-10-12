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
    private TextMeshProUGUI itemPrice = null;
    [SerializeField]
    private TextMeshProUGUI itemDiscription = null;
    [SerializeField]
    private long itemUpgradeValue;
    [SerializeField]
    private Button itemPurchaseBtn = null;
    [SerializeField]
    private Sprite[] itemSprite = null;

    private ShopItem shopItem = null;

    public void SetValue(ShopItem shopItem)
    {
        this.shopItem = shopItem;
        UpdateValues();
    }

    public void UpdateValues()
    {
        itemImg.sprite = itemSprite[shopItem.itemNumber];
        itemName.text = shopItem.itemName;
        itemDiscription.text = shopItem.itemDiscription;
        itemPrice.text = string.Format("{0}$", shopItem.price);
        itemUpgradeValue = shopItem.upgradeValue;
    }
}
