using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform Playertransform;
    public GameObject zombie;
    public List<Transform> spawnPos;

    private void Start()
    {
        Debug.Log("MainWeaponState : " + WeaponSet.Instance.MainWeaponState);
        Debug.Log("SubeaponState : " + WeaponSet.Instance.SubWeaponState);
        StartCoroutine(spawnEnemy());
    }

    private IEnumerator spawnEnemy()
    {
        while(true)
        {
            int num = Random.Range(0, 8);
            Instantiate(zombie, spawnPos[num]);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
