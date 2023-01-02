using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Module/Bullet")]
public class BulletModule : ScriptableObject
{
    public int atk;//���ݷ�

    public bool isEnemy;//���� ��� �Ѿ��ΰ�

    public float range;//��Ÿ�
    public float speed;//�ӵ�

    public float crtDmg;//ġ��
    public float crtChance;//ġȮ

    public StatusAilments statusAilment;//�ߵ��Ǵ� �����̻�
    public float saChance;//�����̻�Ȯ��
    public bool isExplosion;//����
    public float explosionRange;//��������
    public bool isKnockBack;//�˹�
    public float knockBackRange;//�˹����

    // ��ȭ
    public bool isFlameBullet; // ȭ��ź - ����
    public bool isSlowBullet; // ����ź - ������
    public bool isBigBullet; // ����ź - ��������
    public bool isUpgrade = false; //���׷��̵� �Ǿ��°�?
}
