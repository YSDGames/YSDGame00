using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    [SerializeField] GameObject[] Bosses;
    int bossNum;


    float timer = 0;



    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();

        bossNum = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1.5)
        {
            Spawn();
            timer = 0;
        }

        SpawnBoss();
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetPool(1);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }

    void SpawnBoss()
    {
        if (GameManager.instance.GetTime() / (2 * 60) >= bossNum + 1)
        {
            GameObject boss = Instantiate(Bosses[bossNum++]);
            boss.gameObject.transform.position = new Vector3(0, 18, 0);

        }
    }
}
