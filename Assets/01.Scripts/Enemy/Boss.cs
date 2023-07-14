using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public float hp = 100;
    [SerializeField] float damage = 5;
    [SerializeField] float speed = 1f;
    [SerializeField] float exp = 50;
    int moveDirec = 1;

    float timer;
    float damageInterval = 1f;

    [SerializeField] protected GameObject skillArea;
    [SerializeField] protected float skillCool;
    protected float skillCoolTimer;
    private void Awake()
    {
        skillCoolTimer = skillCool;
    }
    void Update()
    {
        Move();
        Dead();

        skillCoolTimer -= Time.deltaTime;
        Skill();
    }
    public abstract void Skill();

    public IEnumerator SkillAct()
    {
        skillArea.GetComponent<SpriteRenderer>().color = Color.white;
        skillArea.GetComponent<Collider2D>().enabled = true;
        skillArea.GetComponent<Collider2D>().isTrigger = true;

        yield return null;
        skillArea.GetComponent<Collider2D>().isTrigger = false;
        skillArea.GetComponent<Collider2D>().enabled = false;
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
            SoundManager.instance.UISounds(SoundManager.UISound.hitted);
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
                SoundManager.instance.UISounds(SoundManager.UISound.hitted);
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

            SoundManager.instance.UISounds(SoundManager.UISound.die);

            // 경험치볼 생성.
            GameObject expObj = GameManager.instance.ballPool.GetPool(1);
            expObj.gameObject.transform.position = transform.position;
            expObj.gameObject.GetComponent<ExpBall>().exp = this.exp;

            gameObject.SetActive(false);
        }
    }


}
