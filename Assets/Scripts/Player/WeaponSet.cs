using System;
using UnityEngine;

public class WeaponSet : MonoSingleton<WeaponSet>
{
    public WeaponSet()
    {
        //MainWeaponState = WeaponKind.RIFLE;
        //SubWeaponState = WeaponKind.RIFLE;
    }
    private static WeaponKind mainWeaponState = WeaponKind.RIFLE; // °íÁ¤µÈ ¹«±â(¿À¸¥ÆÈ)
    private static WeaponKind subWeaponState = WeaponKind.RIFLE; // º¯°æ°¡´É ¹«±â(¿ÞÆÈ)
    public WeaponKind MainWeaponState
    {
        get
        {
            return mainWeaponState;
        }
        set
        {
            if (value >= WeaponKind.RIFLE && value <= WeaponKind.SWORD)
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
            if (value >= WeaponKind.RIFLE && value <= WeaponKind.SWORD)
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
            case "5":
                _weaponKind = WeaponKind.SWORD;
                break;
            default:
                break;
        }
        return _weaponKind;
    }
    public Vector2 SetWeaponNum()
    {
        return new Vector2((float)mainWeaponState, (float)subWeaponState);
    }
}
