using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField]
    private Collider2D rangeCol;
    private Collider2D[] hitCollider = new Collider2D[50];

    private float finalDamage;

    [SerializeField]
    BulletModule _swordData;

    public void SwordSwing()
    {
        hitCollider.Initialize();
        rangeCol.OverlapCollider(new ContactFilter2D(), hitCollider);
        finalDamage = _swordData.atk + PlayerStat.GetAtk();
        if (Random.value < _swordData.crtChance + PlayerStat.GetCriticalChance())
            finalDamage *= _swordData.crtDmg + PlayerStat.GetCriticalDamage();
        foreach(Collider2D col in hitCollider)
        {
            Debug.Log(col.name);
            if (col == null)
                break;
            col.GetComponent<EnemyBase>()?.Hit(finalDamage, gameObject, _swordData.statusAilment, _swordData.saChance);
        }
    }
}
