using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : MonoBehaviour
{
    [SerializeField] AudioClip eatEXPSound;

    public float exp;
    public float speed = 0.7f;
    public float size;
    public float magRange = 1f;
    bool isEnter = false;

    void Update()
    {
        if (exp == 1) size = 0.1f;
        else if (exp <= 2) size = 0.2f;
        else if (exp <= 3) size = 0.25f;
        else if (exp <= 5) size = 0.4f;
        else if (exp <= 10) size = 0.45f;
        else if (exp <= 49) size = 0.55f;
        else if (exp >= 50) size = 2f;
        transform.localScale = new Vector3(size, size, size);
        

        Move();
        Destroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.SFXPlay("EatEXP", eatEXPSound, 0.5f);

            gameObject.SetActive(false);
            GameManager.instance.exp += exp;
        }
    }
    

    void Move()
    {

        Magnetic();

        if (!isEnter)
            transform.position += Vector3.down * Time.deltaTime * speed;

    }

    void Destroy()
    {
        if(transform.position.y < -6)
            gameObject.SetActive(false);

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
