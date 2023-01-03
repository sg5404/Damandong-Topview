using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonShopManager : MonoSingleton<DungeonShopManager>
{
    [SerializeField]
    private GameObject shopPanelTemplateOne = null;
    [SerializeField]
    private GameObject shopPanelTemplateMul = null;

    private List<ShopPanel> shopOnePanelList = new List<ShopPanel>();
    private List<ShopPanel> shopMulPanelList = new List<ShopPanel>();

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

        foreach (DungeonItem shopItems in SaveManager.Instance.CurrentUser.shopItemInDungeonOne)
        {
            Debug.Log("아이템 생성");
            newPanel = Instantiate(shopPanelTemplateOne, shopPanelTemplateOne.transform.parent);
            newPanelComponent = newPanel.GetComponent<ShopPanel>();
            newPanelComponent.SetValue(shopItems);
            newPanel.SetActive(true);
            shopOnePanelList.Add(newPanelComponent);
        }
        foreach (DungeonItem shopItems in SaveManager.Instance.CurrentUser.shopItemInDungeonMul)
        {
            Debug.Log("아이템 생성");
            newPanel = Instantiate(shopPanelTemplateMul, shopPanelTemplateMul.transform.parent);
            newPanelComponent = newPanel.GetComponent<ShopPanel>();
            newPanelComponent.SetValue(shopItems);
            newPanel.SetActive(true);
            shopMulPanelList.Add(newPanelComponent);
        }
    }
}
