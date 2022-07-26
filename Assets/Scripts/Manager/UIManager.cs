using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] Image leftWepaonImg;
    [SerializeField] Image rightWepaonImg;

    [SerializeField] Sprite[] weaponSprites;

    private void Update()
    {
        //ChangeWeaponSpriteImg();
    }

    public void ChangeWeaponSpriteImg()
    {
        if (WeaponSet.Instance.SubWeaponState == WeaponKind.SWORD)
        {
            rightWepaonImg.sprite = weaponSprites[0];
        }
        if (WeaponSet.Instance.SubWeaponState == WeaponKind.RIFLE)
        {
            rightWepaonImg.sprite = weaponSprites[1];
        }
        if (WeaponSet.Instance.SubWeaponState == WeaponKind.SNIPER)
        {
            rightWepaonImg.sprite = weaponSprites[2];
        }
        if (WeaponSet.Instance.SubWeaponState == WeaponKind.SHOTGUN)
        {
            rightWepaonImg.sprite = weaponSprites[3];
        }
        if (WeaponSet.Instance.SubWeaponState == WeaponKind.GRANADE)
        {
            rightWepaonImg.sprite = weaponSprites[4];
        }
        else
        {
            return;
        }
    }
}
