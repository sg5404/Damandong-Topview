using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform Playertransform;
    [SerializeField] private GameObject zombie;
    [SerializeField] private List<Transform> spawnPos;
    [SerializeField] private List<int> spawnNum;
    [SerializeField] private GameObject Elevator;
    [ReadOnly] public int stage;
    [ReadOnly] public bool isClear = false;
    private int enemyCount = 0;

    [SerializeField] private Animator elevatorAni;

    [SerializeField] private List<GameObject> enemyList;

    private void Start()
    {
        Debug.Log("MainWeaponState : " + WeaponSet.Instance.MainWeaponState);
        Debug.Log("SubeaponState : " + WeaponSet.Instance.SubWeaponState);
        StartSpawn();
    }

    /// <summary>
    /// �� ����
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
