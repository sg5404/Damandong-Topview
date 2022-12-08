using System.Collections.Generic;

[System.Serializable]
public class User
{
    public int money = 0; //현재 돈
    public int level = 1; //현재 레벨
    public int experience = 0; //현재 경험치
    public int[] maxExperience = { 5, 10, 20, 40, 60, 90, 120, 150, 200 }; //level - 1 넣어줘야함 경험치 할당량
    public float attack = 20f;                // 공격력
    public float attackSpeed = 10f;           // 공격속도
    public float defense = 20f;               // 방어력
    public float criticalChance = 0;        // 치명타 확률
    public float criticalDamage = 50f;        // 치명타 데미지
    public int maxMagazine = 30;             // 최대 탄창
    public float speed = 10f;                 // 이동속도 
    public List<ShopItem> shopItem = new List<ShopItem>();          // 마을 내 아이템
    public List<DungeonItem> shopItemInDungeon = new List<DungeonItem>(); // 던전 내 아이템
}
