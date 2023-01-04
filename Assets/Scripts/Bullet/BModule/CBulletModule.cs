using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Module/CBullet")]
public class CBulletModule : BulletModule
{
    [SerializeField]
    BulletModule baseSO;

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        atk = baseSO.atk;
        isEnemy = baseSO.isEnemy;
        range = baseSO.range;
        speed = baseSO.speed;
        crtDmg = baseSO.crtDmg;
        crtChance = baseSO.crtChance;
        statusAilment = baseSO.statusAilment;
        saChance = baseSO.saChance;
        isExplosion = baseSO.isExplosion;
        explosionRange = baseSO.explosionRange;
        isKnockBack = baseSO.isKnockBack;
        knockBackRange = baseSO.knockBackRange;
        isFlameBullet = baseSO.isFlameBullet;
        isSlowBullet = baseSO.isSlowBullet;
        isBigBullet = baseSO.isBigBullet;
        isUpgrade = baseSO.isUpgrade;
    }

    public void UpgradeIron()
    {
        atk += 20;
        range *= 2;
    }

    public void UpgradeHollowPoint()
    {
        statusAilment = StatusAilments.Slow;
        saChance = 1;
    }

    public void UpgradeDragonBreath()
    {
        isExplosion = true;
        explosionRange = 0.2f;
    }
}
