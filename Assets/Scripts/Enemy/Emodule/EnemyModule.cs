using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Module/Enemy")]
public class EnemyModule : ScriptableObject
{
    public int maxHp;
    public float def;
    public float moveSpeed;
    public float shootSpeed;
    public float chaseDistance;
    public float attackDistance;

    public bool isStun;
    public bool isSlow;
    public bool isBurn;
}
