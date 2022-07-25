using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBase : MonoBehaviour, CharBase
{
    [SerializeField]
    private PlayerModule _playerModule;

    #region 캐릭터 기본 수치
    private int _hp;
    public int Hp
    {
        get => _hp;
        set { _hp = Mathf.Clamp(value, 0, _playerModule.HP); }
    }

    private float _def;
    public float Def
    {
        get => _def;
        set { _def = (value + _playerModule.def); }
    }

    private float _moveSpeed;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set { _moveSpeed = (value + _playerModule.moveSpeed); }
    }

    private bool _canAilments;
    public bool CanAilments
    {
        get => _canAilments;
        set { _canAilments = value; }
    }

    private bool _isEnemy=false;
    public bool IsEnemy
    {
        get => _isEnemy;
        set { _isEnemy = value; }
    }

    private bool _isDead;
    public bool IsDead
    {
        get => _isDead;
        set { _isDead = value; }
    }
    #endregion
    #region 적 수치
    public StatusAilments _statusAilment;
    #endregion

    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }

    private void Start()
    {
        Hp = _playerModule.HP;
        Def = _playerModule.def;
        MoveSpeed = _playerModule.moveSpeed;
    }
    public void Hit(int damage, GameObject damageDealer, StatusAilments status, float chance)
    {
        if (IsDead) return;
        Hp -= damage;
        OnGetHit?.Invoke();
        _statusAilment = status;
        if (Hp <= 0)
        {
            OnDie?.Invoke();
            Debug.Log($"플레이어 사망!");
            IsDead = true;
        }
    }
}
