using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Module/Weapon")]
public class WeaponModule : ScriptableObject
{
    public WeaponKind kind;//��������
    public Sprite WeaponSprite;
    public float atkSpeed;//����

    public float bulletSpread;//ź����
    public int oneShotBullets;//�ѹ��� ������ �Ѿ� ��

    public bool isAutoShot;//����

    public int magazine;//źâ
    public int maxMagazine;//�ִ� źâ
    public bool isInfiniteBullet;//�ѹ� źâ
    public float reload;//������ �ð�

    public GameObject bullet;
}
