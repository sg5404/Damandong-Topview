using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponSet weaponSet = null;
    private PlayerSkills playerSkills = null;

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
    private GameObject gunpoint = null;

    public WeaponModule[] module;
    public int weapon { private set; get; } = 1;

    private float cooltime = 0.1f;
    private float curtime = 0;

    void Start()
    {
        weaponSet = GetComponent<WeaponSet>();
        playerSkills = GetComponent<PlayerSkills>();
    }

    void Update()
    {
        curtime += Time.deltaTime;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var direction = mousePosition - bulletTransform.position;

        var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bulletTransform.rotation = Quaternion.Euler(0, 0, rotation);
        gunpoint.transform.rotation = Quaternion.Euler(0, 0, rotation);
        CurrentWeapon();

        if (Input.GetKeyDown(KeyCode.E))
        {
            WeaponSkills();
        }
    }

    void CurrentWeapon()
    {
        switch (weaponSet.SubWeaponState) // ÃÑ¾Ë¹ß»ç °¡´ÉÇÏ°Ô²û ÇÏ±â
        {
            case WeaponKind.SWORD:
                weapon = 0;
                break;
            case WeaponKind.RIFLE:
                weapon = 1;
                Fire();
                break;
            case WeaponKind.SNIPER:
                weapon = 2;
                Fire();
                break;
            case WeaponKind.SHOTGUN:
                weapon = 3;
                ShotGunFire();
                break;
            case WeaponKind.GRANADE:
                weapon = 4;
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
            if(curtime >= module[weapon].atkSpeed)
            {
                GameObject bullet = Instantiate(rifleBullet, bulletTransform);
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
                    GameObject bullet = Instantiate(rifleBullet, bulletTransform);
                    bullet.transform.Rotate(0, 0, Random.Range(-module[weapon].bulletSpread , module[weapon].bulletSpread));
                    //bullet.GetComponent<BulletMove>(). = Random.Range(5, 10);
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
            case WeaponKind.SWORD:
                break;
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
