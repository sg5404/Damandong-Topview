using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerExperience : MonoSingleton<PlayerExperience>
{
    [SerializeField]
    private float maxExp;
    [SerializeField] TextMeshProUGUI expText;
    private float currentExp;

    private void Start()
    {
        StartCoroutine(RemoveExp());
        ChangeExperience(0);
    }


    public void ChangeExperience(float exp)
    {
        var CurrencUser = SaveManager.Instance.CurrentUser;
        CurrencUser.experience += exp * PlayerStat.GetMulExp();
        expText.text = string.Format("{0} Exp", (int)SaveManager.Instance.CurrentUser.experience);
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
