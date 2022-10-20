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
    
    private static float baseCriticalDamage = 1;
    private static float addCriticalDamage = 0;
    
    private static float baseMoveSpeed = 0;
    private static float addMoveSpeed = 0;
    
    private static float baseAttackSpeed = 0;
    private static float addAttackSpeed = 0;

    private static float[] reloadTime = { };
    private static float timeDecreasePer = 0.0f;

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

    public static int GetHP()
    {
        return Mathf.RoundToInt((baseHP + addHP) * perHP);
    }

    public static int GetAmmo()
    {
        return (baseAmmo + addAmmo);
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

    public static void UpAddAtk()
    {
        addAtk += 5;
    }

    public static void UpPerAtk()
    {
        perAtk += 0.01f;
    }

    public static void UpAddHP()
    {
        addHP += 10;
    }

    public static void UpPerHP()
    {
        perHP += 0.01f;
    }

    public static void UpMoveSpeed()
    {
        addMoveSpeed += 0.3f;
    }

    public static void UpCriticalChance()
    {
        addCriticalChance += 0.025f;
    }

    public static void UpCriticalDamage()
    {
        addCriticalDamage += 0.075f;
    }
}
