using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletMove
{
    public override BulletModule BulletData
    {
        get => _bulletModule;
        set
        {
            base.BulletData = value;
        }
    }

    private void Awake()
    {
        IsEnemy = true;   
    }
}
