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

    public int playerLevel = 1;

    public float exp = 0;
    List<int> expOfLevel;



    [SerializeField] Text time;
    [SerializeField] Text level;
    public Transform expBar;

    float timer = 0;

    private void Awake()
    {
        
        instance = this;
        DontDestroyOnLoad(gameObject);

        expOfLevel = new List<int>() 
        { 
            0,      //��0
            5,
            5,
            5,
            5,
            5,      //5
            5,      
            5,      
            5,
            5,
            5,      //10
            5,
            5,
            5,
            5,
            5,      //15
            5,      
            5,

        };

    }
    
    void Update()
    {
        // Ÿ�̸�, ����
        timer += Time.deltaTime;
        time.text = $"{(int)timer/60:D2}:{(int)timer%60:D2}";

        level.text = $"Level : {playerLevel}";
        

        // ����ġ, ����ġ��
        if (exp >= expOfLevel[playerLevel])             
        {
            exp -= expOfLevel[playerLevel];
            playerLevel += 1;
        }

        if (playerLevel == expOfLevel.Count)
        {
            playerLevel -= 1;
        }

        if(playerLevel == expOfLevel.Count-1)
            expBar.gameObject.GetComponent<Transform>().localScale = new Vector3(0, 1, 1);
        else
          expBar.gameObject.GetComponent<Transform>().localScale = new Vector3(exp / expOfLevel[playerLevel], 1, 1);
    }

    public float GetTime()
    {
        return timer;
    }
}
