using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonShopManager : MonoSingleton<DungeonShopManager>
{
    [SerializeField]
    private GameObject shopPanelTemplate = null;

    private List<ShopPanel> shopPanelList = new List<ShopPanel>();

    private void Start()
    {
        CreatePanels();
    }

    private void CreatePanels()
    {
        GameObject newPanel = null;
        ShopPanel newPanelComponent = null;

        //int i = 0;
        //foreach (ShopItem shopItems in SaveManager.Instance.CurrentUser.shopItem)
        //{
        //    ++i;
        //}
        //Debug.Log(i);

        foreach (DungeonItem shopItems in SaveManager.Instance.CurrentUser.shopItemInDungeon)
        {
            Debug.Log("아이템 생성");
            newPanel = Instantiate(shopPanelTemplate, shopPanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<ShopPanel>();
            newPanelComponent.SetValue(shopItems);
            newPanel.SetActive(true);
            shopPanelList.Add(newPanelComponent);
        }
    }
}
