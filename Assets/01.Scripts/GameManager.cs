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

    public Orb orbManager;
    public Orb playerOrb;
    public Player player;
    public GameObject mainCamera;
    public Transform expBar;
    public GameObject dieUI;
    public GameObject clearUI;

    public Vector3 bossPosition;
    public int playerLevel;
    public float exp;

    float timer;
    float endTime = 12 * 60;
    public int shootType;
    List<int> expOfLevel;

    [SerializeField] Text time;
    [SerializeField] Text level;

    void Init()
    {
        playerLevel = 1;
        timer = 0;
        exp = 0;
        shootType = 2;

        playerOrb = orbManager.GetComponent<FireOrb>();
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

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
    }

    void Update()
    {
        
        // Ÿ�̸�, ����s
        timer += Time.deltaTime;
        time.text = $"{(int)(endTime-timer) / 60:D2}:{(int)(endTime - timer) % 60:D2}";

        level.text = $"Level : {playerLevel}";

        // 0�ʽ� ��������
        if (timer <= 0)
        {
            timer = 0;
            GameOver();
        }
        LevelUp();
        GetBossDir();
    }
   
    void LevelUp()
    {
        // ����ġ, ����ġ��
        if (exp >= expOfLevel[playerLevel])
        {
            exp -= expOfLevel[playerLevel];
            playerLevel += 1;
            player.nowHp += 1;
        }

        //�ִ뷹�� �ʰ��� ���� �״��.
        if (playerLevel == expOfLevel.Count)
        {
            playerLevel -= 1;
        }

        //�ִ뷹�� ���޽� exp 0���� ����, �ƴϸ� ����ġȹ�� ǥ��
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

    void GetBossDir()
    {
        //������ �����Ҷ��� Ŵ.
        if (GameObject.FindWithTag("Boss")==null)
        {
            BossDirc.instance.gameObject.SetActive(false);
        }
        else
        {
            //ī�޶�ȿ����� ��.
            bossPosition = GameObject.FindWithTag("Boss").transform.position;
            Vector3 CameraCheckBoss = bossPosition - mainCamera.transform.position;

            if (CameraCheckBoss.x < BossDirc.instance.staticMaxX + 4 && CameraCheckBoss.x > BossDirc.instance.staticMinX - 4 && CameraCheckBoss.y < BossDirc.instance.staticMaxY + 4 && CameraCheckBoss.y > BossDirc.instance.staticMinY - 4)
            {
                BossDirc.instance.gameObject.SetActive(false);
            }
            else
            {
                BossDirc.instance.gameObject.SetActive(true);
            }
        }
    }

}
