using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerCtrl : MonoBehaviour
{
    Rigidbody2D rigid;

    [SerializeField]
    Image Main_Weapon;

    [SerializeField] private float speed;

    bool isEquip = false;

    GameObject nearObject;
    private WeaponSet weaponSet = null;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        weaponSet = GetComponent<WeaponSet>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal") * speed;
        float v = Input.GetAxisRaw("Vertical") * speed;
        rigid.velocity = new Vector2(h, v);
        WeaponEquip();
        WeaponChange();
    }

    private void WeaponEquip()
    {
        if (Input.GetKeyDown(KeyCode.E) && isEquip)
        {
            Debug.Log("Click");
            Main_Weapon.sprite = nearObject.GetComponent<SpriteRenderer>().sprite;
            nearObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            var Objects = GameObject.FindGameObjectsWithTag("Weapon").ToList();
            nearObject = Objects.OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.transform.position);
            }).FirstOrDefault();
            isEquip = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            isEquip = false;
        }
    }

    private void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("1");
            Debug.Log(weaponSet.SubWeaponState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("2");
            Debug.Log(weaponSet.SubWeaponState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("3");
            Debug.Log(weaponSet.SubWeaponState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("4");
            Debug.Log(weaponSet.SubWeaponState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("5");
            Debug.Log(weaponSet.SubWeaponState);
        }
    }
}
