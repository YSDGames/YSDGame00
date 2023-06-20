using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    [SerializeField] GameObject[] Bosses;
    int bossNum = 0;


    float timer = 0;


    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            Spawn();
            timer = 0;
        }

        SpawnBoss();
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetPool(1);
        enemy.transform.position = spawnPoint[Random.RandomRange(1, spawnPoint.Length)].position;
    }

    void SpawnBoss()
    {
        if(GameManager.instance.GetTime() / (3 * 60) == bossNum)
        {
            GameObject boss = Instantiate(Bosses[bossNum++]);
            boss.gameObject.transform.position = new Vector3(0, 18, 0);

        }
    }
}
