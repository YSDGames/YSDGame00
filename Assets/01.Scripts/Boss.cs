using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public AudioClip bossDieSound;

    public float hp = 100;
    [SerializeField] float damage = 5;
    [SerializeField] float speed = 1f;
    [SerializeField] float exp = 50;
    int moveDirec = 1;

    float timer;
    float damageInterval = 1f;
    void Update()
    {

        Move();
        Dead();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            bullets bullet = collision.gameObject.GetComponent<bullets>();
            //Item Orb = GameObject.Find("UI/Items").GetComponentInChildren<Item>();

            hp -= bullet.totalDamage;

            bullet.totalPiercingNum -= 1;
            GameManager.instance.MakeEffect(collision);
            GameManager.instance.MakeSound();
        }

        

        if (collision.gameObject.CompareTag("Player"))
        {
            timer = 0;
            SoundManager.instance.HittedSound();
            GameManager.instance.player.nowHp -= damage;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        timer += Time.deltaTime;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (timer > damageInterval)
            {
                SoundManager.instance.HittedSound();
                GameManager.instance.player.nowHp -= damage;
                timer = 0;
            }
        }
    }
    
    void Move()
    {
        if (transform.position.y > 10)
        {
            transform.position += Vector3.down * Time.deltaTime * speed * 3f;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * speed * 2f * moveDirec;
            if (transform.position.x > 10) moveDirec = -1;
            else if (transform.position.x < -10) moveDirec = 1;

        }
    }
    void Dead()
    {
        if (hp <= 0)
        {
            if (gameObject.name == "Boss4")
            {
                GameManager.instance.GameClear();
                gameObject.SetActive(false);
            }

            SoundManager.instance.SFXPlay("BossDie", bossDieSound, 1.0f);

            // 경험치볼 생성.
            GameObject expObj = GameManager.instance.ballPool.GetPool(1);
            expObj.gameObject.transform.position = transform.position;
            expObj.gameObject.GetComponent<ExpBall>().exp = this.exp;

            gameObject.SetActive(false);
        }
    }
}
