using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Module/CWeapon")]
public class CWeaponModule : WeaponModule
{
    [SerializeField]
    WeaponModule baseSO;

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        kind = baseSO.kind;
        WeaponSprite = baseSO.WeaponSprite;
        atkSpeed = baseSO.atkSpeed;
        bulletSpread = baseSO.bulletSpread;
        oneShotBullets = baseSO.oneShotBullets;
        isAutoShot = baseSO.isAutoShot;
        magazine = baseSO.magazine;
        maxMagazine = baseSO.maxMagazine;
        isInfiniteBullet = baseSO.isInfiniteBullet;
        reload = baseSO.reload;
        bullet = baseSO.bullet;
        bulletModule = baseSO.bulletModule;
        isUpgrade = baseSO.isUpgrade;
    }

    public void Upgrade(int num)
    {
        isUpgrade = true;
        switch(num)
        {
            case 0:
                bulletModule.isSlowBullet = true;
                break;
            case 1:
                bulletModule.isBigBullet = true;
                break;
            case 2:
                bulletModule.isFlameBullet = true;
                break;
            case 3:
                oneShotBullets = 3;
                bulletSpread = 0;
                break;
        }
    }
}
