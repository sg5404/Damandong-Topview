using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform arrivePos;
    [SerializeField] private bool startStage = false;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && GameManager.Instance.isClear)
        {
            if (startStage)
            {
                GameManager.Instance.StartSpawn();
                PlayerExperience.Instance.RestartRemoveExp();
                PlayerController.Instance.isShop = !PlayerController.Instance.isShop;
            }
            else
            {
                PlayerExperience.Instance.StopRemoveExp();
                PlayerController.Instance.isShop = !PlayerController.Instance.isShop;
            }
            GameManager.Instance.Playertransform.position  = arrivePos.position;
        }
    }
}
