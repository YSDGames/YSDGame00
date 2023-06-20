using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject player;

    public float hp = 5f;
    public float damage = 1f;
    public float speed = 1f;
    float mainSpeed;


    public float exp = 1f;



    public GameObject expball;

    Vector3 dirVec;


    void Awake()
    {
        player = GameObject.Find("Player");
        mainSpeed = speed;
    }


    void Update()
    {

        LookPlayer();
        GoToPlayer();
        Reposition();
        Dead();

    }
    void LookPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 270, Vector3.forward);

        transform.rotation = q;
    }

    void GoToPlayer()
    {

        if ((transform.position.y - player.transform.position.y) > 1f)
        {
            dirVec = Vector3.Normalize(player.transform.position - transform.position);

            transform.position += dirVec * Time.deltaTime * speed;
        }
        else
        {
            transform.position += dirVec * Time.deltaTime * speed;

        }

        float distanceFroemCameraX = Mathf.Abs(transform.position.x - GameManager.instance.mainCamera.transform.position.x);
        float distanceFroemCameraY = Mathf.Abs(transform.position.y - GameManager.instance.mainCamera.transform.position.y);


        if (distanceFroemCameraX > 4 || distanceFroemCameraY > 6)
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
            Instantiate(expball, transform.position, Quaternion.identity);

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
