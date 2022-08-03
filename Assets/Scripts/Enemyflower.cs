using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum flowerEnemyType
{
    sniper = 0,
    rifle = 1,
    shotgun = 2
}

public class Enemyflower : MonoBehaviour
{
    public flowerEnemyType flowertype;
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
    private GameObject bulletPoolObject;

    private List<GameObject> bulletPool = new List<GameObject>();

    public float reloadTime;
    public float bulletSpeed;
    public List<float> weaponreloadTime = new List<float>();
    public List<float> weponbulletSpeed = new List<float>();

    private List<FlowerBullet> flowerBullets = new List<FlowerBullet>();

    [SerializeField] private float atkdistance;
    [SerializeField] private Transform distanceShow;

    Vector2 targetDir;

    void Start()
    {

        hp = stat.maxHp;
        CreateBulletPool();
        _enemyBase = GetComponent<EnemyBase>();
        StatSet();
        InvokeRepeating("CreateBullet", 2.0f, reloadTime);
        distanceShow.localScale = new Vector3(atkdistance * 2, atkdistance * 2, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StatSet()
    {
        reloadTime = weaponreloadTime[(int)flowertype];
        bulletSpeed = weponbulletSpeed[(int)flowertype];
        //flowerBullet.speed = bulletSpeed;

        for (int i = 0; i < 100; i++)
        {
            flowerBullets.Add(bulletPool[i].GetComponent<FlowerBullet>());
            flowerBullets[i].speed = bulletSpeed;
        }
    }

    void CreateBullet()
    {
        if (_enemyBase._statusAilment == StatusAilments.Stun) return;
        GameObject _bullet = GetBulletinPool();
        targetDir = (GameManager.Instance.Playertransform.position - transform.position);
        //Debug.DrawRay(gameObject.transform.position, targetDir*100, Color.green,10);

        WeaponInstance();

        if (atkdistance > GetDistance())
        {
            Shooting(_bullet);
        }
    }

    void WeaponInstance()
    {
        switch (flowertype)
        {
            case flowerEnemyType.rifle:
                addfloat = Random.Range(-tanpegim, tanpegim);
                bulletAmount = 1;
                break;
            case flowerEnemyType.sniper:
                addfloat = 0;
                bulletAmount = 1;
                break;
            case flowerEnemyType.shotgun:
                bulletAmount = shotgunBullet;
                break;
            default:
                break;
        }
    }

    void Shooting(GameObject _bullet)
    {
        for (int i = 0; i < bulletAmount; i++)
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

    private void OnTriggerEnter2D(Collider2D collision) //상현이가 고쳐야함 크리티컬? 만 어케하면 될듯 총 멈추는건 다른데서 해결함
    {
        if (collision.CompareTag("Bullet"))
        {
            /*BulletModule bulletHit = collision.gameObject.GetComponent<BulletMove>().BulletData;
            float crit = Random.value;
            //Debug.Log(crit);
            hp -= bulletHit.atk;
            if (crit<bulletHit.crtChance)
            {
                hp -= bulletHit.atk;
            }
            DeadCheck();*/
            //Destroy(collision.gameObject);
        }
    }

    public void DeadCheck(int _hp)
    {
        if (_hp <= 0)
        {
            CancelInvoke("CreateBullet");
            gameObject.SetActive(false);
        }
    }

    private float GetDistance()
    {
        return Vector2.Distance(GameManager.GetInstance().Playertransform.position, transform.position);
    }
}