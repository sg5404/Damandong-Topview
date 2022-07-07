using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGranade : Bullet
{
    private float bangTime = 0;
    [SerializeField]
    private float exploseTime;
    private CircleCollider2D circle;
    private List<EnemyBase> inside = new List<EnemyBase>();

    private void OnEnable()
    {
        IsEnemy = false;
        circle = GetComponent<CircleCollider2D>();
        StartCoroutine(Bang());
    }

    private IEnumerator Bang()
    {
        while(bangTime<exploseTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            bangTime += Time.deltaTime;
            circle.radius += _bulletModule.explosionRange / exploseTime * Time.deltaTime;
        }
        Debug.Log("Æã");
        if (inside.Count <= 0) yield break;
        foreach (EnemyBase enemy in inside)
        {
            enemy.Stun();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        inside.Add(collision.gameObject.GetComponent<EnemyBase>());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        inside.Remove(collision.gameObject.GetComponent<EnemyBase>());
    }
}
