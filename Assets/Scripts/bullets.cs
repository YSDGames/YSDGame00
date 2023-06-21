using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullets : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 3f;

    //[SerializeField] float shootSpeed = 0.2f;
    [SerializeField] float damage = 1f;

    private void Awake()
    {
        //ShootBul.Instance.shootSpeed = shootSpeed;
        transform.rotation = Quaternion.AngleAxis(90, new Vector3(0, 0, 1));

        transform.parent = GameObject.Find("PoolManager").gameObject.transform;
    }

    private void Update()
    {
        ShootStraight();
        DestroyBullet();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Boss>().hp -= damage;
        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
            collision.GetComponent<Enemy>().hp -= damage;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void DestroyBullet()
    {
        //카메라밖으로 나가면 총알삭제
        if (transform.position.y > GameManager.instance.mainCamera.transform.position.y + Camera.main.orthographicSize + 1)
        {
            Destroy(gameObject);
        }
    }
    void ShootStraight()
    {
        transform.position += Vector3.up * bulletSpeed * Time.deltaTime;
    }
}


