using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullets : MonoBehaviour
{
    public float bulletSpeed = 3f;


    public GameObject hitEffect;
    //[SerializeField] float shootSpeed = 0.2f;
    public float damage = 1f;
    [HideInInspector] public float totalDamage;

    public int piercingNum = 1;
    [HideInInspector] public int totalPiercingNum;

    public bool rotTrigger;
    public float rotTimer;
    public float rotLifeTime;

    public float deg;
    public float circleR;
    Transform[] childTrans;
    private void Awake()
    {
        circleR = 1;
        deg = 0;
        //ShootBul.Instance.shootSpeed = shootSpeed;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        totalDamage = damage;
        totalPiercingNum = piercingNum;

        rotTrigger = false;
        rotLifeTime = 10;

    }

    private void Start()
    {
        Transform[] childTrans = GetComponentsInChildren<Transform>();


        //Item Orb = GameObject.Find("UI/LevelUp/Panel/Items").GetComponentInChildren<Item>();
        if (GameManager.instance.playerOrb.aura != null)
            Instantiate(GameManager.instance.playerOrb.aura, childTrans[1]);
    }

  
    private void Update()
    {
        

        if (rotTrigger) Rotate(25);
        else Move();

        DestroyBullet();
    }

    void DestroyBullet()
    {
        rotTimer += Time.deltaTime;

        if (rotTimer > rotLifeTime && rotTrigger) gameObject.SetActive(false);

        if (totalPiercingNum == 0)
        {
            gameObject.SetActive(false);
        }

        //카메라밖으로 나가면 총알삭제
        if (transform.position.y > GameManager.instance.mainCamera.transform.position.y + Camera.main.orthographicSize + 3)
        {
            gameObject.SetActive(false);
        }
    }
    void Move()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    void Rotate(float rotSpeed)
    {

        deg += Time.deltaTime * bulletSpeed * rotSpeed;
        if (deg < 360)
        {
            var rad = Mathf.Deg2Rad * (deg);
            var x = circleR * Mathf.Sin(rad);
            var y = circleR * Mathf.Cos(rad);
            transform.position = GameManager.instance.player.transform.position + new Vector3(x, y);
            transform.rotation = Quaternion.Euler(0, 0, -1 * deg);
        }
        else
        {
            deg = 0;
        }

    }

}


