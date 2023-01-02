using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoSingleton<Move>
{
    //1 : 0.5625

    [SerializeField]
    private float speed = 10f;
    //[SerializeField]
    //private GameObject[] npcObj;

    Vector3 playerDir;

    float shortestDisToNpc;
    private GameObject shortestNpcObj = null;
    private Animator animator;
    private Rigidbody2D rigid;

    private bool isStop = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isStop) return;
        MovePlayer();
        KeyDown();
    }

    public void MovePlayer()
    {
        if (Fade.Instance.isFade || TownUIManager.Instance.isWeaponChoose || TownUIManager.Instance.isDialogue) return;

        playerDir = transform.position;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if(h == 1)
        {
            gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else if(h == -1)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //transform.position += new Vector3(h, v, 0) * Time.deltaTime * speed;

        rigid.velocity = new Vector3(h, v, 0) * speed;// * Time.deltaTime * speed;

        playerDir.x = Mathf.Clamp(transform.position.x, -12f, 12f);
        playerDir.y = Mathf.Clamp(transform.position.y, -6.5f, 6.5f);

        transform.position = playerDir;
        if (h != 0 || v != 0) 
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);
    }

    void KeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !TownUIManager.Instance.isWeaponChoose)
        {
            TownUIManager.Instance.DisActiveAllPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DungeonPortal"))
        {
            TownUIManager.Instance.ToggleGoDungeonPanel(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DungeonPortal"))
        {
            TownUIManager.Instance.ToggleGoDungeonPanel(false);
        }
    }

    public void TogglePause(bool stop)
    {
        isStop = stop;
    }
}
