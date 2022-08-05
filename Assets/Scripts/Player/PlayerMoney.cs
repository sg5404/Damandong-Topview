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
        money += price;
        moneyText.SetText($"{money}");
    }
}
