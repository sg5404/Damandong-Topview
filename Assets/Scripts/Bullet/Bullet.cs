using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected BulletModule _bulletModule;

    protected bool _isEnemy;
    public bool IsEnemy
    {
        get => _isEnemy;
        set
        {
            _isEnemy = value;
        }
    }

    public int damage = 1; //총알의 데미지 계수

    public virtual BulletModule BulletData
    {
        get => _bulletModule;
        set
        {
            _bulletModule = value;
            damage = _bulletModule.atk;
        }
    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }
}
