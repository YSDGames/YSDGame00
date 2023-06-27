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

        SpawnByTime();

        //보스는 0~bossNum까지있으므로..
        if (bossNum < 5)
        {
            SpawnBoss();
        }

    }

    void SpawnByTime()
    {
        if (GameManager.instance.GetTime() > 60 * 9)
        {
            if (timer > 1f)
            {
                Spawn(7, "all");
                Spawn(3, "left");
                Spawn(3, "left");

                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 8)
        {
            if (timer > 3f)
            {
                Spawn(7, "center");
                Spawn(6, "right");
                Spawn(6, "left");
                Spawn(3, "left");
                Spawn(3, "left");
                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 7)
        {
            if (timer > 3f)
            {
                Spawn(6, "center");
                Spawn(5, "right");
                Spawn(5, "left");
                Spawn(3, "left");


                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 6)
        {
            if (timer > 3f)
            {
                Spawn(6, "center");
                Spawn(5, "all");
                Spawn(4, "all");
                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 5)
        {
            if (timer > 3f)
            {
                Spawn(5, "center");
                Spawn(4, "all");
                Spawn(4, "all");
                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 4)
        {
            if (timer > 3f)
            {
                Spawn(5, "center");
                Spawn(3, "right");
                Spawn(3, "left");
                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 3)
        {
            if (timer > 1f)
            {
                Spawn(4, "all");
                Spawn(2, "center");

                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 2)
        {
            if (timer > 1f)
            {
                Spawn(3, "all");
                Spawn(2, "all");
                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60)
        {
            if (timer > 1.5)
            {
                Spawn(3, "right");
                Spawn(2, "left");
                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 30)
        {
            if (timer > 0.5)
            {
                Spawn(2, "center");
                timer = 0;
            }
        }
        else
        {
            if (timer > 1.5)
            {
                Spawn(2, "center");
                timer = 0;
            }
        }

    }

    void Spawn(int monsterType, string spawnDir)
    {
        switch (spawnDir)
        {
            case "right":
                GameObject enemy0 = GameManager.instance.pool.GetPool(monsterType);
                enemy0.transform.position = spawnPoint[Random.Range(1, 6)].position;
                break;
            case "left":
                GameObject enemy1 = GameManager.instance.pool.GetPool(monsterType);
                enemy1.transform.position = spawnPoint[Random.Range(11, spawnPoint.Length)].position;
                break;
            case "center":
                GameObject enemy2 = GameManager.instance.pool.GetPool(monsterType);
                enemy2.transform.position = spawnPoint[Random.Range(6, 11)].position;
                break;
            case "all":
            default:
                GameObject enemy3 = GameManager.instance.pool.GetPool(monsterType);
                enemy3.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
                break;
        }



    }

    void SpawnBoss()
    {
        if (GameObject.FindWithTag("Boss") == null)
        {
            if (GameManager.instance.GetTime() / (2 * 60) >= bossNum + 1)
            {
                GameObject boss = Instantiate(Bosses[bossNum++]);
                boss.gameObject.transform.position = new Vector3(0, 25, 0);

            }
        }
    }
}
