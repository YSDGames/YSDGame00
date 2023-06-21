using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootBul : MonoBehaviour
{
    public float shootSpeed = 0.2f;

    public static ShootBul Instance = null;
    float timer = 0;
    int shootType = 0;


    int bulltype = 0;

    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //레벨에 따라 총알 종류, 쏘는 타입 결정.
        bulltype = (GameManager.instance.playerLevel - 1) / 2; //Player Level Start 1. bullNum Start 0.
        Mathf.Clamp(bulltype, 0, 11);

        shootType = (GameManager.instance.playerLevel) / 2;
        Mathf.Clamp(shootType, 0, 5);

        //레벨에따른 shotspeed
        if (GameManager.instance.playerLevel == 5)
            shootSpeed = 0.3f;
        if (GameManager.instance.playerLevel == 10)
            shootSpeed = 0.25f;
        if (GameManager.instance.playerLevel == 15)
            shootSpeed = 0.2f;


        if (timer > shootSpeed)
        {
            ShootType(shootType, bulltype);
            timer = 0;
        }


    }

    void ShootType(int shoottype, int bullet)
    {
        switch (shoottype)
        {
            case 0:
                Instantiate(KindOfBullet.instance.Kind(bullet), transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x + 0.075f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x - 0.075f, transform.position.y), Quaternion.identity);
                break;
            case 2:
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x + 0.15f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x - 0.15f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                break;
            case 3:
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x + 0.075f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x - 0.075f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x + 0.225f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x - 0.225f, transform.position.y), Quaternion.identity);
                break;
            default:
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x + 0.3f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x - 0.3f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x + 0.15f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x - 0.15f, transform.position.y), Quaternion.identity);
                Instantiate(KindOfBullet.instance.Kind(bullet), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                break;
        }


    }



}
