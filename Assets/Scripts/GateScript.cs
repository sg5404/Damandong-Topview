using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public bool isStore = false;
    [SerializeField] private Transform StagePos;
    [SerializeField] private Transform StorePos;
    [SerializeField] private Transform TargetPos;
    // Start is called before the first frame update
    void Start()
    {
        posCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void posCheck()
    {
        if(transform.position.x > 30f)
        {
            isStore = true;
        }
        isStore = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player"))
        //{
        //    Transform movePos = isStore switch
        //    {
        //        true => StagePos,
        //        false => StorePos,
        //    };

        //    collision.transform.position = movePos.position;
        //    isStore = !isStore;
        //}
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = TargetPos.position;
        }
    }
}
