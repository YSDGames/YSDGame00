using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootBul : MonoBehaviour
{
    public static ShootBul Instance = null;

    [SerializeField] public AudioClip soundShoot;

    public float shootSpeed = 0.2f;
    float timer = 0;
    int numBullets = 0;


    int bulltype = 0;

    private void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        timer += Time.deltaTime;

        SetShooting();

        if (timer > shootSpeed)
        {
            switch (GameManager.instance.shootType)
            {
                case 0:
                    ShootStright(numBullets, bulltype);
                    break;
                case 1:
                    ShootSpread(numBullets, bulltype ,15);
                    break;
            }

            SoundManager.instance.SFXPlay("Shoot", soundShoot, 0.5f);

            timer = 0;

        }


    }
    
    void SetShooting()
    {
        //레벨에 따라 총알 종류, 쏘는 타입 결정.
        bulltype = (GameManager.instance.playerLevel - 1) / 2; //Player Level Start 1. bullNum Start 0.
        Mathf.Clamp(bulltype, 0, 11);
        numBullets = (GameManager.instance.playerLevel) / 2 + 1;  //1렙은 한개 2렙 두개 이런식.
        Mathf.Clamp(numBullets, 0, 5);

        //레벨에따른 shotspeed
        if (GameManager.instance.playerLevel == 5)
            shootSpeed = 0.3f;
        if (GameManager.instance.playerLevel == 10)
            shootSpeed = 0.25f;
        if (GameManager.instance.playerLevel == 15)
            shootSpeed = 0.2f;
    }


    void ShootStright(float numBullets, int bulltype)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameManager.instance.bulletPool.GetPool(bulltype, new Vector2(transform.position.x - 0.075f* (numBullets-1) + i*0.15f, transform.position.y), Quaternion.identity);

        }
    }

    void ShootSpread(float numBullets,  int bulltype, float rad)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b = GameManager.instance.bulletPool.GetPool(bulltype, new Vector2(transform.position.x - 0.025f * (numBullets - 1) + i * 0.05f, transform.position.y), Quaternion.identity);
            b.transform.rotation = Quaternion.Euler(0, 0, (90 + (((numBullets-1) / 2) * rad) - (rad * i)));

        }
    }


}

    




