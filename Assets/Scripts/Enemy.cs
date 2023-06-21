using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHp;
    [HideInInspector] public float hp;
    public float damage = 1f;
    public float speed = 1f;
    float mainSpeed;

    public float exp = 1f;

    float damageInterval = 1f;
    float timer = 0;

    Vector3 dirVec;
    

    void Awake()
    {
        mainSpeed = speed;
        
    }


    void Update()
    {

        LookPlayer();
        GoToPlayer();
        Reposition();
        Dead();

    }

    private void OnEnable()
    {
        hp = maxHp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        timer = 0;

        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.player.nowHp -= damage;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        timer += Time.deltaTime;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (timer > damageInterval)
            {
                GameManager.instance.player.nowHp -= damage;
                timer = 0;
            }
        }
    }



    void LookPlayer()
    {
        Vector2 direction = (GameManager.instance.player.gameObject.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 270, Vector3.forward);

        transform.rotation = q;
    }

    void GoToPlayer()
    {

        if ((transform.position.y - GameManager.instance.player.gameObject.transform.position.y) > 1f)
        {
            dirVec = Vector3.Normalize(GameManager.instance.player.gameObject.transform.position - transform.position);

            transform.position += dirVec * Time.deltaTime * speed;
        }
        else
        {
            transform.position += dirVec * Time.deltaTime * speed;

        }

        //카메라 밖에 있을때 빠르게 움직이게
        float distanceFroemCameraX = Mathf.Abs(transform.position.x - GameManager.instance.mainCamera.transform.position.x);
        float distanceFroemCameraY = Mathf.Abs(transform.position.y - GameManager.instance.mainCamera.transform.position.y);


        if (distanceFroemCameraX > 6 || distanceFroemCameraY > 8)
        {
            speed = 5 * mainSpeed;
        }
        else
        {
            speed = mainSpeed;
        }

    }

    void Dead()
    {
        if (hp <= 0)
        {
            // 경험치볼 생성.
            GameObject expObj = GameManager.instance.pool.GetPool(0);
            expObj.gameObject.transform.position = transform.position;
            expObj.gameObject.GetComponent<ExpBall>().exp = this.exp;
            
            gameObject.SetActive(false);

        }
    }

    void Reposition()
    {
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(-transform.position.x, 15f, transform.position.z);
        }

        if (Mathf.Abs(transform.position.x) > 25)
        {
            transform.position = new Vector3(0f, 15f, transform.position.z);
        }
    }

}
