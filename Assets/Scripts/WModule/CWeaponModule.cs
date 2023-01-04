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
    }
}
