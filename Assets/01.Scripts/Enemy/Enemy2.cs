
using UnityEngine;

public class Enemy2 : Enemy
{    private void Start()
    {
        LookPlayer();
    }

    void Update()
    {
        if (!isLive) return;

        SpeedControl();
        Move();
        Reposition();
    }


    public override void Move()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
    }
}


