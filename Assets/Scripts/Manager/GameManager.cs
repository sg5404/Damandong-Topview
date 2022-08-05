using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform Playertransform;

    private void Start()
    {
        Debug.Log("MainWeaponState : " + WeaponSet.Instance.MainWeaponState);
        Debug.Log("SubeaponState : " + WeaponSet.Instance.SubWeaponState);
    }
}
