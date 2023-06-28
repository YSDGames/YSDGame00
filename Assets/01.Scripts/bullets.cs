using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullets : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 3f;
   

    public GameObject hitEffect;
    public GameObject aura;
    //[SerializeField] float shootSpeed = 0.2f;
    public float damage = 1f;
    [HideInInspector]public float totalDamage;
    public int piercingNum = 1;
    [HideInInspector] public int totalPiercingNum;

    private void Awake()
    {
        //ShootBul.Instance.shootSpeed = shootSpeed;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
       


        totalDamage = damage;
        totalPiercingNum = piercingNum;
    }

    private void Update()
    {
        Shoot();
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
    void Shoot()
    {
        transform.Translate( Vector3.right * bulletSpeed * Time.deltaTime);
    }

    
}


