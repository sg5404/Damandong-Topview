using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform Playertransform;
    [SerializeField] private GameObject zombie;
    [SerializeField] private List<Transform> spawnPos;
    [SerializeField] private GameObject Elevator;
    [ReadOnly] public int stage;
    [ReadOnly] public bool isClear = false;
    //private List<int> spawnNum;
    [SerializeField] private int totalEnemyCount = 0;
    [SerializeField] private int curEnemyCount = 0;

    [SerializeField] private Animator elevatorAni;

    [SerializeField] private List<GameObject> enemyList;

    private void Start()
    {
        Debug.Log("MainWeaponState : " + WeaponSet.Instance.MainWeaponState);
        Debug.Log("SubeaponState : " + WeaponSet.Instance.SubWeaponState);

        //for (int i = 1; i < 31; ++i)
        //{
        //    spawnNum[i - 1] = (int)Mathf.Ceil(15 + (5 + i) * (i / 2 + Mathf.Pow(Mathf.Floor(i / 10), 2)));
        //    Debug.Log(spawnNum[i - 1]);
        //}
        stage = 1;
        Debug.Log("stage : " + stage);
        StartSpawn();

    }

    /// <summary>
    /// 적 스폰
    /// </summary>
    /// <returns></returns>
    private IEnumerator spawnEnemy()
    {
        // 올림(15 + (5 + 스테이지 레벨) * (스테이지 레벨 / 2 + 제곱(내림(스테이지레벨 / 10, 1의자리), 2), 1의자리)
        
        totalEnemyCount = (int)Mathf.Ceil(15 + (5 + stage) * (float)(stage * 0.5 + Mathf.Pow(Mathf.Floor(stage / 10), 2)));

        Debug.Log("Total Enemy Count : " + totalEnemyCount);

        while (curEnemyCount < totalEnemyCount || !ClearCheck(curEnemyCount))
        {
            if (curEnemyCount < totalEnemyCount)
            {
                int num = Random.Range(0, 8);
                enemyList.Add(Instantiate(zombie, spawnPos[num]));
                curEnemyCount++;
                yield return new WaitForSeconds(1.5f);
            }
            else
                yield return null;
        }
        Clear();
    }

    bool ClearCheck(int enemyCount)
    {
        if (enemyCount < totalEnemyCount)
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
        curEnemyCount = 0;
        stage++;
        StopCoroutine(spawnEnemy());
        enemyList.Clear();
        ElavatorSpawn();
    }

    /// <summary>
    /// 넘어왔을때 실행
    /// </summary>
    public void StartSpawn()
    {
        isClear = false;
        StartCoroutine(spawnEnemy());
        StartCoroutine(Delay("Close", false));
    }

    /// <summary>
    /// 엘리베이터 스폰
    /// </summary>
    void ElavatorSpawn()
    {
        //스폰 코드
        Debug.Log("엘리베이터 스폰!");
        StartCoroutine(Delay("Open", true));
        //포탈을 타면 startspawn다시 켜주면됨
    }

    IEnumerator Delay(string trigger, bool IsActive)
    {
        elevatorAni.SetTrigger(trigger);
        yield return new WaitForSeconds(2f);
        Elevator.SetActive(IsActive);
        StopCoroutine(Delay(trigger, IsActive));
    }
}
