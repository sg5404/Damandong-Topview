using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] Sprite[] weaponSprites;

    [SerializeField] GameObject[] mainWeaponImageObj; // right
    [SerializeField] GameObject[] subWeaponImageObj; // left

    [SerializeField] public Slider plyerHpSlider;
    [SerializeField] public TextMeshProUGUI playerHpTmp;
    //[SerializeField] public TextMeshProUGUI main_magazineQuantity;
    //[SerializeField] public TextMeshProUGUI sub_magazineQuantity;

    private void Awake()
    {
        ChangeUIWeaponSpriteImg(PlayerAttack.Instance.leftWeapon);
        mainWeaponImageObj[PlayerAttack.Instance.rightWeapon].SetActive(true);
    }

    public void ChangeUIWeaponSpriteImg(int leftWeaponNumber)
    {
        DisableAllWeaponSpriteImg();
        subWeaponImageObj[leftWeaponNumber].SetActive(true);

        //sub_magazineQuantity.text = $"{PlayerBase.Instance.SubMagazine}/{PlayerBase.Instance.SubMaxMagazine}";
    }

    void DisableAllWeaponSpriteImg()
    {
        foreach(var imgItem in subWeaponImageObj)
        {
            imgItem.SetActive(false);
        }
    }
}