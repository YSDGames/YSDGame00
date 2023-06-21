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

    public PoolManager pool;
    public Player player;
    public GameObject mainCamera;
    public Transform expBar;
    public GameObject dieUI;
    public GameObject clearUI;


    public int playerLevel = 1;
    public float exp = 0;
    float timer = 0;
    float endTime = 12 * 60;
    List<int> expOfLevel;

    [SerializeField] Text time;
    [SerializeField] Text level;

    private void Awake()
    {

        instance = this;


        expOfLevel = new List<int>()
        {
            0,      //렙0
            10,
            15,
            20,
            40,
            50,      //5
            60,
            70,
            80,
            150,
            180,     //10
            210,
            240,
            270,
            350,      
            400,     //15
            450,
            500

        };

    }

    void Update()
    {
        // 타이머, 레벨s
        timer += Time.deltaTime;
        time.text = $"{(int)(endTime-timer) / 60:D2}:{(int)(endTime - timer) % 60:D2}";

        level.text = $"Level : {playerLevel}";

        // 0초시 게임종료
        if (timer <= 0)
        {
            timer = 0;

            GameOver();
        }

        LevelUp();
    }
   
    void LevelUp()
    {
        // 경험치, 경험치바
        if (exp >= expOfLevel[playerLevel])
        {
            exp -= expOfLevel[playerLevel];
            playerLevel += 1;
        }

        //최대레벨 초과시 레벨 그대로.
        if (playerLevel == expOfLevel.Count)
        {
            playerLevel -= 1;
        }

        //최대레벨 도달시 exp 0으로 고정, 아니면 경험치획득 표현
        if (playerLevel == expOfLevel.Count - 1)
            expBar.gameObject.GetComponent<Transform>().localScale = new Vector3(0, 1, 1);
        else
            expBar.gameObject.GetComponent<Transform>().localScale = new Vector3(exp / expOfLevel[playerLevel], 1, 1);

    }
    public float GetTime()
    {
        return timer;
    }

    public void GameClear()
    {
        Time.timeScale = 0;

        clearUI.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        dieUI.SetActive(true);

    }
}
