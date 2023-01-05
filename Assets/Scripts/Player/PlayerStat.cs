using System.Collections.Generic;
using UnityEngine;

public static class PlayerStat
{
    private static int baseHP = 0;
    private static int addHP = 0;
    private static float perHP = 1;
    
    private static int baseAmmo = 0;
    private static int addAmmo = 0;
    
    private static float baseAtk = 0;
    private static float addAtk = 0;
    private static float perAtk = 1;
    
    private static float baseCriticalChance = 0;
    private static float addCriticalChance = 0;
    
    private static float baseCriticalDamage = 0;
    private static float addCriticalDamage = 0;
    
    private static float baseMoveSpeed = 0;
    private static float addMoveSpeed = 0;
    
    private static float baseAttackSpeed = 0;
    private static float addAttackSpeed = 0;

    private static float[] reloadTime = { };
    private static float timeDecreasePer = 0.0f;

    private static float mulGold = 1;
    private static float mulExp = 1;


    public static void SetBase(int hp, int ammo, float atk, float criChance, float criDamage, float moveSpeed, float attackSpeed)
    {
        baseHP = hp;
        baseAmmo = ammo;
        baseAtk = atk;
        baseCriticalChance = criChance;
        baseCriticalDamage = criDamage;
        baseMoveSpeed = moveSpeed;
        baseAttackSpeed = attackSpeed;
    }

    public static void LoadStat()
    {
        List<ShopItem> items = SaveManager.Instance.CurrentUser.shopItem;
        addAtk = items[0].upgradeValue;
        perAtk = items[1].upgradeValue + 1;
        addHP = (int)items[2].upgradeValue;
        perHP = items[3].upgradeValue + 1;
        addAmmo = (int)items[4].upgradeValue;
        addMoveSpeed = items[5].upgradeValue;
        addCriticalChance = items[6].upgradeValue;
        addCriticalDamage = items[7].upgradeValue;
    }

    public static int GetHP()
    {
        return Mathf.RoundToInt((baseHP + addHP) * perHP);
    }

    public static int GetAddAmmo()
    {
        return addAmmo;
    }

    public static float GetAtk()
    {
        return (baseAtk + addAtk) * perAtk;
    }

    public static float GetCriticalChance()
    {
        return (baseCriticalChance + addCriticalChance);
    }

    public static float GetCriticalDamage()
    {
        return (baseCriticalDamage + addCriticalDamage);
    }
    public static float GetMoveSpeed()
    {
        return (baseMoveSpeed + addMoveSpeed);
    }
    public static float GetAttackSpeed()
    {
        return (baseAttackSpeed + addAttackSpeed);
    }

    public static void AddAtk(int stat, int per)
    {
        addAtk += stat;
        perAtk += per / 100;
    }

    public static float[] GetReloadTime()
    {
        float[] c_reloadTime = { };
        for(int i = 0; i < reloadTime.Length; i++)
            c_reloadTime[i] = reloadTime[i] - (reloadTime[i] * timeDecreasePer / 100); 
        return c_reloadTime;
    }

    public static void upgradeReloadTime(int num)
    {
        timeDecreasePer += num;
    }

    public static float GetMulGold()
    {
        return mulGold;
    }
    public static float GetMulExp()
    {
        return mulExp;
    }

    public static void ResetMul()
    {
        mulGold = 1;
        mulExp = 1;
    }

    public static void UpgradeGold()
    {
        mulGold += 0.1f;
    }
    public static void UpgradeExp()
    {
        mulExp += 0.1f;
    }
}
