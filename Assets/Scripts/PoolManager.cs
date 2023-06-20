using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] pool;
    List<GameObject>[] enemyList;

    private void Awake()
    {
        enemyList = new List<GameObject>[pool.Length];


        for (int i = 0; i < pool.Length; i++)
        {
            enemyList[i] = new List<GameObject>();
        }
    }


    public GameObject GetPool(int num)
    {

        GameObject select = null;

        foreach (GameObject e in enemyList[num])
        {
            if (!e.activeSelf)
            {
                select = e;
                e.gameObject.SetActive(true);
                

                break;

            }
        }

        if (!select)
        {

            select = Instantiate(pool[num], transform);
            enemyList[num].Add(select);
        }

        return select;

    }

}
