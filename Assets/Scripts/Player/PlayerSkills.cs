using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSkills : MonoSingleton<PlayerSkills>
{
    private float curDelay = 0f;
    private float madangDelay = 0f;
    private float stunDelay = 0f;

    [SerializeField]
    private Transform lookTrs;

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

    private void Update()
    {
        curDelay += Time.deltaTime;
        madangDelay += Time.deltaTime;
        stunDelay += Time.deltaTime;
    }

    public IEnumerator Lambo()
    {
        if(curDelay >= 30f)
        {
            curDelay = 0f;
            Debug.Log("LamboMode On");

            float defaultWSpd = PlayerAttack.Instance.module[PlayerAttack.Instance.rightWeapon].atkSpeed;

            // 공속증가
            PlayerAttack.Instance.module[PlayerAttack.Instance.rightWeapon].atkSpeed = 0.1f;

            yield return new WaitForSeconds(15f);

            PlayerAttack.Instance.module[PlayerAttack.Instance.rightWeapon].atkSpeed = defaultWSpd;
        }

    }

    public void MadangSslGi()
    {
        if(madangDelay >= 6f)
        {
            madangDelay = 0f;

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
        }
        //playerAttack.module[playerAttack.weapon].magazine = 8;
    }

    public IEnumerator Stun()
    {
        if(stunDelay >= 5f)
        {
            Debug.Log("Stun On");

            GameObject stun = Instantiate(stunGranade, transform.position, lookTrs.rotation);
            stun.transform.SetParent(null);
            stun.transform.DOMove(stun.transform.position + (stun.transform.right * 2.5f), 0.75f);
            yield return new WaitForSeconds(2f);
            Destroy(stun);
        }
    }

    public void None()
    {

    }
}
