using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public PlayerBase Stat;

    public void Damaged(int damage)
    {
        Stat.Hp -= damage;
        if(Stat.Hp <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {

    }

}
