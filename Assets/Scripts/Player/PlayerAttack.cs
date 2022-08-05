using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoSingleton<PlayerAttack>
{

    [Header("ÃÑ¾Ë")]
    [SerializeField]
    private GameObject rifleBullet = null;
    [SerializeField]
    private GameObject sniperBullet = null;
    [SerializeField]
    private GameObject shotgunBullet = null;
    [SerializeField]
    private GameObject granadeBullet = null;

    [SerializeField]
    private Transform bulletTransform = null;
    [SerializeField]
    private GameObject leftGunpoint = null;
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
    public int weapon { private set; get; } = 0;

    private float curtime = 0;
    private float timer = 0;
    float rotation;

    private WeaponSet weaponSet = null;
    private PlayerSkills playerSkills = null;
    private InventoryScript inventoryScript = null;

    Vector3 leftWeaponPosTemp;
    Vector3 rightWeaponPosTemp;

    void Start()
    {
        weaponSet = GetComponent<WeaponSet>();
        playerSkills = GetComponent<PlayerSkills>();
        inventoryScript = FindObjectOfType<InventoryScript>();

        weaponPos = weaponObj.transform.localPosition;
        leftWeaponPosTemp = leftWeaponPos.transform.localPosition;
        rightWeaponPosTemp = rightWeaponPos.transform.localPosition;

        fireEff2[(int)weaponSet.SetWeaponNum().x - 1].SetActive(false);
        
    }

    void Update()
    {
        Debug.Log((int)weaponSet.SetWeaponNum().y - 1);
        curtime += Time.deltaTime;

        RotateGun();

        CurrentWeapon();

        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponSkills();
        }

        if (timer >= 0)
        {
            fireEff[(int)weaponSet.SetWeaponNum().y-1].SetActive(true);
            fireEff[(int)weaponSet.SetWeaponNum().y-1].GetComponent<ParticleSystem>().startRotation = (rotation+180)/57.295f * -1;
            //Debug.Log(rotation);
            timer -= Time.deltaTime;
        }
        else
        {
            fireEff[(int)weaponSet.SetWeaponNum().y - 1].SetActive(false);
        }
    }

    void RotateGun()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var direction = mousePosition - bulletTransform.position;

        rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Debug.Log(rotation);
        bulletTransform.rotation = Quaternion.Euler(0, 0, rotation);

        //Debug.Log((int)weaponSet.SetWeaponNum().x);
        //Debug.Log((int)weaponSet.SetWeaponNum().y);

        if (rotation >= -90 && rotation <= 90)
        {
            RotateWeapons(false);
            return;
        }
        RotateWeapons(true);
    }

    void RotateWeapons(bool isturn)
    {
        float num = 0;
        gameObject.GetComponent<SpriteRenderer>().flipX = isturn;
        rightWeaponList[(int)weaponSet.SetWeaponNum().x - 1].GetComponent<SpriteRenderer>().flipY = isturn;
        leftWeaponList[(int)weaponSet.SetWeaponNum().y - 1].GetComponent<SpriteRenderer>().flipY = isturn;
        weaponObj.transform.localPosition = new Vector2(weaponPos.x * TurnCheck(isturn), weaponPos.y);
        
    }

    int TurnCheck(bool isturn)
    {
        int num = 0;

        leftWeaponPos.transform.localPosition = isturn switch
        {
            true => rightWeaponPosTemp,
            false => leftWeaponPosTemp,
        };

        rightWeaponPos.transform.localPosition = isturn switch
        {
            true => leftWeaponPosTemp,
            false => rightWeaponPosTemp,
        };

        return num = isturn switch
        {
            true => -1,
            false => 1,
        };
    }

    void CurrentWeapon()
    {
        switch (weaponSet.SubWeaponState) // ÃÑ¾Ë¹ß»ç °¡´ÉÇÏ°Ô²û ÇÏ±â
        {
            case WeaponKind.RIFLE:
                weapon = 0;
                Fire();
                break;
            case WeaponKind.SNIPER:
                weapon = 1;
                Fire();
                break;
            case WeaponKind.SHOTGUN:
                weapon = 2;
                ShotGunFire();
                break;
            case WeaponKind.GRANADE:
                weapon = 3;
                Fire();
                break;
            default:
                break;
                
        }
    }

    void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            if (inventoryScript.isShop) return;

            if(curtime >= module[weapon].atkSpeed)
            {
                GameObject bullet = Instantiate(rifleBullet, leftGunpoint.transform);
                timer = 0.08f;
                bullet.transform.SetParent(null);
                curtime = 0;
            }

        }
    }

    void ShotGunFire()
    {
        if (Input.GetMouseButton(0))
        {
            if (curtime >= module[weapon].atkSpeed)
            {
                timer = 0.08f;
                for (int i = 0; i <= 8; i++)
                {
                    GameObject bullet = Instantiate(rifleBullet, leftGunpoint.transform);
                    bullet.transform.Rotate(0, 0, Random.Range(-module[weapon].bulletSpread , module[weapon].bulletSpread));
                    bullet.transform.SetParent(null);
                }
                    curtime = 0;
            }
        }
    }

    void WeaponSkills()
    {
        switch (weaponSet.SubWeaponState)
        {
            case WeaponKind.RIFLE:
                playerSkills.Lambo();
                break;
            case WeaponKind.SNIPER:
                break;
            case WeaponKind.SHOTGUN:
                playerSkills.MadangSslGi();
                break;
            case WeaponKind.GRANADE:
                playerSkills.Stun();
                break;
        }
    }
}
