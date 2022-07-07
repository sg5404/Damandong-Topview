using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum flwerEnemyType
{
    sniper,
    rifle,
    shotgun
}

public class Enemyflower : MonoBehaviour
{
    public flwerEnemyType flowertype;
    private EnemyBase _enemyBase;
    [SerializeField]
    private EnemyModule stat;
    private int hp;

    [SerializeField]
    private int shotgunBullet;

    private int bulletAmount;

    [SerializeField]
    private float tanpegim;

    private float addfloat;

    [SerializeField]
    private int maxBullets;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float createTime = 1.0f;

    [SerializeField]
    private GameObject bulletPoolObject;

    public List<GameObject> bulletPool = new List<GameObject>();

    Vector2 targetDir;

    void Start()
    {
        hp = stat.maxHp;
        CreateBulletPool();
        _enemyBase = GetComponent<EnemyBase>();
        InvokeRepeating("CreateBullet", 2.0f, stat.shootSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateBullet()
    {
        if (_enemyBase._statusAilment == StatusAilments.Stun) return;
        GameObject _bullet = GetBulletinPool();
        targetDir = (GameManager.GetInstance().Playertransform.position - transform.position);
        //Debug.DrawRay(gameObject.transform.position, targetDir*100, Color.green,10);
        switch(flowertype)
        {
            case flwerEnemyType.rifle:
                addfloat = Random.Range(-tanpegim, tanpegim);
                bulletAmount = 1;
                break;
            case flwerEnemyType.sniper:
                addfloat = 0;
                bulletAmount = 1;
                break;
            case flwerEnemyType.shotgun:
                bulletAmount = shotgunBullet;
                break;
            default:
                break;
        }

        for(int i = 0; i < bulletAmount; i++)
        {
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg + addfloat;
            Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            _bullet?.transform.SetPositionAndRotation(this.gameObject.transform.position, angleAxis);
            _bullet.SetActive(true);
            //_bullet.transform.SetParent(null);
        }
    }

    void CreateBulletPool()
    {
        for (int i = 0; i < maxBullets; ++i)
        {
            var _bullet = Instantiate<GameObject>(bullet);

            _bullet.name = $"Bullet_{i:00}";

            _bullet.SetActive(false);

            bulletPool.Add(_bullet);

            _bullet.transform.SetParent(bulletPoolObject.transform);
        }
    }

    public GameObject GetBulletinPool()
    {
        foreach (var _monster in bulletPool)
        {
            if (_monster.activeSelf == false)
            {
                return _monster;
            }
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            /*BulletModule bulletHit = collision.GetComponent<BulletMove>().bulletStat;
            float crit = Random.value;
            //Debug.Log(crit);
            hp -= bulletHit.atk;
            DeadCheck();
            if (crit<bulletHit.crtChance)
            {
                hp -= bulletHit.atk;
                DeadCheck();
                return;
            }
            Destroy(collision.gameObject);*/
        }
    }

    private void DeadCheck()
    {
        if (hp <= 0)
        {
            CancelInvoke("CreateBullet");
            gameObject.SetActive(false);
        }
    }
}
