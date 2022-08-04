using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTownObj : MonoBehaviour
{
    [SerializeField] List<GameObject> townObjlist = null;
    [SerializeField] List<Transform> townObjpos = null;
    [SerializeField] float shortestDis;
    [SerializeField] GameObject MainObjs;
    [SerializeField] GameObject OutObj;

    int temp = 0;

    private Vector3 tempPos;

    private GameObject townObj = null;

    private enum TOWMOBJ
    {
        SHOP = 0,
        SMITHY, //대장간
        HOME,
        ETC,
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("InDoor"))
        {
            if (Fade.Instance.isFade) yield return null;
            Fade.Instance.FadeIn();
            yield return new WaitForSeconds(Fade.Instance.fadeTime);
            GetInTownObj();
            Fade.Instance.FadeOut();
        }

        if(collision.CompareTag("OutDoor"))
        {
            if (Fade.Instance.isFade) yield return null;
            Fade.Instance.FadeIn();
            yield return new WaitForSeconds(Fade.Instance.fadeTime);
            GetOutTownObj();
            Fade.Instance.FadeOut();
        }

        yield break;
    }

    void GetOutTownObj()
    {
        townObjpos[temp].position = tempPos;
        MainObjs.SetActive(true);
        OutObj.SetActive(false);
        transform.position = townObjlist[temp].transform.position + new Vector3(0, -3f, 0);
    }

    void GetInTownObj()
    {
        temp = 0;
        int i = 0;
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
                    temp = i;
                }
                i++;
            }
        }


        if (Vector2.Distance(transform.position, townObj.transform.position) <= 3f)
        {
            Debug.Log(townObj);
            Debug.Log(townObjlist[temp].name);

            MainObjs.SetActive(false);
            OutObj.SetActive(true);

            tempPos = townObjpos[temp].position; //원래 있던 위치를 저장하기
            townObjpos[temp].position = new Vector3(0, 0, 0); //선택된 오브젝트를 가운데로 가져오기
            transform.position = OutObj.transform.position + new Vector3(0, 2, 0); //플레이어 위치 선정

        }
    }
}
