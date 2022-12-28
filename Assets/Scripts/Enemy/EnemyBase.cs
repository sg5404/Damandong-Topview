using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour, CharBase
{
    [SerializeField] private EnemyModule _enemyModule;
    [SerializeField] private Image hpBarImage;

    #region 캐릭터 기본 수치
    public float MaxHp;
    public int _hp;
    public int Hp
    {
        get => _hp;
        set { _hp = Mathf.Clamp(value, 0, _enemyModule.maxHp); }
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
    Animator ani;
    private float stunTime;
    private float burnTime;
    [field:SerializeField] public UnityEvent OnDie { get; set; }
    [field:SerializeField] public UnityEvent OnGetHit { get; set; }

    EnemyScript enemy;
    private float timer = 0;
    private void Start()
    {
        enemy = GetComponent<EnemyScript>();
        ani = GetComponent<Animator>();
        MaxHp = Hp;
    }
    private void Update()
    {
        DurationChange();
        DeadCheck();
    }
    public virtual void Hit(float damage, GameObject damageDealer, StatusAilments status, float chance)
    {
        if (IsDead) return;
        OnGetHit?.Invoke();
        ani.SetTrigger("Hit");
        if(_statusAilment==StatusAilments.None) _statusAilment = status;
        HpBar(damage);
        if (Hp > 0) return;
        ani.SetTrigger("Die");
        OnDie?.Invoke();
        //Debug.Log($"{gameObject.name}이 죽었음미다");
        PlayerMoney.Instance.ChangeMoney(Random.Range(1, 4));
        PlayerExperience.Instance.ChangeExperience(_enemyModule.exp); //경험치 얼마 올릴지 몰라서 대충 정해놓음
        //레이어 바꿔주기
        gameObject.tag = "Untagged";
        IsDead = true;
    }

    private void HpBar(float damage)
    {
        Hp -= (int)damage;
        hpBarImage.fillAmount = Hp / MaxHp;
    }

    private void DeadCheck()
    {
        if (!IsDead) return;
        timer += Time.deltaTime;
        if (timer < 2.0f) return;
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsDead) return;
        if (!collision.CompareTag("Player")) return;
        var hit = collision.GetComponent<CharBase>();
        hit.Hit(10, gameObject, StatusAilments.None, 0);
    }

    public void Stun(float durationTime)
    {
        _statusAilment = StatusAilments.Stun;
        stunTime = durationTime;
        Debug.Log("스턴");
    }

    public void Burn(float burnTime)
    {
        _statusAilment = StatusAilments.Burn;
        this.burnTime = burnTime;
        Debug.Log("Burn");
    }

    private void BurnDuration(float burnTime)
    {
        while(burnTime > 0)
        {
            burnTime -= Time.deltaTime;
        }

        _statusAilment = StatusAilments.None;
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
