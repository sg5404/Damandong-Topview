using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] Sprite[] weaponSprites;

    [SerializeField] GameObject[] mainWeaponImageObj;
    [SerializeField] GameObject[] subWeaponImageObj;

    [SerializeField] public Slider plyerHpSlider;
    [SerializeField] public TextMeshProUGUI playerHpTmp;
    [SerializeField] public TextMeshProUGUI main_magazineQuantity;
    [SerializeField] public TextMeshProUGUI sub_magazineQuantity;

    public void ChangeUIWeaponSpriteImg(int weaponNumber)
    {
        DisableAllWeaponSpriteImg();
        subWeaponImageObj[weaponNumber].SetActive(true);
    }

    void DisableAllWeaponSpriteImg()
    {
        foreach(var imgItem in subWeaponImageObj)
        {
            imgItem.SetActive(false);
        }
    }
}