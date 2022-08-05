using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public PlayerBase Stat;
    public GameObject dieText;
    public GameObject continueText;

    private void Start()
    {
        dieText.SetActive(false);
        continueText.SetActive(false);
    }

    public void Damaged(int damage)
    {
        Stat.Hp -= damage;
        if (Stat.Hp <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        dieText.SetActive(true);
        continueText.SetActive(true);
    }

    private void Update()
    {
        if (!PlayerCtrl.Instance.isDead) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TownScene");
        }
    }
}