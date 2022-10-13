using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform arrivePos;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && GameManager.Instance.isClear)
        {
            GameManager.Instance.Playertransform.position  = arrivePos.position;
        }
    }
}
