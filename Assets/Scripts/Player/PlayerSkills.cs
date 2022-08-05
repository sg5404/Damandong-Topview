using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSkills : MonoSingleton<PlayerSkills>
{
    private float curDelay = 0f;

    private PlayerBase playerData = null;
    private PlayerAttack playerAttack = null;

    private List<GameObject> enemyObject = new List<GameObject>();

    [SerializeField]
    private GameObject stunGranade;

    private Rigidbody2D rb = null;


    private void Awake()
    {
        playerData = GetComponent<PlayerBase>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    public void Lambo()
    {
        Debug.Log("LamboMode On");

        float defaultWSpd = PlayerAttack.Instance.module[PlayerAttack.Instance.rightWeapon].atkSpeed;

        while (curDelay <= 15f)
        {
            // 공속증가
            PlayerAttack.Instance.module[PlayerAttack.Instance.rightWeapon].atkSpeed *= 1.3f;
            curDelay += Time.deltaTime;
        }

        PlayerAttack.Instance.module[PlayerAttack.Instance.rightWeapon].atkSpeed = defaultWSpd;

    }

    public void MadangSslGi()
    {
        Debug.Log("MadanSslgi On");

        enemyObject = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (GameObject enemyItem in enemyObject)
        {
            if (Vector3.Distance(transform.position, enemyItem.transform.position) <= 2f)
            {
                rb = enemyItem.GetComponent<Rigidbody2D>();
                Vector3 reactVec = enemyItem.transform.position - transform.position;
                reactVec = reactVec.normalized;
                enemyItem.transform.DOMove(enemyItem.transform.position + (reactVec * 5f), 0.5f);
                Debug.Log(reactVec);
                Debug.Log("넉백");
            }

        }
        //playerAttack.module[playerAttack.weapon].magazine = 8;
    }

    public void Stun()
    {
        Debug.Log("Stun On");

        GameObject stun = Instantiate(stunGranade, transform.position, transform.rotation);
        stun.transform.SetParent(null);
        stun.transform.DOMove(stun.transform.position + (stun.transform.right * 2.5f), 0.75f);
    }

    public void None()
    {

    }
}
