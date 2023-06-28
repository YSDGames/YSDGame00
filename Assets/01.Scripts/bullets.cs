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
    public int attackNum;
    int setAttackNum = 1;
    


    private void Awake()
    {
        //ShootBul.Instance.shootSpeed = shootSpeed;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        attackNum = setAttackNum;


    }

    private void Update()
    {
        Shoot();
        DestroyBullet();
    }

    private void OnEnable()
    {
        attackNum = setAttackNum;
    }

    void DestroyBullet()
    {
        if (attackNum <= 0)
        {
            gameObject.SetActive(false);
        }

        //ī�޶������ ������ �Ѿ˻���
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


