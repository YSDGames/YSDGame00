using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float maxHp;
    [HideInInspector] public float hp;
    public AudioClip enemyDieSound;
    public Player player;

    Animator aniControl;

    SpriteRenderer spriter;
    Rigidbody2D rig;
    Collider2D coll;
    public float damage = 1f;
    public float speed = 1f;
    protected float mainSpeed;
    protected bool isLive;
    public float exp = 1f;

    protected float damageInterval;
    protected float timer;
    public bool moveTrigger;
    protected Vector3 dirVec;


    void Awake()
    {
        damageInterval = 1f;
        timer = 0;
        isLive = true;
        moveTrigger = true;

        mainSpeed = speed;
        aniControl = gameObject.GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rig = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        LookPlayer();

        moveTrigger = true;
        hp = maxHp;
        isLive = true;
        coll.enabled = true;
        rig.simulated = true;
        aniControl.SetBool("Live", true);
        spriter.sortingOrder = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Skill2"))
        {
            isLive = false;
            coll.enabled = false;
            rig.simulated = false;
            aniControl.SetBool("Live", false);
            spriter.sortingOrder = -1;

            SoundManager.instance.SFXPlay("EnemyDie", enemyDieSound, 2f);
        }

        if (collision.CompareTag("Bullet"))
        {
            bullets bullet = collision.gameObject.GetComponent<bullets>();
            //Item Orb = GameObject.Find("UI/LevelUp/Panel/Items").GetComponentsInChildren<Item>()[6];

            hp -= bullet.totalDamage;

            bullet.totalPiercingNum -= 1;
            GameManager.instance.MakeEffect(collision);
            GameManager.instance.MakeSound();

            if (hp <= 0)
            {
                isLive = false;
                coll.enabled = false;
                rig.simulated = false;
                aniControl.SetBool("Live", false);
                spriter.sortingOrder = -1;

                SoundManager.instance.SFXPlay("EnemyDie", enemyDieSound, 2f);
            }
            else
            {
                //넉백
                Vector3 dir = GameManager.instance.player.transform.position - transform.position;
                transform.position -= dir.normalized * 0.1f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        timer = 0;

        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.UISounds(SoundManager.UISound.hitted);
            GameManager.instance.player.nowHp -= damage;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
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
    public abstract void Move();

    public void LookPlayer()
    {
        Vector2 direction = new Vector2();

        if (player != null)
            direction = (player.transform.position - transform.position).normalized;
        else
            direction = (GameManager.instance.player.gameObject.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 270, Vector3.forward);

        transform.rotation = q;
    }
    public void Dead()
    {
        int rand = Random.Range(1, 101);
        if (rand <= 5)
        {
            // 회복구슬
            GameObject heal = GameManager.instance.ballPool.GetPool(2);
            heal.gameObject.transform.position = transform.position;
        }
        else
        {
            // 경험치볼 생성.
            GameObject expObj = GameManager.instance.ballPool.GetPool(0);
            expObj.gameObject.transform.position = transform.position;
            expObj.gameObject.GetComponent<ExpBall>().exp = this.exp;
        }
        gameObject.SetActive(false);
    }
    public void Reposition()
    {
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(-transform.position.x, 15f, transform.position.z);
            LookPlayer();
            moveTrigger = true;
        }

        if (Mathf.Abs(transform.position.x) > 25)
        {
            transform.position = new Vector3(0f, 15f, transform.position.z);
            LookPlayer();
            moveTrigger = true;
        }
    }

    public void SpeedControl()
    {

        //카메라 밖에 있을때 빠르게 움직이게
        float distanceFroemCameraX = Mathf.Abs(transform.position.x - GameManager.instance.mainCamera.transform.position.x);
        float distanceFroemCameraY = Mathf.Abs(transform.position.y - GameManager.instance.mainCamera.transform.position.y);

        if (distanceFroemCameraX > 6 || distanceFroemCameraY > 7)
        {
            speed = 5 * mainSpeed;
        }
        else
        {
            speed = mainSpeed;
        }
    }
}
