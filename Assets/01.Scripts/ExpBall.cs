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

    private void Start()
    {
        if (exp == 1 || exp == 5) size = 0.1f;
        else if (exp >= 50) size = 1f;
        else size = 0.2f;

        transform.localScale = new Vector3(size, size, size);
    }
 
    
    void Update()
    {
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
    private void OnEnable()
    {
        if (exp == 1 || exp == 5) size = 0.1f;
        else if (exp >= 50) size = 1f;
        else size = 0.2f;

        transform.localScale = new Vector3(size, size, size);
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
