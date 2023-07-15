using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBul : MonoBehaviour
{
    public static ShootBul Instance = null;

    [SerializeField] public AudioClip soundShoot;
    public GameObject skill2Right;
    public GameObject skill2Left;
    public Image skillCoolDown;
    public Image skill2Active;
    bool guideSkillTigger;
    [SerializeField] Text txtCoolTime;
    [SerializeField] Text txtSkill2Num;


    [HideInInspector] public float addDamege;
    [HideInInspector] public int addPiercingNum;
    public float baseShootSpeed;
    [HideInInspector] public float shootSpeed;
    float timer;

    [HideInInspector] public float skillTimer;
    [HideInInspector] public float skillCoolTime;
    [HideInInspector] public int skill2Num;

    [HideInInspector] public int numBullets;
    [HideInInspector] public int bulltype;

    Scanner scanner;

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

        skill2Num = 3;
        guideSkillTigger = false;
        scanner = GetComponentInParent<Scanner>();
    }


    void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameState.ing)
            return;

        Skill1CoolTime();

        Skill2Count();

        ActiveSkill2();

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
                case 3:
                    ShootGuide(numBullets, bulltype);
                    break;

            }
            timer = 0;
        }
    }
    public void Skill1CoolTime()
    {
        if (skillTimer <= 0) txtCoolTime.text = "";
        else if (skillTimer < 1) txtCoolTime.text = $"{skillTimer:N1}";
        else txtCoolTime.text = $"{(int)skillTimer}";

        skillTimer -= Time.deltaTime;
        skillTimer = skillTimer <= 0 ? 0 : skillTimer;

        skillCoolDown.fillAmount = skillTimer / skillCoolTime;
    }
    public void OnActiveSkill1()
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
                    SkillRotate(10, 0.05f);  // * 3 *40 배  20,0.1  40,0.05
                    break;
                case 3:
                default:
                    StartCoroutine(SkillGuide());
                    break;
            }
            skillTimer = skillCoolTime;
        }
    }

    void Skill2Count()
    {
        if (skill2Num == 0) skill2Active.gameObject.SetActive(true);
        else if (skill2Num > 0) skill2Active.gameObject.SetActive(false);
        txtSkill2Num.text = $"{skill2Num}";
    }
    public void OnSkill2Click()
    {
        if (!skill2Right.activeSelf && !skill2Left.activeSelf)
        {
            skill2Right.SetActive(true);
            skill2Left.SetActive(true);

            skill2Num--;
        }
    }
    void ActiveSkill2()
    {
        if (skill2Right.activeSelf || skill2Left.activeSelf)
        {
            // 스킬의 움직임
            if (skill2Right.transform.localScale.y < 10 && skill2Right.activeSelf)
            {
                skill2Right.transform.localScale += 2 * Vector3.up * Time.deltaTime * 8;
                skill2Right.transform.localPosition += Vector3.up * Time.deltaTime * 8;
            }
            else if (skill2Right.transform.localScale.y >= 10 && skill2Right.activeSelf)
            {
                skill2Right.transform.SetParent(null);
                skill2Right.transform.position += Vector3.right * Time.deltaTime * 8;
            }

            if (skill2Left.transform.localScale.y < 10 && skill2Left.activeSelf)
            {
                skill2Left.transform.localScale += 2 * Vector3.up * Time.deltaTime * 8;
                skill2Left.transform.localPosition += Vector3.up * Time.deltaTime * 8;
            }
            else if (skill2Left.transform.localScale.y >= 10 && skill2Left.activeSelf)
            {
                skill2Left.transform.SetParent(null);
                skill2Left.transform.position += Vector3.left * Time.deltaTime * 8;
            }

            //스킬 초기화
            if ((skill2Right.transform.position.x - Camera.main.transform.position.x) - CameraMovment.instance.width > 2 && skill2Right.activeSelf)
            {
                skill2Right.transform.SetParent(GameObject.Find("Player/Skill2").transform);

                skill2Right.transform.localPosition = Vector3.right * 0.0125f;
                skill2Right.transform.localScale = new Vector3(0.025f, 0.025f, 0);
                skill2Right.SetActive(false);
            }

            if ((skill2Left.transform.position.x - Camera.main.transform.position.x) + CameraMovment.instance.width < -2 && skill2Left.activeSelf)
            {
                skill2Left.transform.SetParent(GameObject.Find("Player/Skill2").transform);

                skill2Left.transform.localPosition = Vector3.left * 0.0125f;
                skill2Left.transform.localScale = new Vector3(0.025f, 0.025f, 0);
                skill2Left.SetActive(false);
            }
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
        bullet.transform.localScale = new Vector3(bullet.baseScale, bullet.baseScale, 1);

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
    //===========================================GuideShoot===============================================
    void ShootGuide(float numBullets, int bulltype)
    {
        StartCoroutine(GuideDelayShoot(numBullets, bulltype));
    }
    IEnumerator GuideDelayShoot(float numBullets, int bulltype)
    {
        for (int i = 0; i < numBullets; i++)
        {
            if (scanner.nearestTargets[i] != null)
            {
                GameObject b = GameManager.instance.bulletPool.GetPool(bulltype);
                b.transform.position = transform.position;
                b.transform.rotation = LookEnemy(scanner.nearestTargets[i].transform);
                b.transform.Rotate(new Vector3(0, 0, 90));
                UpdateStat(b);

                if (guideSkillTigger)
                {
                    b.GetComponent<bullets>().totalDamage *= 3;
                    b.GetComponent<bullets>().totalPiercingNum += 3;
                    b.transform.localScale *= 5; 
                }

                SoundManager.instance.SFXPlay("Shoot", soundShoot, 0.3f);
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                GameObject b = GameManager.instance.bulletPool.GetPool(bulltype);
                b.transform.position = transform.position;
                b.transform.rotation = Quaternion.Euler(0, 0, 90);
                UpdateStat(b);

                if (guideSkillTigger)
                {
                    b.GetComponent<bullets>().totalDamage *= 3;
                    b.GetComponent<bullets>().totalPiercingNum += 3;
                    b.transform.localScale *= 5;
                }

                SoundManager.instance.SFXPlay("Shoot", soundShoot, 0.3f);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
    public Quaternion LookEnemy(Transform enemy)
    {
        Vector2 direction = new Vector2();

        direction = (enemy.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 270, Vector3.forward);

        return q;
    }
    //===========================================RotateShoot==========================================================================

    void ShootRotate(float numBullets, int bulltype)
    {
        StartCoroutine(RotateDelayShoot(numBullets, bulltype));
    }

    IEnumerator RotateDelayShoot(float numBullets, int bulltype)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b = GameManager.instance.bulletPool.GetPool(bulltype, transform.position, Quaternion.Euler(0, 0, 90));
            UpdateStat(b);
            b.GetComponent<bullets>().circleR = 1 + 0.2f * i;
            //b.GetComponent<bullets>().rotSpeed = 25 + 0.05f * i;
            b.gameObject.GetComponent<bullets>().rotTrigger = true;


            SoundManager.instance.SFXPlay("Shoot", soundShoot, 0.3f);
            yield return new WaitForSeconds(0.3f); //첨값 0.3
        }
    }

    IEnumerator RotateDelayShoot(float numBullets, int bulltype, float bullSpeed, float shootSpeed, float r, float startDeg)
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject b = GameManager.instance.bulletPool.GetPool(bulltype, transform.position, Quaternion.Euler(0, 0, 90));
            UpdateStat(b);
            b.GetComponent<bullets>().bulletSpeed = bullSpeed;
            b.GetComponent<bullets>().circleR = r;
            b.gameObject.GetComponent<bullets>().rotTrigger = true;
            b.GetComponent<bullets>().deg = startDeg;

            SoundManager.instance.SFXPlay("Shoot", soundShoot, 0.2f);
            yield return new WaitForSeconds(shootSpeed);
        }
    }
    //==================================================================================================================================================
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
        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(RotateDelayShoot(Num, bulltype, 7, shootSpeed, 2, 0 + 90 * i));
            StartCoroutine(RotateDelayShoot(Num, bulltype, 7, shootSpeed, 3, 0 + 90 * i));
            StartCoroutine(RotateDelayShoot(Num, bulltype, 7, shootSpeed, 4, 0 + 90 * i));
        }

    }

    IEnumerator SkillGuide()
    {
        guideSkillTigger = true;
        yield return new WaitForSeconds(5);
        guideSkillTigger = false;
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






