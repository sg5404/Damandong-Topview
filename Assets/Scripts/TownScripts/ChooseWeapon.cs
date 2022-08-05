using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeapon : MonoBehaviour
{
    public void rightWeaponChange(int weaponKind)
    {
        WeaponSet.Instance.MainWeaponState = (WeaponKind)weaponKind;
        Debug.Log(WeaponSet.Instance.MainWeaponState);
    }
}
