using System.Collections.Generic;

[System.Serializable]
public class User
{
    public int money = 0;
    public float attack = 20f;                // 공격력
    public float attackSpeed = 10f;           // 공격속도
    public float defense = 20f;               // 방어력
    public float criticalChance = 0;        // 치명타 확률
    public float criticalDamage = 50f;        // 치명타 데미지
    public int maxMagazine = 30;             // 최대 탄창
    public float speed = 10f;                 // 이동속도 
}
