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
    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.right * _bulletModule.speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) return;
        var hit = collision.GetComponent<CharBase>();
        //if (hit.IsEnemy == IsEnemy) return;
        hit.Hit(_bulletModule.atk, gameObject, _bulletModule.statusAilment, _bulletModule.saChance);
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
