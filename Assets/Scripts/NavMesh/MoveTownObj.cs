using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTownObj : MonoBehaviour
{
    [SerializeField] List<GameObject> townObjlist = null;
    [SerializeField] float shortestDis;

    private GameObject townObj = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GoInTownObj();
        }
    }

    void GoInTownObj()
    {
        if (townObjlist.Count >= 1)
        {
            shortestDis = Vector2.Distance(transform.position, townObjlist[0].transform.position);
            townObj = townObjlist[0];

            foreach (GameObject townObjItem in townObjlist)
            {
                float distance = Vector2.Distance(transform.position, townObjItem.transform.position);
                if (distance < shortestDis)
                {
                    townObj = townObjItem;
                    shortestDis = distance;
                }
            }
        }

        if (Vector2.Distance(transform.position, townObj.transform.position) <= 2f)
        {
            Debug.Log(townObj);
        }

    }
}
