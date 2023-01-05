using System.Collections.Generic;

[System.Serializable]
public class User
{
    public float money = 0; //���� ��
    public float experience = 0; //���� ����ġ
    public float attack = 20f;                // ���ݷ�
    public float attackSpeed = 10f;           // ���ݼӵ�
    public float defense = 20f;               // ����
    public float criticalChance = 0;        // ġ��Ÿ Ȯ��
    public float criticalDamage = 50f;        // ġ��Ÿ ������
    public int maxMagazine = 30;             // �ִ� źâ
    public float speed = 10f;                 // �̵��ӵ� 
    public List<ShopItem> shopItem = new List<ShopItem>();          // ���� �� ������
    public List<DungeonItem> shopItemInDungeonOne = new List<DungeonItem>(); // ���� �� ������
    public List<DungeonItem> shopItemInDungeonMul = new List<DungeonItem>(); // ���� �� ������
    public List<DungeonItem> _CshopItemInDungeonOne = new List<DungeonItem>(); // ���� �� ������
    public List<DungeonItem> _CshopItemInDungeonMul = new List<DungeonItem>(); // ���� �� ������
}
