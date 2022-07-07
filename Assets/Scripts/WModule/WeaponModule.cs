using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Module/Weapon")]
public class WeaponModule : ScriptableObject
{
    public WeaponKind kind;//무기종류
    public float atkSpeed;//공속

    public float bulletSpread;//탄퍼짐
    public int oneShotBullets;//한번에 나가는 총알 수

    public bool isAutoShot;//연사

    public int magazine;//탄창
    public bool isInfiniteBullet;//한무 탄창
    public float reload;//재장전 시간
}
