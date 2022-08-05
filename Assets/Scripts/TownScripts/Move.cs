using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoSingleton<Move>
{
    //1 : 0.5625

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private GameObject[] npcObj;

    Vector3 playerDir;

    float shortestDisToNpc;
    private GameObject shortestNpcObj = null;
    private Animator animator;

    private bool isStop = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isStop) return;
        Interaction();
        MovePlayer();
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            shortestDisToNpc = Vector2.Distance(transform.position, npcObj[0].transform.position);
            shortestNpcObj = npcObj[0];
            foreach (GameObject npcObjItem in npcObj)
            {
                float distance = Vector2.Distance(transform.position, npcObjItem.transform.position);
                if (distance < shortestDisToNpc)
                {
                    shortestNpcObj = npcObjItem;
                    shortestDisToNpc = distance;
                }
            }

            Debug.Log(Vector2.Distance(transform.position, shortestNpcObj.transform.position));
            Debug.Log(shortestNpcObj.tag);

            if (Vector2.Distance(transform.position, shortestNpcObj.transform.position) <= 2f
                && !TownUIManager.Instance.isDialogueWithNpc)
            {
                switch (shortestNpcObj.tag)
                {
                    case "Smith":
                        TownUIManager.Instance.InteractionSmith();
                        break;
                    case "SalesMan":
                        TownUIManager.Instance.InteractionSalesman();
                        break;
                    case "Home":
                        TownUIManager.Instance.InteractionHome();
                        break;
                    case "ETC":
                        TownUIManager.Instance.InteractionETC();
                        break;
                }
            }
        }

        if(shortestNpcObj != null)
        {
            if (Vector2.Distance(transform.position, shortestNpcObj.transform.position) >= 2.5f
                && Vector2.Distance(transform.position, shortestNpcObj.transform.position) <= 10f)
                {
                    TownUIManager.Instance.DisActiveAllPanel();
                    TownUIManager.Instance.isDialogue = false;
                    TownUIManager.Instance.isDialogueWithNpc = false;
                }
        }
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

        transform.position += new Vector3(h, v, 0) * Time.deltaTime * speed;

        playerDir.x = Mathf.Clamp(transform.position.x, -12f, 12f);
        playerDir.y = Mathf.Clamp(transform.position.y, -6.5f, 6.5f);

        transform.position = playerDir;
        if (h != 0 || v != 0) 
            animator.SetBool("isMove", true);
        else
            animator.SetBool("isMove", false);
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
