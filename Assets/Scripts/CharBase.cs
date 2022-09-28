using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public enum StatusAilments
{
    None,
    Stun,
    Slow,
    Burn
}

public enum WeaponKind
{
    RIFLE = 1,
    SNIPER,
    SHOTGUN,
    GRANADE,
    SWORD
}

public interface CharBase
{
    public int Hp { get; }
    public float Def { get; }
    public float MoveSpeed { get; }
    public bool CanAilments { get; }//�����̻��� �ɸ��� �ֳ�?
    public bool IsEnemy { get; }//�갡 ���ΰ�?
    public bool IsDead { get; }//����?

    public void Hit(int damage, GameObject damageDealer, StatusAilments status, float chance);
    
    public UnityEvent OnDie { get; set; }
    public UnityEvent OnGetHit { get; set; }
}
