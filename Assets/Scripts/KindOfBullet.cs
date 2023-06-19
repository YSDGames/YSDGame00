using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KindOfBullet : MonoBehaviour
{
    public GameObject[] kindOfBullet;

    public static KindOfBullet instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var item in kindOfBullet)
        {
            int i = 0;
            kindOfBullet[i++].transform.rotation = Quaternion.AngleAxis(90, new Vector3(0, 0, 1));

        }
    }
   

    public GameObject Kind(int num)
    {
        return kindOfBullet[num];

        
    }


}
