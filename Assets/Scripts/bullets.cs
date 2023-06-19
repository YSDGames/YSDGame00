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
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
            collision.GetComponent<Enemy>().hp -= damage;
        }
    }

    void DestroyBullet()
    {
        if (transform.position.y > 20)
        {
            Destroy(gameObject);
        }
    }
    void ShootStraight()
    {
        transform.position += Vector3.up * bulletSpeed * Time.deltaTime;
    }
}


