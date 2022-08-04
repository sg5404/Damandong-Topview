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


    private void Awake()
    {
        ChangeUIWeaponSpriteImg(PlayerAttack.Instance.weapon);
    }

    public void ChangeUIWeaponSpriteImg(int weaponNumber)
    {
        DisableAllWeaponSpriteImg();
        subWeaponImageObj[weaponNumber].SetActive(true);

        main_magazineQuantity.text = $"{PlayerCtrl.Instance.rightWeapons[(int)PlayerCtrl.Instance.weaponSet.SetWeaponNum().x - 1].GetComponent<Consumable>().weaponModule.magazine}/{PlayerCtrl.Instance.rightWeapons[(int)PlayerCtrl.Instance.weaponSet.SetWeaponNum().x - 1].GetComponent<Consumable>().weaponModule.maxMagazine}";
        sub_magazineQuantity.text = $"{PlayerCtrl.Instance.leftWeapons[(int)PlayerCtrl.Instance.weaponSet.SetWeaponNum().y - 1].GetComponent<Consumable>().weaponModule.magazine}/{PlayerCtrl.Instance.leftWeapons[(int)PlayerCtrl.Instance.weaponSet.SetWeaponNum().y - 1].GetComponent<Consumable>().weaponModule.maxMagazine}";
    }

    void DisableAllWeaponSpriteImg()
    {
        foreach(var imgItem in subWeaponImageObj)
        {
            imgItem.SetActive(false);
        }
    }
}