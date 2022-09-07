using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : Bullet
{

    private float timer = 0;

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
        if (!collision.CompareTag("Enemy")&&!collision.CompareTag("Player")) return;
        var hit = collision.GetComponent<CharBase>();
        if (hit.IsEnemy == IsEnemy) return;
        if(_bulletModule.isExplosion)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(collision.transform.position, _bulletModule.explosionRange/2);
            foreach(Collider2D col in cols)
            {
                col.GetComponent<EnemyBase>()?.Hit(_bulletModule.atk, gameObject, _bulletModule.statusAilment, _bulletModule.saChance);
            }
            Destroy(gameObject);
        }
        else
            hit.Hit(_bulletModule.atk, gameObject, _bulletModule.statusAilment, _bulletModule.saChance);
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
