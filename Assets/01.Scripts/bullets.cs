using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullets : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 3f;

    //[SerializeField] float shootSpeed = 0.2f;
    public float damage = 1f;
    


    private void Awake()
    {
        //ShootBul.Instance.shootSpeed = shootSpeed;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);

        transform.parent = GameObject.Find("PoolManager").gameObject.transform;
    }

    private void Update()
    {
        Shoot();
        DestroyBullet();
    }

    

    void DestroyBullet()
    {
        //ī�޶������ ������ �Ѿ˻���
        if (transform.position.y > GameManager.instance.mainCamera.transform.position.y + Camera.main.orthographicSize + 1)
        {
            Destroy(gameObject);
        }
    }
    void Shoot()
    {
        transform.Translate( Vector3.right * bulletSpeed * Time.deltaTime);
    }

    
}

