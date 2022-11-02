using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoney : MonoSingleton<PlayerMoney>
{
    [SerializeField] TextMeshProUGUI moneyText;

    private static int money;

    private void Start()
    {
        ChangeMoney(0);
    }


    public void ChangeMoney(int price)
    {
        SaveManager.Instance.CurrentUser.money += price;
        moneyText.text = string.Format("{0}", SaveManager.Instance.CurrentUser.money);
    }

    public void UpdateMoneyText()
    {
        //moneyText.SetText($"{money}");
        //moneyText.text = string.Format("{0}", SaveManager.Instance.CurrentUser.money);
        //위에 한줄 일단 주석쳐놓음
    }
}
