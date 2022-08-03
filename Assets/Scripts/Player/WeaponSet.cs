using System;
using UnityEngine;

public class WeaponSet : MonoSingleton<WeaponSet>
{
   public WeaponSet()
    {
        MainWeaponState = WeaponKind.RIFLE;
        SubWeaponState = WeaponKind.RIFLE;
    }
    private WeaponKind mainWeaponState = 0; // °íÁ¤µÈ ¹«±â(¿ÞÆÈ)
    private WeaponKind subWeaponState = 0; // º¯°æ°¡´É ¹«±â(¿À¸¥ÆÈ)
    public WeaponKind MainWeaponState
    {
        get
        {
            return mainWeaponState;
        }
        set
        {
            if (value >= WeaponKind.RIFLE && value <= WeaponKind.GRANADE)
            {
                mainWeaponState = value;
            }
        }
    }
    public WeaponKind SubWeaponState
    {
        get
        {
            return subWeaponState;
        }
        set
        {
            if (value >= WeaponKind.RIFLE && value <= WeaponKind.GRANADE)
            {
                subWeaponState = value;
            }
        }
    }
    public WeaponKind SetWeapon(string _input)
    {
        WeaponKind _weaponKind = subWeaponState;

        switch (_input)
        {
            case "1":
                _weaponKind = WeaponKind.RIFLE;
                break;
            case "2":
                _weaponKind = WeaponKind.SNIPER;
                break;
            case "3":
                _weaponKind = WeaponKind.SHOTGUN;
                break;
            case "4":
                _weaponKind = WeaponKind.GRANADE;
                break;
            default:
                break;
        }
        return _weaponKind;
    }
}
