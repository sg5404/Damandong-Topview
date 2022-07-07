using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Module/Bullet")]
public class BulletModule : ScriptableObject
{
    public int atk;//공격력

    public float range;//사거리
    public float speed;//속도

    public float crtDmg;//치뎀
    public float crtChance;//치확

    public StatusAilments statusAilment;//발동되는 상태이상
    public float saChance;//상태이상확률
    public bool isExplosion;//광역
    public float explosionRange;//광역범위
    public bool isKnockBack;//넉백
    public float knockBackRange;//넉백범위
}
