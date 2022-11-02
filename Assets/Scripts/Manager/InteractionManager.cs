using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoSingleton<InteractionManager>
{
    [SerializeField]
    private GameObject[] npcObj;

    float shortestDisToNpc;
    private GameObject shortestNpcObj = null;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Interaction();
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rigid.velocity = Vector2.zero;
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

        if (shortestNpcObj != null)
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
}
