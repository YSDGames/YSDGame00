using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject[] enemies;
    List<GameObject>[] enemyList;

    private void Awake()
    {
        enemyList = new List<GameObject>[enemies.Length];


        for (int i = 0; i < enemies.Length; i++)
        {
            enemyList[i] = new List<GameObject>();
        }
    }


    public GameObject GetEnemy(int num)
    {

        GameObject select = null;

        foreach (GameObject e in enemyList[num])
        {
            if (!e.activeSelf)
            {
                select = e;

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemyList[i] = new List<GameObject>();
                }

            }
        }

        if (!select)
        {

            select = Instantiate(enemies[num], transform);
            enemyList[num].Add(select);
        }

        return select;

    }

}
