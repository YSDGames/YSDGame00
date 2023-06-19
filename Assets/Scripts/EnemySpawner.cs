using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    float timer = 0;
    
    private void Awake()
    {
        
        spawnPoint = GetComponentsInChildren<Transform>();
        
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.02)
        {

            Spawn();
            timer = 0;
        }

    }

    public void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.GetEnemy(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }



}
