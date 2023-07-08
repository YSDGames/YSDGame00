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

    public PoolManager enemyPool;
    public PoolManager bulletPool;
    public PoolManager auraPool;
    public PoolManager effectPool;
    public PoolManager ballPool;

    public GameObject gameStart;
    public Item playerOrb;
    public Player player;
    public GameObject mainCamera;
    public Transform expBar;
    public GameObject dieUI;
    public GameObject clearUI;
    public LevelUp uiLevelUp;

    public Vector3 bossPosition;
    public int playerLevel;
    public float exp;

    float timer;
    float endTime = 12 * 60 + 1;
    public int shootType;
    List<int> expOfLevel;

    [SerializeField] Text time;
    [SerializeField] Text level;

    public GameState gameState;
    public enum GameState
    {
        ing,
        stay
    }
    void Init()
    {
        playerLevel = 1;
        timer = 0;
        exp = 0;
        shootType = 0;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        expOfLevel = new List<int>()
        {
            0,      //렙0
            5,
            10,
            20,
            40,
            70,      //5
            90,
            110,
            140,
            170,
            200,     //10
            210,
            240,
            270,
            350,
            400,     //15
            450,
            500,
            550,
            600,
            700,     //20
            800,
            900


        };

    }

    private void Start()
    {
        Init();
        GameStart();
    }

    void Update()
    {
        if (gameState == GameState.stay)
            return;
        // 타이머, 레벨s
        timer += Time.deltaTime;
        time.text = $"{(int)(endTime - timer) / 60:D2}:{(int)(endTime - timer) % 60:D2}";

        level.text = $"Level : {playerLevel}";

        // 0초시 게임종료
        if (endTime <= timer)
        {
            timer = endTime;
            GameOver();
        }
        LevelUp();
        GetBossDir();
    }

    void LevelUp()
    {
        // 경험치, 경험치바
        if (exp >= expOfLevel[playerLevel])
        {
            exp -= expOfLevel[playerLevel];
            playerLevel += 1;
            player.nowHp += 1;
            uiLevelUp.Show();
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
        gameState = GameState.stay;
        clearUI.SetActive(true);
    }

    public void GameStart()
    {
        gameStart.SetActive(true);
        gameState = GameState.stay;
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameState = GameState.stay;
        dieUI.SetActive(true);
    }

    void GetBossDir()
    {
        //보스가 존재할때만 킴.
        if (GameObject.FindWithTag("Boss") == null)
        {
            BossDirc.instance.gameObject.transform.localScale = Vector3.zero;
        }
        else
        {
            //카메라안에오면 끔.
            bossPosition = GameObject.FindWithTag("Boss").transform.position;
            Vector3 CameraCheckBoss = bossPosition - mainCamera.transform.position;

            if (CameraCheckBoss.x < BossDirc.instance.staticMaxX + 4 && CameraCheckBoss.x > BossDirc.instance.staticMinX - 4 && CameraCheckBoss.y < BossDirc.instance.staticMaxY + 4 && CameraCheckBoss.y > BossDirc.instance.staticMinY - 4)
            {
                BossDirc.instance.gameObject.transform.localScale = Vector3.zero;
            }
            else
            {
                BossDirc.instance.gameObject.transform.localScale = Vector3.one;
            }
        }
    }


}
