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
    /// �� ����
    /// </summary>
    /// <returns></returns>
    private IEnumerator spawnEnemy()
    {
        // �ø�(15 + (5 + �������� ����) * (�������� ���� / 2 + ����(����(������������ / 10, 1���ڸ�), 2), 1���ڸ�)
        
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
    /// �Ѿ������ ����
    /// </summary>
    public void StartSpawn()
    {
        isClear = false;
        StartCoroutine(spawnEnemy());
        StartCoroutine(Delay("Close", false));
    }

    /// <summary>
    /// ���������� ����
    /// </summary>
    void ElavatorSpawn()
    {
        //���� �ڵ�
        Debug.Log("���������� ����!");
        StartCoroutine(Delay("Open", true));
        //��Ż�� Ÿ�� startspawn�ٽ� ���ָ��
    }

    IEnumerator Delay(string trigger, bool IsActive)
    {
        elevatorAni.SetTrigger(trigger);
        yield return new WaitForSeconds(2f);
        Elevator.SetActive(IsActive);
        StopCoroutine(Delay(trigger, IsActive));
    }
}
