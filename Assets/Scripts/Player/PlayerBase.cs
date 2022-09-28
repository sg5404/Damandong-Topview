using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBase : MonoSingleton<PlayerBase>, CharBase
{
    [SerializeField] private PlayerModule _playerModule;
    [SerializeField] private float enableTimer = 0.0f;

    private Animator animator;

    #region ĳ���� �⺻ ��ġ
    private int _hp;
    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if (_hp < 0)
            {
                _hp = 0;
            }
            Debug.Log(_hp);
            Debug.Log(_maxHp);
            UIManager.Instance.playerHpTmp.text = $"{_hp}/{_maxHp}";
            UIManager.Instance.plyerHpSlider.value = (_hp / (float)_maxHp);
        }
    }

    private int _maxHp;
    public int MaxHP { get { return _maxHp; } }


    private float _def;
    public float Def
    {
        get => _def;
        set { _def = (value + _playerModule.def); }
    }


    //private int _mainMagazine;

    //public int MainMagazine
    //{
    //    get
    //    {
    //        return _mainMagazine;
    //    }
    //    set
    //    {
    //        _mainMagazine = value;
    //        if (_mainMagazine < 0)
    //        {
    //            _mainMagazine = 0;
    //        }
    //        UIManager.Instance.main_magazineQuantity.text = $"{_mainMagazine}/{_mainMaxMagazine}";
    //    }
    //}

    //private int _mainMaxMagazine;

    //public int MainMaxMagazine 
    //{ 
    //    get 
    //    {
    //        return _mainMaxMagazine; 
    //    }
    //    set
    //    {
    //        _mainMaxMagazine = value;
    //        UIManager.Instance.main_magazineQuantity.text = $"{_mainMagazine}/{_mainMaxMagazine}";
    //    }
    //}

    //private int _subMagazine;

    //public int SubMagazine
    //{
    //    get
    //    {
    //        return _subMagazine;
    //    }
    //    set
    //    {
    //        _subMagazine = value;
    //        if (_subMagazine < 0)
    //        {
    //            _subMagazine = 0;
    //        }
    //        UIManager.Instance.sub_magazineQuantity.text = $"{_subMagazine}/{_subMaxMagazine}";
    //    }
    //}

    //private int _subMaxMagazine;

    //public int SubMaxMagazine
    //{
    //    get
    //    {
    //        return _subMaxMagazine;
    //    }
    //    set
    //    {
    //        _subMaxMagazine = value;
    //        UIManager.Instance.sub_magazineQuantity.text = $"{_subMagazine}/{_subMaxMagazine}";
    //    }
    //}

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
    #region �� ��ġ
    public StatusAilments _statusAilment;
    #endregion

    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        _maxHp = _playerModule.HP;
        Hp = _maxHp;
        Def = _playerModule.def;
        MoveSpeed = _playerModule.moveSpeed;
        //_mainMagazine = _mainMaxMagazine;
        //_subMagazine = _subMaxMagazine;
    }
    private void Update()
    {
        enableTimer -= Time.deltaTime;
    }
    public void Hit(int damage, GameObject damageDealer, StatusAilments status, float chance)
    {
        if(enableTimer > 0.0f) return;
        if (IsDead) return;
        HitEvent(damage, status);
        if (Die()) return;
        animator.SetTrigger("Hit");
        enableTimer = 1.2f;
    }

    private void HitEvent(int damage, StatusAilments status)
    {
        PlayerManager.Instance.Damaged(damage);
        OnGetHit?.Invoke();
        _statusAilment = status;
    }

    private bool Die()
    {
        if (Hp > 0)
        {
            return false;
        }
        OnDie?.Invoke();
        //PlayerCtrl.Instance.isDead = true;
        IsDead = true;
        animator.SetTrigger("Die");
        return true;
    }
}
