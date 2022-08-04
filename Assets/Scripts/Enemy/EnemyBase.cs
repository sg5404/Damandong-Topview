using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour, CharBase
{
    [SerializeField]
    private EnemyModule _enemyModule;
    [SerializeField]
    private Image hpBarImage;

    #region 캐릭터 기본 수치
    public float MaxHp;
    public int _hp;
    public int Hp
    {
        get => _hp;
        set { _hp = Mathf.Clamp(value, 0, _enemyModule.maxHp); }
    }

    private float _def;
    public float Def
    {
        get => _def;
        set { _def = (value + _enemyModule.def); }
    }

    private float _moveSpeed;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set { _moveSpeed = (value + _enemyModule.moveSpeed); }
    }

    private bool _canAilments;
    public bool CanAilments
    {
        get => _canAilments;
        set { _canAilments = value; }
    }

    public bool _isEnemy = true;
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

    private float stunTime;
    [field:SerializeField] public UnityEvent OnDie { get; set; }
    [field:SerializeField] public UnityEvent OnGetHit { get; set; }

    Enemyflower enemy;
    private void Start()
    {
        enemy = GetComponent<Enemyflower>();
        MaxHp = Hp;
    }
    private void Update()
    {
        DurationChange();
    }
    public virtual void Hit(int damage, GameObject damageDealer, StatusAilments status, float chance)
    {
        if (IsDead) return;
        OnGetHit?.Invoke();
        if(_statusAilment==StatusAilments.None)
            _statusAilment = status;
        Hp -= damage;
        hpBarImage.fillAmount = Hp / MaxHp;
        if (Hp <= 0)
        {
            OnDie?.Invoke();
            enemy.DeadCheck(Hp);
            Debug.Log($"{gameObject.name}이 죽었음미다");
            //내가 임의로 수정함 -기현-
            gameObject.SetActive(false);
            //여기까지
            IsDead = true;
        }
    }

    public void Stun(float durationTime)
    {
        _statusAilment = StatusAilments.Stun;
        stunTime = durationTime;
        Debug.Log("으앙 스턴");
    }

    private void DurationChange()
    {
        if (stunTime > 0)
            stunTime -= Time.deltaTime;
        if (stunTime < 0)
            stunTime = 0;
        if (stunTime == 0)
            _statusAilment = StatusAilments.None;
    }
}
