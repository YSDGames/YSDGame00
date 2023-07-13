using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy1 : Enemy
{
    
    private void Update()
    {
        if (!isLive) return;

        SpeedControl();
        Move();
        Reposition();
    }

    public override void Move()
    {
        //거리 1보다 멀면 player쪽으로 향해서가고 1보다 가까워지면 그냥 앞으로쭉.
        if ((transform.position.y - GameManager.instance.player.gameObject.transform.position.y) > 1f && moveTrigger)
        {
            dirVec = Vector3.Normalize(GameManager.instance.player.gameObject.transform.position - transform.position);

            transform.position += dirVec * Time.deltaTime * speed;
            LookPlayer();
        }
        else
        {
            transform.position += dirVec * Time.deltaTime * speed;
            moveTrigger = false;
        }

    }
}
