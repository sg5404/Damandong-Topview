using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour, CharBase
{
    [SerializeField] private EnemyModule _enemyModule;
    [SerializeField] private Image hpBarImage;

    #region ĳ���� �⺻ ��ġ
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
    #region �� ��ġ
    public StatusAilments _statusAilment;
    #endregion
    Animator ani;
    private float crowdTime;
    //private float stunTime;
    //private float burnTime;
    //private float slowTime;
    [field:SerializeField] public UnityEvent OnDie { get; set; }
    [field:SerializeField] public UnityEvent OnGetHit { get; set; }

    EnemyScript enemy;
    private float timer = 0;
   
    private void Start()
    {
        enemy = GetComponent<EnemyScript>();
        ani = GetComponent<Animator>();
        MaxHp = Hp;
        StartCoroutine(CrowdControlDuration());
    }
    private void Update()
    {
        //DurationChange();
        //BurnDuration();
        //SlowDuration();
        DeadCheck();
    }
    public virtual void Hit(float damage, GameObject damageDealer, StatusAilments status, float chance)
    {
        if (IsDead) return;
        OnGetHit?.Invoke();
        ani.SetTrigger("Hit");
        if(_statusAilment==StatusAilments.None) _statusAilment = status;
        CheckStatus(status);
        HpBar(damage);
        if (Hp > 0) return;
        ani.SetTrigger("Die");
        OnDie?.Invoke();
        //Debug.Log($"{gameObject.name}�� �׾����̴�");
        PlayerMoney.Instance.ChangeMoney(Random.Range(1, 4) * PlayerController.Instance.addMoney);
        PlayerExperience.Instance.ChangeExperience(_enemyModule.exp * PlayerController.Instance.addExp); //����ġ �� �ø��� ���� ���� ���س���
        //���̾� �ٲ��ֱ�
        gameObject.tag = "Untagged";
        IsDead = true;
    }

    void CheckStatus(StatusAilments status)
    {
        if(status == StatusAilments.Burn)
        {
            Burn(3f);
        }
        if (status == StatusAilments.Slow)
        {
            Slow(3f);
        }

    }

    public void HpBar(float damage)
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
        crowdTime = durationTime;
        Debug.Log("����");
    }

    public void Burn(float burnTime)
    {
        _statusAilment = StatusAilments.Burn;
        Debug.Log(_statusAilment);
        this.crowdTime = burnTime;
        Debug.Log("Burn");
    }

    public void Slow(float slowTime)
    {
        _statusAilment = StatusAilments.Slow;
        this.crowdTime = slowTime;
        Debug.Log("Slow");
    }

    private IEnumerator CrowdControlDuration()
    {
        while (true)
        {
            if (crowdTime > 0)
                crowdTime -= Time.deltaTime;
            if (crowdTime < 0)
                crowdTime = 0;
            if (crowdTime == 0)
                _statusAilment = StatusAilments.None;
            yield return new WaitForEndOfFrame();
        }
    }

    //private void BurnDuration()
    //{
    //    if (burnTime > 0)
    //        burnTime -= Time.deltaTime;
    //    if (burnTime < 0)
    //        burnTime = 0;
    //    if (burnTime == 0)
    //        _statusAilment = StatusAilments.None;
    //}

    //private void SlowDuration()
    //{
    //    if (slowTime > 0)
    //        slowTime -= Time.deltaTime;
    //    if (slowTime < 0)
    //        slowTime = 0;
    //    //if (slowTime == 0)
    //    //    _statusAilment = StatusAilments.None;
    //}

    //private void DurationChange()
    //{
    //    if (stunTime > 0)
    //        stunTime -= Time.deltaTime;
    //    if (stunTime < 0)
    //        stunTime = 0;
    //    //if (stunTime == 0)
    //    //    _statusAilment = StatusAilments.None;
    //}
}
