using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullets : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 3f;
   

    public GameObject hitEffect;
    //[SerializeField] float shootSpeed = 0.2f;
    public float damage = 1f;
    [HideInInspector]public float totalDamage;

    public int piercingNum = 1;
    [HideInInspector] public int totalPiercingNum;

    public bool rotTrigger;

    private void Awake()
    {
        //ShootBul.Instance.shootSpeed = shootSpeed;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        totalDamage = damage;
        totalPiercingNum = piercingNum;
        rotTrigger = false;

    }

    private void Start()
    {
        Transform[] childTrans = GetComponentsInChildren<Transform>();
        if (GameManager.instance.playerOrb.aura)
            Instantiate(GameManager.instance.playerOrb.aura, childTrans[1]);
    }

    private void Update()
    {
        if (!rotTrigger) Rotate();
        else Rotate();
        DestroyBullet();
    }

    void DestroyBullet()
    {
        if (totalPiercingNum <= 0)
        {
            gameObject.SetActive(false);
        }

        //카메라밖으로 나가면 총알삭제
        if (transform.position.y > GameManager.instance.mainCamera.transform.position.y + Camera.main.orthographicSize + 1)
        {
            gameObject.SetActive(false);
        }
    }
    void Move()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    void Rotate()
    {

    }
    
}


