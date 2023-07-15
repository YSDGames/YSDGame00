using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBall : Ball
{
    public float heal = 0;
    void Update()
    {
        Move();
        Magnetic();
        Destroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.UISounds(SoundManager.UISound.heal);
            gameObject.SetActive(false);
            Player.instance.nowHp += heal;
        }
    }
}
