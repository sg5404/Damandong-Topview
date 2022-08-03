using System.Collections.Generic;
using UnityEngine;

public class smallStage : MonoBehaviour
{

    [SerializeField] private Vector3 MapSize = new Vector3(35, 35, 0);

    [SerializeField] private Collider2D[] col;
    [SerializeField] private List<GameObject> enemyList;

    [SerializeField] private GameObject Gate;

    bool isClear = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = MapSize; ;
        EnemySearch();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyClear();
    }

    void EnemySearch()
    {
        col = Physics2D.OverlapAreaAll(transform.position - MapSize / 2, transform.position + MapSize / 2);
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].CompareTag("Enemy"))
            {
                enemyList.Add(col[i].gameObject);
            }
        }
    }

    void EnemyClear()
    {
        while (!isClear)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].activeSelf)
                {
                    //안끝남
                    Gate.SetActive(false);
                    return;
                }
            }
            //끝남
            isClear = true;
            Debug.Log("Clear");
            //애니메이션 추가해주면 좋을듯
            Gate.SetActive(true);
        }
    }

    //void StageClear()
    //{
    //    Gate.SetActive(true);
    //}

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(1, 1, 1));
    }
}
