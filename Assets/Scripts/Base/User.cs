using System.Collections.Generic;

[System.Serializable]
public class User
{
    public int money;
    public float attack;                // 공격력
    public float attackSpeed;           // 공격속도
    public float defense;               // 방어력
    public float criticalChance;        // 치명타 확률
    public float criticalDamage;        // 치명타 데미지
    public int maxMagazine;             // 최대 탄창
}
