using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Module/Player")]
public class PlayerModule : ScriptableObject
{
    public int HP;
    public int MP;
    public int ammo;
    public float atk;
    public float def;
    public float criticalChance;
    public float criticalDamage;
    public float moveSpeed;
    public float attackSpeed;
}
