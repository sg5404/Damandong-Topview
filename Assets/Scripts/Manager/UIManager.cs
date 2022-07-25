using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

@ -11,39 +10,36 @@ public class UIManager : MonoSingleton<UIManager>

    [SerializeField] Sprite[] weaponSprites;

    [SerializeField] public Slider plyerHpSlider;
    [SerializeField] public TextMeshProUGUI playerHpTmp;

    private void Update()
    {

        ChangeWeaponSpriteImg();
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