using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoSingleton<ShopManager>
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

        foreach(ShopItem shopItems in SaveManager.Instance.CurrentUser.shopItem)
        {
            newPanel = Instantiate(shopPanelTemplate, shopPanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<ShopPanel>();
            newPanelComponent.SetValue(shopItems);
            newPanel.SetActive(true);
            shopPanelList.Add(newPanelComponent);
        }
    }

    public void IncreaseStat(int item)
    {
        switch(item)
        {
            case 0:
                PlayerStat.UpAddAtk();
                break;
            case 1:
                PlayerStat.UpPerAtk();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                PlayerStat.UpAddHP();
                break;
            case 5:
                PlayerStat.UpPerHP();
                break;
            case 6:
                //탄창증가히히
                break;
            case 7:
                PlayerStat.UpMoveSpeed();
                break;
            case 8:
                PlayerStat.UpCriticalChance();
                break;
            case 9:
                PlayerStat.UpCriticalDamage();
                break;
        }
    }
}
