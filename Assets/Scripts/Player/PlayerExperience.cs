using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoSingleton<PlayerExperience>
{
    [SerializeField]
    private int maxExp;
    private int currentExp;

    private void Start()
    {
        StartCoroutine(RemoveExp());
        ChangeExperience(0);
    }


    public void ChangeExperience(int exp)
    {
        var CurrencUser = SaveManager.Instance.CurrentUser;
        CurrencUser.experience += exp;
    }

    public void UpdateExperienceText()
    {

    }

    private IEnumerator RemoveExp()
    {
        currentExp = maxExp;
        while(true)
        {
            if (currentExp <= 0) break;
            currentExp--;
            //Debug.Log(currentExp);
            yield return new WaitForSeconds(1f);
        }
    }

    public void StopRemoveExp()
    {
        StopAllCoroutines();
        ChangeExperience(currentExp);
    }

    public void RestartRemoveExp()
    {
        StartCoroutine(RemoveExp());
    }
}
