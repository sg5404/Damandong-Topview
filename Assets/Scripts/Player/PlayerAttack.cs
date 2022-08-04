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

    public List<GameObject> leftWeaponList;
    public List<GameObject> rightWeaponList;

    private Vector2 weaponPos;

    public WeaponModule[] module;
    public int weapon { private set; get; } = 0;

    private float curtime = 0;

    private WeaponSet weaponSet = null;
    private PlayerSkills playerSkills = null;
    private InventoryScript inventoryScript = null;

    void Start()
    {
        weaponSet = GetComponent<WeaponSet>();
        playerSkills = GetComponent<PlayerSkills>();
        inventoryScript = FindObjectOfType<InventoryScript>();
        weaponPos = weaponObj.transform.localPosition;
    }

    void Update()
    {
        curtime += Time.deltaTime;

        RotateGun();

        CurrentWeapon();

        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponSkills();
        }
    }

    void RotateGun()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var direction = mousePosition - bulletTransform.position;

        var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Debug.Log(rotation);
        bulletTransform.rotation = Quaternion.Euler(0, 0, rotation);

        Debug.Log((int)weaponSet.SetWeaponNum().x);
        Debug.Log((int)weaponSet.SetWeaponNum().y);

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

        if (isturn)
            num = -1;
        else
            num = 1;

        weaponObj.transform.localPosition = new Vector2(weaponPos.x * num, weaponPos.y);
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
                for(int i = 0; i <= 8; i++)
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
