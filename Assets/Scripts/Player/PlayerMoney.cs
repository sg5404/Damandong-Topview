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
    }

    public void UpdateMoneyText()
    {
        //moneyText.SetText($"{money}");
        moneyText.text = string.Format("{0}", SaveManager.Instance.CurrentUser.money);
    }
}
