using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public EnemyPool pool;
    public spaceship player;

    public int playerLevel = 1;

    public float exp = 0;
    int[] expOfLevel;



    [SerializeField] Text time;
    [SerializeField] Text level;
    public Transform expBar;

    float timer = 0;

    private void Awake()
    {
        
        instance = this;
        DontDestroyOnLoad(gameObject);


        expOfLevel = new int[9];

        expOfLevel[0] = 0;
        expOfLevel[1] = 5;
        expOfLevel[2] = 10;
        expOfLevel[3] = 20;
        expOfLevel[4] = 25;
        expOfLevel[5] = 30;
        expOfLevel[6] = 35;
        expOfLevel[7] = 50;
        expOfLevel[8] = 60;

    }

    void Start()
    {

    }


    void Update()
    {
        // 타이머
        timer += Time.deltaTime;

        level.text = $"Level : {playerLevel}";
        time.text = $"{timer:N2}";

        // 경험치, 경험치바
        if (exp >= expOfLevel[playerLevel])
        {
            exp -= expOfLevel[playerLevel];
            playerLevel += 1;
        }

        expBar.gameObject.GetComponent<Transform>().localScale = new Vector3(exp / expOfLevel[playerLevel], 1, 1);

    }
}
