using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public AudioClip eatEXPSound;
    public float speed = 0.7f;
    [HideInInspector] public float size;
    [HideInInspector] public static float magRange = 0.5f;
    bool isEnter = false;
    
    public void Move()
    {
        if (!isEnter)
            transform.position += Vector3.down * Time.deltaTime * speed;
    }

    public void Destroy()
    {
        if(transform.position.y < -6)
            gameObject.SetActive(false);
    }
    

    public void Magnetic()
    {
        Vector3 dir = GameManager.instance.player.transform.position - transform.position;
        float distance = dir.magnitude;

        if (distance < magRange)
        {
            transform.position += dir.normalized * Time.deltaTime * 15f;

            isEnter = true;
        }
        else
            isEnter = false;
    }
}
