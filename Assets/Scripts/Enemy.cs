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
    public float exp = 1f;

    public GameObject expball;

    Vector3 dirVec;


    void Start()
    {
        player = GameObject.Find("Player");
    }

    
    void Update()
    {

        LookPlayer();
        GoToPlayer();
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
        
        if ((transform.position.y - player.transform.position.y) > 1f )
        {
            dirVec = Vector3.Normalize(player.transform.position - transform.position);

            transform.position += dirVec * Time.deltaTime * speed;
        }
        else
        {
            transform.position += dirVec * Time.deltaTime * speed;
            
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
}
