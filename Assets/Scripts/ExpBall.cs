using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : MonoBehaviour
{
    public float exp = 1f;
    public float speed = 1f;

    public float magRange = 1f;
    bool isEnter = false;

    void Update()
    {
        Move();
        Destroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.instance.exp += exp;
        }
    }

    void Destroy()
    {
        if(transform.position.y < -6)
            gameObject.SetActive(false);

    }
    void Move()
    {

        Magnetic();

        if (!isEnter)
            transform.position += Vector3.down * Time.deltaTime * speed;

    }

    void Magnetic()
    {
        Vector3 dir = GameManager.instance.player.transform.position - transform.position;
        float distance = dir.magnitude;

        if (distance < magRange)
        {
            transform.position += dir.normalized * Time.deltaTime * 5f;

            isEnter = true;
        }
        else
            isEnter = false;
    }
}
