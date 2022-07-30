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

    private void Update()
    {
        //ChangeWeaponSpriteImg();
    }

    public void ChangeUIWeaponSpriteImg(int weaponNumber)
    {
        DisableAllWeaponSpriteImg();
        subWeaponImageObj[weaponNumber - 1].SetActive(true);
    }

    void DisableAllWeaponSpriteImg()
    {
        foreach(var imgItem in subWeaponImageObj)
        {
            imgItem.SetActive(false);
        }
    }

    //public void ChangeWeaponSpriteImg()
    //{
    //    if (WeaponSet.Instance.SubWeaponState == WeaponKind.SWORD)
    //    {
    //        rightWepaonImg.sprite = weaponSprites[0];
    //    }
    //    if (WeaponSet.Instance.SubWeaponState == WeaponKind.RIFLE)
    //    {
    //        rightWepaonImg.sprite = weaponSprites[1];
    //    }
    //    if (WeaponSet.Instance.SubWeaponState == WeaponKind.SNIPER)
    //    {
    //        rightWepaonImg.sprite = weaponSprites[2];
    //    }
    //    if (WeaponSet.Instance.SubWeaponState == WeaponKind.SHOTGUN)
    //    {
    //        rightWepaonImg.sprite = weaponSprites[3];
    //    }
    //    if (WeaponSet.Instance.SubWeaponState == WeaponKind.GRANADE)
    //    {
    //        rightWepaonImg.sprite = weaponSprites[4];
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}
}