using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoSingleton<PlayerAttack>
{

    [Header("�Ѿ�")]
    [SerializeField]
    private GameObject rifleBullet = null;
    [SerializeField]
    private GameObject sniperBullet = null;
    [SerializeField]
    private GameObject shotgunBullet = null;
    [SerializeField]
    private GameObject granadeBullet = null;
    [SerializeField]
    private GameObject swordBullet = null;

    [SerializeField]
    private Transform bulletTransform = null;
    [SerializeField]
    private GameObject leftGunpoint = null;
    [SerializeField]
    private GameObject rightGunpoint = null;
    [SerializeField]
    private GameObject weaponObj = null;
    [SerializeField]
    private GameObject leftWeaponPos = null;
    [SerializeField]
    private GameObject rightWeaponPos = null;

    public List<GameObject> fireEff;
    public List<GameObject> fireEff2;

    public GameObject[] leftWeaponList;
    public GameObject[] rightWeaponList;

    private Vector2 weaponPos;

    public WeaponModule[] module;
    public int leftWeapon { private set; get; } = 0;
    public int rightWeapon { private set; get; } = 0;

    private float leftCurtime = 0;
    private float rightCurtime = 0;

    private float leftTimer = 0;
    private float rightTimer = 0;
    float leftRotation;

    private WeaponSet weaponSet = null;
    private PlayerSkills playerSkills = null;
    private InventoryScript inventoryScript = null;

    private UIManager _ui;

    Vector3 leftWeaponPosTemp;
    Vector3 rightWeaponPosTemp;

    void Start()
    {
        _ui = FindObjectOfType<UIManager>();
        weaponSet = GetComponent<WeaponSet>();
        playerSkills = GetComponent<PlayerSkills>();
        inventoryScript = FindObjectOfType<InventoryScript>();

        weaponPos = weaponObj.transform.localPosition;
        leftWeaponPosTemp = leftWeaponPos.transform.localPosition;
        rightWeaponPosTemp = rightWeaponPos.transform.localPosition;

        fireEff2[(int)weaponSet.SetWeaponNum().x - 1].SetActive(false);
        Setting();
        CurrentWeapon();
    }

    void Update()
    {
        if (PlayerCtrl.Instance.playerBase.IsDead) return;
        //Debug.Log((int)weaponSet.SetWeaponNum().y - 1);
        leftCurtime += Time.deltaTime;
        rightCurtime += Time.deltaTime;

        //Debug.Log($"isStoped : {_ui.isStoped}");
        if (!_ui.isStoped)
            RotateGun();

        CurrentWeapon();

        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponSkills();
        }

        if (leftTimer >= 0)
        {
            fireEff[(int)weaponSet.SetWeaponNum().y - 1].SetActive(true);
            fireEff[(int)weaponSet.SetWeaponNum().y - 1].GetComponent<ParticleSystem>().startRotation = (leftRotation + 180) / 57.295f * -1;
            //Debug.Log(rotation);
            leftTimer -= Time.deltaTime;
        }
        else
        {
            fireEff[(int)weaponSet.SetWeaponNum().y - 1].SetActive(false);
        }

        if (rightTimer >= 0)
        {
            fireEff2[(int)weaponSet.SetWeaponNum().x - 1].SetActive(true);
            fireEff2[(int)weaponSet.SetWeaponNum().x - 1].GetComponent<ParticleSystem>().startRotation = (leftRotation + 180) / 57.295f * -1;
            //Debug.Log(rotation);
            rightTimer -= Time.deltaTime;
        }
        else
        {
            fireEff2[(int)weaponSet.SetWeaponNum().x - 1].SetActive(false);
        }
    }

    private void Setting()
    {
        for (int i = 0; i < 4; i++)
        {
            fireEff[i].SetActive(false);
            fireEff2[i].SetActive(false);
        }
        fireEff[(int)weaponSet.SetWeaponNum().y - 1].SetActive(true);
        fireEff2[(int)weaponSet.SetWeaponNum().x - 1].SetActive(true);
    }

    void RotateGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var leftDir = mousePosition - leftWeaponPos.transform.position;
        leftRotation = Mathf.Atan2(leftDir.y, leftDir.x) * Mathf.Rad2Deg;

        rightWeaponPos.transform.rotation = Quaternion.Euler(0, 0, leftRotation);
        leftWeaponPos.transform.rotation = Quaternion.Euler(0, 0, leftRotation);

        if (leftRotation >= -90 && leftRotation <= 90)
        {
            flipWeapons(false);
            return;
        }
        flipWeapons(true);
    }

    void flipWeapons(bool isturn)
    {


        gameObject.GetComponent<SpriteRenderer>().flipX = isturn;
        rightWeaponList[rightWeapon].GetComponent<SpriteRenderer>().flipY = isturn;
        leftWeaponList[leftWeapon].GetComponent<SpriteRenderer>().flipY = isturn;

        //weaponObj.transform.localPosition = new Vector2(weaponPos.x * TurnCheck(isturn), weaponPos.y);
        TurnCheck(isturn);
    }

    int TurnCheck(bool isturn)
    {
        int num = 0;
        Vector3 temp = Vector3.zero;
        leftWeaponPos.transform.localPosition = isturn switch
        {
            true => rightWeaponPosTemp,
            false => new Vector3(leftWeaponPosTemp.x, rightWeaponPosTemp.y, leftWeaponPosTemp.z), //left
        };

        rightWeaponPos.transform.localPosition = isturn switch
        {
            true => leftWeaponPosTemp, //left
            false => new Vector3(rightWeaponPosTemp.x, leftWeaponPosTemp.y, rightWeaponPosTemp.z),
        };

        return num = isturn switch
        {
            true => -1,
            false => 1,
        };
    }

    void CurrentWeapon()
    {
        switch (weaponSet.SubWeaponState)
        {
            case WeaponKind.RIFLE:
                leftWeapon = 0;
                LeftWeaponFire();
                break;
            case WeaponKind.SNIPER:
                leftWeapon = 1;
                LeftWeaponFire();
                break;
            case WeaponKind.SHOTGUN:
                leftWeapon = 2;
                LeftShotGunFire();
                break;
            case WeaponKind.GRANADE:
                leftWeapon = 3;
                LeftWeaponFire();
                break;
            case WeaponKind.SWORD:
                leftWeapon = 4;
                LeftWeaponFire();
                break;
            default:
                break;
        }

        switch (weaponSet.MainWeaponState)
        {
            case WeaponKind.RIFLE:
                rightWeapon = 0;
                RightWeaponFire();
                break;
            case WeaponKind.SNIPER:
                rightWeapon = 1;
                RightWeaponFire();
                break;
            case WeaponKind.SHOTGUN:
                rightWeapon = 2;
                RightShotGunFire();
                break;
            case WeaponKind.GRANADE:
                rightWeapon = 3;
                RightWeaponFire();
                break;
            case WeaponKind.SWORD:
                rightWeapon = 4;
                RightWeaponFire();
                break;
            default:
                break;
        }
    }

    void LeftWeaponFire()
    {
        if (Input.GetMouseButton(0))
        {
            if (inventoryScript.isShop)/* || PlayerManager.Instance.Stat.SubMagazine == 0)*/ return;

            if (leftCurtime >= module[leftWeapon].atkSpeed)
            {
                GameObject bullet = Instantiate(module[leftWeapon].bullet, leftGunpoint.transform);
                bullet.transform.SetParent(null);
                //PlayerManager.Instance.Stat.SubMagazine -= 1;
                leftCurtime = 0;
                leftTimer = 0.08f;
            }
        }
    }

    void RightWeaponFire()
    {
        if (Input.GetMouseButton(1))
        {
            if (inventoryScript.isShop)/* || PlayerManager.Instance.Stat.SubMagazine == 0)*/ 
                return;

            if (rightCurtime >= module[rightWeapon].atkSpeed)
            {
                GameObject bullet = Instantiate(module[rightWeapon].bullet, rightGunpoint.transform);
                bullet.transform.SetParent(null);
                rightCurtime = 0;
                rightTimer = 0.08f;
                Debug.Log("����");
            }
        }
    }

    void LeftShotGunFire()
    {
        if (Input.GetMouseButton(0))
        {
            if (leftCurtime >= module[leftWeapon].atkSpeed)
            {
                for (int i = 0; i <= 8; i++)
                {
                    GameObject Lbullet = Instantiate(rifleBullet, leftGunpoint.transform);
                    //bullet.transform.Rotate(0, 0, Random.Range(-module[leftWeapon].bulletSpread, module[leftWeapon].bulletSpread));
                    Lbullet.transform.Rotate(0, 0, Random.Range(-20f, 20f));
                    Lbullet.transform.SetParent(null);
                }
                leftCurtime = 0;
                leftTimer = 0.08f;
            }
        }
    }

    void RightShotGunFire()
    {
        if (Input.GetMouseButton(1))
        {
            if (rightCurtime >= module[rightWeapon].atkSpeed)
            {
                for (int i = 0; i <= 8; i++)
                {
                    GameObject Rbullet = Instantiate(rifleBullet, rightGunpoint.transform);
                    //bullet.transform.Rotate(0, 0, Random.Range(-module[rightWeapon].bulletSpread, module[rightWeapon].bulletSpread));
                    Rbullet.transform.Rotate(0, 0, Random.Range(-20f, 20f));
                    Rbullet.transform.SetParent(null);
                }
                rightCurtime = 0;

                rightTimer = 0.08f;
            }
        }
    }

    void WeaponSkills()
    {
        switch (weaponSet.SubWeaponState)
        {
            case WeaponKind.RIFLE:
                StartCoroutine(PlayerSkills.Instance.Lambo());
                break;
            case WeaponKind.SNIPER:
                break;
            case WeaponKind.SHOTGUN:
                PlayerSkills.Instance.MadangSslGi();
                break;
            case WeaponKind.GRANADE:
                StartCoroutine(PlayerSkills.Instance.Stun());
                break;
        }
    }
}
