using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoSingleton<PlayerExperience>
{
    private static int money;

    private void Start()
    {
        ChangeExperience(0);
    }


    public void ChangeExperience(int exp)
    {
        var CurrencUser = SaveManager.Instance.CurrentUser;
        CurrencUser.experience += exp;
        if(CurrencUser.experience >= CurrencUser.maxExperience[CurrencUser.level - 1])
        {
            CurrencUser.experience -= CurrencUser.maxExperience[CurrencUser.level - 1];
            CurrencUser.level++;
        }
    }

    public void UpdateExperienceText()
    {

    }
}
