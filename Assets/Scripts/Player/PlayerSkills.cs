using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class PlayerSkills : MonoSingleton<PlayerSkills>
{
    [SerializeField] private float rifle_SkillDelay;
    [SerializeField] private float sniper_SkillDelay = 0f;
    [SerializeField] private float shotgun_SkillDelay;
    [SerializeField] private float granade_SkillDelay;


    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] TextMeshProUGUI tmp2;
    public WeaponSet weaponSet = null;

    private float rifleDelay = 0f;
    private float sniperDelay = 0f;
    private float madangDelay = 0f;
    private float stunDelay = 0f;

    [SerializeField]
    private Transform lookTrs;

    private List<GameObject> enemyObject = new List<GameObject>();

    [SerializeField]
    private GameObject stunGranade;

    private Rigidbody2D rb = null;

    private void Start()
    {
        weaponSet = GetComponent<WeaponSet>();

    }

    private void Update()
    {
        SkillDelayKind();
        rifleDelay += Time.deltaTime;
        madangDelay += Time.deltaTime;
        stunDelay += Time.deltaTime;
    }

    //void SkillDelayReceive()
    //{

    //}

    void SkillDelayKind()
    {
        var coolTime = weaponSet.SubWeaponState switch
        {
            WeaponKind.RIFLE => rifle_SkillDelay,
            WeaponKind.SNIPER => sniper_SkillDelay,
            WeaponKind.SHOTGUN => shotgun_SkillDelay,
            WeaponKind.GRANADE => granade_SkillDelay,
        };

        var delay = weaponSet.SubWeaponState switch
        {
            WeaponKind.RIFLE => rifleDelay,
            WeaponKind.SNIPER => sniperDelay,
            WeaponKind.SHOTGUN => madangDelay,
            WeaponKind.GRANADE => stunDelay,
        };

        //Debug.Log(weaponSet.MainWeaponState.ToString());
        CoolTimeDisplay(coolTime, delay);
    }

    public IEnumerator Lambo()
    {
        if (rifleDelay >= rifle_SkillDelay)
        {
            rifleDelay = 0f;
            Debug.Log("LamboMode On");

            float defaultWSpd = PlayerController.Instance.module[0].atkSpeed;

            // ��������
            PlayerController.Instance.module[0].atkSpeed = defaultWSpd * 0.5f;

            //����źâ
            PlayerController.Instance.infinityBullet = true;

            yield return new WaitForSeconds(15f);

            PlayerController.Instance.infinityBullet = false;

            PlayerController.Instance.module[0].atkSpeed = defaultWSpd;
        }

    }

    public void MadangSslGi()
    {
        if (madangDelay >= shotgun_SkillDelay)
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
                    Debug.Log("�˹�");
                }

            }
        }
        //playerAttack.module[playerAttack.weapon].magazine = 8;
    }

    public IEnumerator Stun()
    {
        if (stunDelay >= granade_SkillDelay)
        {
            stunDelay = 0f;
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

    void CoolTimeDisplay(float coolTime, float curTime)
    {
        if (curTime > coolTime)
        {
            curTime = coolTime;
        }

        float percent = curTime / coolTime;
        tmp.text = ((int)(coolTime - curTime)).ToString();

        if (percent > 0.99f || coolTime == 0f)
        {
            image.gameObject.SetActive(false);
            tmp.gameObject.SetActive(false);
            tmp2.gameObject.SetActive(true);
            return;
        }

        image.gameObject.SetActive(true);
        tmp.gameObject.SetActive(true);
        tmp2.gameObject.SetActive(false);
        image.fillAmount = 1 - percent;
    }

    private void OnApplicationQuit()
    {
        PlayerController.Instance.module[0].atkSpeed = 0.15f;
    }
}
