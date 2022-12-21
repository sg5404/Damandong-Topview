using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Module/CWeapon")]
public class CurrentWeaponModule : WeaponModule
{
    [SerializeField]
    WeaponModule baseSO;

    private void OnEnable()
    {
        atkSpeed = baseSO.atkSpeed;
        WeaponSprite = baseSO.WeaponSprite;
        bulletSpread = baseSO.bulletSpread;
        oneShotBullets = baseSO.oneShotBullets;
        magazine = baseSO.magazine;
        maxMagazine = baseSO.maxMagazine;
        reload = baseSO.reload;
        bullet = baseSO.bullet;
    }

    public void Upgrade()
    {
        atkSpeed = baseSO.atkSpeed*0.85f;
        WeaponSprite = baseSO.WeaponSprite;
        bulletSpread = baseSO.bulletSpread*0.85f;
        magazine = baseSO.magazine+1;
        maxMagazine = baseSO.maxMagazine+1;
        reload = baseSO.reload*0.85f;
    }
}
