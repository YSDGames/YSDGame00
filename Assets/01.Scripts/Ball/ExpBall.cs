using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : Ball
{
    public float exp;
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
        Magnetic();
        Destroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.UISounds(SoundManager.UISound.expBall);
            gameObject.SetActive(false);
            GameManager.instance.exp += exp;
        }
    }
}
