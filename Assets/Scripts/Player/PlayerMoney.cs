using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoney : MonoSingleton<PlayerMoney>
{
    [SerializeField] TextMeshProUGUI moneyText;

    private void Start()
    {
        ChangeMoney(0);
    }


    public void ChangeMoney(float price)
    {
        SaveManager.Instance.CurrentUser.money += price * PlayerStat.GetMulGold();
        moneyText.text = string.Format("{0} C", (int)SaveManager.Instance.CurrentUser.money);
    }
}
