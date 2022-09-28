using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform Playertransform;
    public GameObject zombie;
    public List<Transform> spawnPos;
    public List<int> spawnNum;
    public int stage;
    public bool isClear = false;
    public int enemyCount = 0;

    [SerializeField] private List<GameObject> enemyList;

    private void Start()
    {
        Debug.Log("MainWeaponState : " + WeaponSet.Instance.MainWeaponState);
        Debug.Log("SubeaponState : " + WeaponSet.Instance.SubWeaponState);
        StartSpawn();
    }

    /// <summary>
    /// 적 스폰
    /// </summary>
    /// <returns></returns>
    private IEnumerator spawnEnemy()
    {

        while(enemyCount < spawnNum[stage] || !ClearCheck(enemyCount))
        {
            if (enemyCount < spawnNum[stage])
            {
                int num = Random.Range(0, 8);
                enemyList.Add(Instantiate(zombie, spawnPos[num]));
                enemyCount++;
                yield return new WaitForSeconds(1.5f);
            }
            else
                yield return null;
        }
        Clear();
    }

    bool ClearCheck(int enemyCount)
    {
        if (enemyCount < spawnNum[stage])
            return false;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].activeSelf)
                 return false;
        }
        return true;
    }

    void Clear()
    {
        isClear = true;
        enemyCount = 0;
        stage++;
        enemyList.Clear();
        ElavatorSpawn();
        StopCoroutine(spawnEnemy());
    }

    /// <summary>
    /// 넘어왔을때 실행
    /// </summary>
    void StartSpawn()
    {
        isClear = false;
        StartCoroutine(spawnEnemy());
    }

    /// <summary>
    /// 엘리베이터 스폰
    /// </summary>
    void ElavatorSpawn()
    {
        //스폰 코드
        Debug.Log("엘리베이터 스폰!");
        //해야할일
        //포탈을 타면 startspawn다시 켜주면됨
    }
}
