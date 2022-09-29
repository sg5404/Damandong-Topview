using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : Bullet
{
    public WeaponKind weaponKind;

    private float timer = 0;
    private float finalDamage;

    public override BulletModule BulletData 
    { 
        get => _bulletModule;
        set 
        {
            base.BulletData = value;
        }
    }

    private void Start()
    {
        IsEnemy = BulletData.isEnemy;
        Destroy(gameObject, BulletData.range);
    }

    void Update()
    {
        transform.Translate(Vector3.right * _bulletModule.speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OutDoor")) Destroy(gameObject);
        if (!collision.CompareTag("Enemy")) return;
        var hit = collision.GetComponent<CharBase>();
        if (hit.IsEnemy == IsEnemy) return;
        if (Random.value < _bulletModule.crtChance)
            finalDamage = _bulletModule.atk * _bulletModule.crtDmg;
        else
            finalDamage = _bulletModule.atk;
        if(!_bulletModule.isExplosion)
        {
            hit.Hit(finalDamage, gameObject, _bulletModule.statusAilment, _bulletModule.saChance);
            SniperCheck();
            return;
        }
        EnemyHit(collision);
        SniperCheck();
    }

    private void EnemyHit(Collider2D collision)
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(collision.transform.position, _bulletModule.explosionRange / 2);
        foreach (Collider2D col in cols)
        {
            col.GetComponent<EnemyBase>()?.Hit(_bulletModule.atk, gameObject, _bulletModule.statusAilment, _bulletModule.saChance);
        }
        Destroy(gameObject);
    }

    private void SniperCheck()
    {
        bool result = weaponKind switch
        {
            WeaponKind.SNIPER => true,
            _ => false,
        };
        if (!result) Destroy(gameObject);
    }
}
