using System.Collections.Generic;

[System.Serializable]
public class User
{
    public int money = 0; //���� ��
    public int level = 1; //���� ����
    public int experience = 0; //���� ����ġ
    public int[] maxExperience = { 5, 10, 20, 40, 60, 90, 120, 150, 200 }; //level - 1 �־������ ����ġ �Ҵ緮
    public float attack = 20f;                // ���ݷ�
    public float attackSpeed = 10f;           // ���ݼӵ�
    public float defense = 20f;               // ����
    public float criticalChance = 0;        // ġ��Ÿ Ȯ��
    public float criticalDamage = 50f;        // ġ��Ÿ ������
    public int maxMagazine = 30;             // �ִ� źâ
    public float speed = 10f;                 // �̵��ӵ� 
    public List<ShopItem> shopItem = new List<ShopItem>();          // ���� �� ������
    public List<DungeonItem> shopItemInDungeon = new List<DungeonItem>(); // ���� �� ������
}
