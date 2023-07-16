using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    public GameObject mainCamera;
    public Item playerOrb;
    public Player player;
    public int playerLevel;
    public GameObject dieUI;
    public GameObject clearUI;
    public UI_LevelUp uiLevelUp;
    public bool gamePadMode = true;

    public GameObject moveGuide;
    public GameObject joyBG;
    public GameObject joyStick;

    public Vector3 bossPosition;

    public float exp;
    List<int> expOfLevel;
    public RectTransform expBar;
    float expBarWidth;

    float timer;
    float endTime = 12 * 60 + 1;
    public int shootType;

    [SerializeField] Text time;
    [SerializeField] Text level;

    public GameState gameState;
    public enum GameState
    {
        menu,
        ing,
        stay
    }
    void Init()
    {
        playerLevel = 1;
        timer = 0;
        exp = 0;
        shootType = 0;
        expBarWidth = 2285;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;

        expOfLevel = new List<int>()
        {
            0,      //렙0
            3,
            8,
            15,
            24,
            40,      //5
            60,
            80,
            110,
            140,
            180,     //10
            240,
            300,
            360,
            430,
            500,     //15
            600,
            750,
            900,
            1100,
            1300,     //20
            1600,
            1900,
            2400,
            3000,
            3600,      //25
            4200,
            4800,
            5400,
            6000,
            6600       //30

        };

    }

    private void Start()
    {
        Init();
        GameStart();
        GamePadMode(PlayerPrefs.GetInt("ControlMode"));
    }

    void Update()
    {

        if (gameState != GameState.ing)
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

    void GamePadMode(int mode)
    {
        if (mode == 0)
        {
            joyBG.SetActive(true);
            moveGuide.SetActive(false);
        }
        else
        {
            joyBG.SetActive(false);
            moveGuide.SetActive(true);
        }
    }
    void LevelUp()
    {
        // 경험치
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

        //경험치바. 최대레벨 도달시 exp 0으로 고정, 아니면 경험치획득 표현
        if (playerLevel == expOfLevel.Count - 1)
            expBar.offsetMax = new Vector2(-expBarWidth, expBar.offsetMax.y);
        else
            expBar.offsetMax = new Vector2(-expBarWidth * (1 - exp / expOfLevel[playerLevel]), expBar.offsetMax.y);
        //expBar.localScale = new Vector3(exp / expOfLevel[playerLevel], 1, 1);

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
        StartCoroutine(GoMenu());
    }
    IEnumerator GoMenu()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("MainMenu");
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

    public void MakeSound()
    {
        SoundManager.instance.SFXPlay("Hit", playerOrb.clip, 0.4f);

    }

    public void MakeEffect(Collider2D collposition)
    {
        StartCoroutine(GetEffect(collposition));
    }

    IEnumerator GetEffect(Collider2D collposition)
    {
        GameObject _effect = effectPool.GetPool(playerOrb.effID, collposition.transform.position, playerOrb.data.effect.transform.rotation);
        yield return new WaitForSeconds(1.5f);

        if (_effect != null)
            _effect.SetActive(false);
    }

}
