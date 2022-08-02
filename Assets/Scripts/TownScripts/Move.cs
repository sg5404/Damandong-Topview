using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoSingleton<Move>
{
    //1 : 0.5625

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private GameObject smithObj;

    Vector3 playerDir;

    void Update()
    {
        Interaction();
        MovePlayer();
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Vector2.Distance(transform.position, smithObj.transform.position) <= 1f
                && !TownUIManager.Instance.isDialogueWithSmith)
            {
                StartCoroutine(TownUIManager.Instance.InteractionSmith());
            }
        }

        if (Vector2.Distance(transform.position, smithObj.transform.position) >= 3f)
        {
            TownUIManager.Instance.DisActiveAllPanel();
        }
    }

    public void MovePlayer()
    {
        if (Fade.Instance.isFade) return;

        playerDir = transform.position;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(h, v, 0) * Time.deltaTime * speed;

        playerDir.x = Mathf.Clamp(transform.position.x, -12f, 12f);
        playerDir.y = Mathf.Clamp(transform.position.y, -6.5f, 6.5f);

        transform.position = playerDir;
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
}
