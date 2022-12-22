using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private EnemyBase _enemyBase;
    [SerializeField] private EnemyModule stat;
    private int hp;

    [SerializeField] private float atkdistance;
    [SerializeField] private Transform distanceShow;

    Vector2 targetDir;
    Vector2 dir;

    private Rigidbody2D rigid;

    [SerializeField] private float speed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        hp = stat.maxHp;
        _enemyBase = GetComponent<EnemyBase>();
        distanceShow.localScale = new Vector3(atkdistance * 2, atkdistance * 2, 0);

        StartCoroutine(CheckCurrentState());
    }

    private void Update()
    {
        Enemymove();
        turnEnemy();

        //Debug.Log(dir);
    }

    void Enemymove()
    {
        if (_enemyBase._statusAilment == StatusAilments.Stun || _enemyBase.IsDead)
        {
            rigid.velocity = new Vector3(0, 0);
            return;
        }
        targetDir = (GameManager.Instance.Playertransform.position - transform.position);
        dir = targetDir.normalized;

        if(Vector2.Distance(GameManager.Instance.Playertransform.position, transform.position) < 0.1f)
        {
            return;
        }

        rigid.velocity = dir * speed;
    }

    public IEnumerator CheckCurrentState()
    {
        while (true)
        {
            if(_enemyBase._statusAilment == StatusAilments.None)
            {
                continue;
            }
            else if (_enemyBase._statusAilment == StatusAilments.Burn)
            {
                hp -= 1;
            }
            else if(_enemyBase._statusAilment == StatusAilments.Slow)
            {
                
            }


            yield return new WaitForSeconds(1f);
        }
    }

    void turnEnemy()
    {
        if (dir.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            return;
        }
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        return;
    }
}