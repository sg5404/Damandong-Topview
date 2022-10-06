using UnityEngine;

public static class PlayerStat
{
    public static int baseHP = 0;
    public static int addHP = 0;
    public static float perHP = 1;

    public static int baseAmmo = 0;
    public static int addAmmo = 0;

    public static float baseAtk = 0;
    public static float addAtk = 0;
    public static float perAtk = 1;

    public static float baseCriticalChance = 0;
    public static float addCriticalChance = 0;

    public static float baseCriticalDamage = 0;
    public static float addCriticalDamage = 0;

    public static float baseMoveSpeed = 0;
    public static float addMoveSpeed = 0;

    public static float baseAttackSpeed = 0;
    public static float addAttackSpeed = 0;

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
}
