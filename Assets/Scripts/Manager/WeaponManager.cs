using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject subWeapon;
    private WeaponSet weaponSet = null;

    void Start()
    {
        weaponSet = FindObjectOfType<WeaponSet>();
    }

    void Update()
    {
        WeaponChange();
    }

    private void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("1");
            Debug.Log(weaponSet.SubWeaponState);
            subWeapon.GetComponent<Image>().color = Color.red;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("2");
            Debug.Log(weaponSet.SubWeaponState);
            subWeapon.GetComponent<Image>().color = Color.yellow;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("3");
            Debug.Log(weaponSet.SubWeaponState);
            subWeapon.GetComponent<Image>().color = Color.green;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("4");
            Debug.Log(weaponSet.SubWeaponState);
            subWeapon.GetComponent<Image>().color = Color.blue;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            weaponSet.SubWeaponState = weaponSet.SetWeapon("5");
            Debug.Log(weaponSet.SubWeaponState);
            subWeapon.GetComponent<Image>().color = Color.cyan;
        }
    }
}
