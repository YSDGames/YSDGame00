using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    [SerializeField] GameObject[] Bosses;
    int bossNum;
    float timer = 0;

    bool trigger1 = true;
    bool trigger2 = true;
    bool trigger3 = true;


    enum MonsterName
    {
        Lv0,
        Lv1,
        Lv2,
        Lv3,
        Lv4,
        Lv5,
        Lv6
    }


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
                Spawn((int)MonsterName.Lv5, "all");
                Spawn((int)MonsterName.Lv5, "all");
                Spawn((int)MonsterName.Lv1, "left");
                Spawn((int)MonsterName.Lv1, "left");

                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 8)
        {
            if (timer > 3f)
            {
                Spawn((int)MonsterName.Lv5, "center");
                Spawn((int)MonsterName.Lv4, "right");
                Spawn((int)MonsterName.Lv4, "left");
                Spawn((int)MonsterName.Lv4, "left");
                Spawn((int)MonsterName.Lv1, "left");
                Spawn((int)MonsterName.Lv1, "left");
                Spawn((int)MonsterName.Lv1, "left");
                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 7)
        {
            if (timer > 3f)
            {
                Spawn((int)MonsterName.Lv4, "center");
                Spawn((int)MonsterName.Lv4, "center");
                Spawn((int)MonsterName.Lv3, "right");
                Spawn((int)MonsterName.Lv3, "all");
                Spawn((int)MonsterName.Lv3, "left");
                Spawn((int)MonsterName.Lv1, "left");
                Spawn((int)MonsterName.Lv1, "left");


                timer = 0;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 6)
        {
            if (timer > 3f)
            {
                Spawn((int)MonsterName.Lv4, "center");
                Spawn((int)MonsterName.Lv3, "all");
                Spawn((int)MonsterName.Lv3, "all");
                Spawn((int)MonsterName.Lv2, "all");
                Spawn((int)MonsterName.Lv2, "all");
                Spawn((int)MonsterName.Lv2, "all");
                timer = 0;
            }

            
        }
        else if (GameManager.instance.GetTime() > 60 * 5)
        {
            if (timer > 3f)
            {
                Spawn((int)MonsterName.Lv3, "center");
                Spawn((int)MonsterName.Lv2, "all");
                Spawn((int)MonsterName.Lv2, "all");
                Spawn((int)MonsterName.Lv2, "all");
                Spawn((int)MonsterName.Lv1, "all");

                timer = 0;
            }

            if (trigger3)
            {
                Spawn((int)MonsterName.Lv6, "right");
                Spawn((int)MonsterName.Lv6, "right");
                Spawn((int)MonsterName.Lv6, "right");
                Spawn((int)MonsterName.Lv6, "right");
                Spawn((int)MonsterName.Lv6, "right");
                Spawn((int)MonsterName.Lv6, "right");
                Spawn((int)MonsterName.Lv6, "right");
                Spawn((int)MonsterName.Lv6, "right");
                Spawn((int)MonsterName.Lv6, "right");
                Spawn((int)MonsterName.Lv6, "right");

                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");

                trigger3 = false;
            }
        }
        else if (GameManager.instance.GetTime() > 60 * 4)
        {
            if (timer > 3f)
            {
                Spawn((int)MonsterName.Lv3, "center");
                Spawn((int)MonsterName.Lv1, "right");
                Spawn((int)MonsterName.Lv2, "center");
                Spawn((int)MonsterName.Lv1, "left");
                timer = 0;
            }

            
        }
        else if (GameManager.instance.GetTime() > 60 * 3)
        {
            if (timer > 1f)
            {
                Spawn((int)MonsterName.Lv2, "all");
                Spawn((int)MonsterName.Lv0, "center");

                timer = 0;
            }

            if (trigger2)
            {
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");

                trigger2 = false;
            }

        }
        else if (GameManager.instance.GetTime() > 60 * 2)
        {
            if (timer > 1f)
            {
                Spawn((int)MonsterName.Lv1, "all");
                Spawn((int)MonsterName.Lv0, "all");
                timer = 0;
            }

            
        }
        else if (GameManager.instance.GetTime() > 60)
        {
            if (timer > 1.5)
            {
                Spawn((int)MonsterName.Lv1, "right");
                Spawn((int)MonsterName.Lv0, "left");
                timer = 0;
            }

            //=====================================================한번소환
            if (trigger1)
            {
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");
                Spawn((int)MonsterName.Lv6, "left");

                trigger1 = false;
            }
        }
        else if (GameManager.instance.GetTime() > 30)
        {
            if (timer > 0.5)
            {
                Spawn((int)MonsterName.Lv0, "center");
                timer = 0;
            }
        }
        else
        {
            if (timer > 1.5)
            {
                Spawn((int)MonsterName.Lv0, "center");
                timer = 0;
            }
        }
    }

    void Spawn(int monsterType, string spawnDir)
    {
        switch (spawnDir)
        {
            case "right":
                GameObject enemy0 = GameManager.instance.enemyPool.GetPool(monsterType);
                enemy0.transform.position = spawnPoint[Random.Range(1, 6)].position;
                break;
            case "left":
                GameObject enemy1 = GameManager.instance.enemyPool.GetPool(monsterType);
                enemy1.transform.position = spawnPoint[Random.Range(11, spawnPoint.Length)].position;
                break;
            case "center":
                GameObject enemy2 = GameManager.instance.enemyPool.GetPool(monsterType);
                enemy2.transform.position = spawnPoint[Random.Range(6, 11)].position;
                break;
            case "all":
            default:
                GameObject enemy3 = GameManager.instance.enemyPool.GetPool(monsterType);
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
