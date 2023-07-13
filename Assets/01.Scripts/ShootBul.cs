using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBul : MonoBehaviour
{
    public static ShootBul Instance = null;

    [SerializeField] public AudioClip soundShoot;
    public Image skillCoolDown;

    public float addDamege;
    public int addPiercingNum;
    public float baseShootSpeed;
    public float shootSpeed;
    float timer;

    [SerializeField] Text txtCoolTime;
    public float skillTimer;
    public float skillCoolTime;
    public int numBullets;
    public int bulltype;

    private void Awake()
    {
        Instance = this;

        baseShootSpeed = 0.5f;
        shootSpeed = baseShootSpeed;
        numBullets = 1;
        bulltype = 0;

        timer = 0;
        skillCoolTime = 20;
        skillTimer = 0;
    }


    void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameState.ing)
            return;

        SkillCoolTime();

        timer += Time.deltaTime;
        //SetShooting();
        if (timer > shootSpeed)
        {
            switch (GameManager.instance.shootType)
            {
                case 0:
                    ShootStright(numBullets, bulltype);
                    break;
                case 1:
                    ShootSpread(numBullets, bulltype, 15);
                    break;
                case 2:
                    ShootRotate(numBullets, bulltype);
                    break;
            }
            timer = 0;
        }
    }
    public void SkillCoolTime()
    {
        if (skillTimer <= 0) txtCoolTime.text = "";
        else if (skillTimer < 1) txtCoolTime.text = $"{skillTimer:N1}";
        else txtCoolTime.text = $"{(int)skillTimer}";

        skillTimer -= Time.deltaTime;
        skillTimer = skillTimer <= 0 ? 0 : skillTimer;

        skillCoolDown.fillAmount = skillTimer / skillCoolTime;
    }
    public void ActiveSkill()
    {
        if (skillTimer <= 0)
        {
            switch (GameManager.instance.shootType)
            {
                case 0:
                    SkillStright(100, 15);            //*1배
                    break;
                case 1:
                    StartCoroutine(SkillSpread(30, 0.05f)); // * bullNum * 30배
                    break;
                case 2:
                default:
                    SkillRotate(40, 0.05f);  // * 3 *40 배  20,0.1  40,0.05
                    break;
            }
            skillTimer = skillCoolTime;
        }
    }
    void UpdateStat(GameObject b)
    {
        bullets bullet = b.GetComponent<bullets>();

        bullet.rotTrigger = false;
        bullet.rotTimer = 0;
        bullet.deg = 0;
        bullet.bulletSpeed = 6f;
        bullet.circleR = 1f;

        bullet.totalDamage = bullet.damage + addDamege;
        bullet.totalPiercingNum = bullet.piercingNum + addPiercingNum;
    }

    void ShootStright(float numBullets, int bulltype)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b = GameManager.instance.bulletPool.GetPool(bulltype, new Vector2(transform.position.x - 0.075f * (numBullets - 1) + i * 0.15f, transform.position.y), Quaternion.identity);
            b.transform.rotation = Quaternion.Euler(0, 0, 90);
            UpdateStat(b);

            SoundManager.instance.SFXPlay("Shoot", soundShoot, 0.3f);
        }

    }

    void ShootSpread(float numBullets, int bulltype, float rad)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b = GameManager.instance.bulletPool.GetPool(bulltype, new Vector2(transform.position.x - 0.025f * (numBullets - 1) + i * 0.05f, transform.position.y), Quaternion.identity);
            b.transform.rotation = Quaternion.Euler(0, 0, (90 + (((numBullets - 1) / 2) * rad) - (rad * i)));
            UpdateStat(b);

            SoundManager.instance.SFXPlay("Shoot", soundShoot, 0.3f);

        }

    }

    void ShootRotate(float numBullets, int bulltype)
    {
        StartCoroutine(DelayShoot(numBullets, bulltype));
    }

    IEnumerator DelayShoot(float numBullets, int bulltype)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b = GameManager.instance.bulletPool.GetPool(bulltype, transform.position, Quaternion.Euler(0, 0, 90));
            UpdateStat(b);
            b.gameObject.GetComponent<bullets>().rotTrigger = true;


            SoundManager.instance.SFXPlay("Shoot", soundShoot, 0.3f);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator DelayShoot(float numBullets, int bulltype, float bullSpeed, float shootSpeed, float r)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b = GameManager.instance.bulletPool.GetPool(bulltype, transform.position, Quaternion.Euler(0, 0, 90));
            UpdateStat(b);
            b.GetComponent<bullets>().bulletSpeed = bullSpeed;
            b.GetComponent<bullets>().circleR = r;
            b.gameObject.GetComponent<bullets>().rotTrigger = true;


            SoundManager.instance.SFXPlay("Shoot", soundShoot, 0.2f);
            yield return new WaitForSeconds(shootSpeed);
        }
    }

    void SkillStright(float damMulti, float scale)
    {
        GameObject b = Instantiate(GameManager.instance.bulletPool.pool[bulltype], transform.position, Quaternion.identity);

        UpdateStat(b);
        b.transform.localScale = Vector3.one * scale;

        //b.GetComponentInChildren<ParticleSystem>().startSize = 0.4f * scale;  //왜 안되지
        b.GetComponent<bullets>().totalDamage = b.GetComponent<bullets>().totalDamage * damMulti;
        b.GetComponent<bullets>().totalPiercingNum = -1; // 무한
        b.GetComponent<bullets>().bulletSpeed = 2f;
    }
    IEnumerator SkillSpread(int Num, float shootSpeed)
    {
        for (int i = 0; i < Num; i++)
        {
            ShootSpread(numBullets, bulltype, 15);
            yield return new WaitForSeconds(shootSpeed);
        }
    }

    void SkillRotate(int Num, float shootSpeed)
    {
        StartCoroutine(DelayShoot(Num, bulltype, 7, shootSpeed, 2));
        StartCoroutine(DelayShoot(Num, bulltype, 7, shootSpeed, 3));
        StartCoroutine(DelayShoot(Num, bulltype, 7, shootSpeed, 4));
    }

    //void SetShooting()
    //{
    //    //레벨에 따라 총알 종류, 쏘는 타입 결정.
    //    bulltype = (GameManager.instance.playerLevel - 1) / 2; //Player Level Start 1. bullNum Start 0.
    //    Mathf.Clamp(bulltype, 0, 11);
    //    numBullets = (GameManager.instance.playerLevel) / 2 + 1;  //1렙은 한개 2렙 두개 이런식.
    //    Mathf.Clamp(numBullets, 0, 5);

    //    //레벨에따른 shotspeed
    //    if (GameManager.instance.playerLevel == 5)
    //        shootSpeed = 0.3f;
    //    if (GameManager.instance.playerLevel == 10)
    //        shootSpeed = 0.25f;
    //    if (GameManager.instance.playerLevel == 15)
    //        shootSpeed = 0.2f;
    //}
}






