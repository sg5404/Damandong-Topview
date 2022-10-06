using UnityEngine;

public static class PlayerStat
{
    public static int baseHP;
    public static int addHP;
    public static float perHP;

    public static int baseAmmo;
    public static int addAmmo;

    public static float baseAtk;
    public static float addAtk;
    public static float perAtk;

    public static float baseCriticalChance;
    public static float addCriticalChance;

    public static float baseCriticalDamage;
    public static float addCriticalDamage;

    public static float baseMoveSpeed;
    public static float addMoveSpeed;

    public static float baseAttackSpeed;
    public static float addAttackSpeed;

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
