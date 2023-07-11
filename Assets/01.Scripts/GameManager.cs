using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject mainCamera;
    public Item playerOrb;
    public Player player;
    public int playerLevel;
    public GameObject dieUI;
    public GameObject clearUI;
    public LevelUp uiLevelUp;

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
            0,      //��0
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
        if (gameState != GameState.ing)
            return;

        // Ÿ�̸�, ����s
        timer += Time.deltaTime;
        time.text = $"{(int)(endTime - timer) / 60:D2}:{(int)(endTime - timer) % 60:D2}";

        level.text = $"Level : {playerLevel}";

        // 0�ʽ� ��������
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
        // ����ġ
        if (exp >= expOfLevel[playerLevel])
        {
            exp -= expOfLevel[playerLevel];
            playerLevel += 1;
            player.nowHp += 1;
            uiLevelUp.Show();
        }

        //�ִ뷹�� �ʰ��� ���� �״��.
        if (playerLevel == expOfLevel.Count)
        {
            playerLevel -= 1;
        }

        //����ġ��. �ִ뷹�� ���޽� exp 0���� ����, �ƴϸ� ����ġȹ�� ǥ��
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
        //������ �����Ҷ��� Ŵ.
        if (GameObject.FindWithTag("Boss") == null)
        {
            BossDirc.instance.gameObject.transform.localScale = Vector3.zero;
        }
        else
        {
            //ī�޶�ȿ����� ��.
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
        SoundManager.instance.SFXPlay("Hit", GameManager.instance.playerOrb.clip, 0.4f);

    }

    public void MakeEffect(Collider2D collposition)
    {
        StartCoroutine(GetEffect(collposition));
    }

    IEnumerator GetEffect(Collider2D collposition)
    {
        GameObject _effect = GameManager.instance.effectPool.GetPool(GameManager.instance.playerOrb.effID, collposition.transform.position, GameManager.instance.playerOrb.data.effect.transform.rotation);
        yield return new WaitForSeconds(1.5f);

        if (_effect != null)
            _effect.SetActive(false);
    }

}
