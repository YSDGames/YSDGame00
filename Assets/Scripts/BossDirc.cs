using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirc : MonoBehaviour
{
    
    Vector3 dir;

    // ����� 0,0���� ������ ��,�Ʒ� �ִ� x,y��
    
    public float staticMaxX { get;} = 2.82f;
    public float staticMaxY { get;} = 3.92f;
    public float staticMinX { get;} = -2.82f;
    public float staticMinY { get; } = -3.92f;

    static public BossDirc instance=null;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        
        SetPosition();
        LookBoss();
    }

    
    void SetPosition()
    {
        dir = GameManager.instance.bossPosition - GameManager.instance.player.transform.position;
        Vector3 playerLocalVec = GameManager.instance.player.transform.position - GameManager.instance.mainCamera.transform.position;

        //dir�� 1,2,3,4 �и��϶��� ����.
        if (dir.x >= 0 && dir.y >= 0)
        {
            float lengthVecX = (staticMaxY - playerLocalVec.y) / dir.y * dir.x;
            float lengthVecY = (staticMaxX - playerLocalVec.x) / dir.x * dir.y;

            if (Mathf.Abs(lengthVecX) <= Mathf.Abs(playerLocalVec.x - staticMaxX))
            {
                transform.localPosition = new Vector3(playerLocalVec.x + lengthVecX, staticMaxY, 10);
            }
            else
            {
                transform.localPosition = new Vector3(staticMaxX, playerLocalVec.y + lengthVecY, 10);
            }
        }
        else if (dir.x >= 0 && dir.y <= 0)
        {
            float lengthVecX = (staticMinY - playerLocalVec.y) / dir.y * dir.x;
            float lengthVecY = (staticMaxX - playerLocalVec.x) / dir.x * dir.y;

            if (Mathf.Abs(lengthVecX) <= Mathf.Abs(playerLocalVec.x - staticMaxX))
            {
                transform.localPosition = new Vector3(playerLocalVec.x + lengthVecX, staticMinY,10);
            }
            else
            {
                transform.localPosition = new Vector3(staticMaxX, playerLocalVec.y + lengthVecY, 10);
            }

        }
        else if (dir.x <= 0 && dir.y <= 0)
        {
            float lengthVecX = (staticMinY - playerLocalVec.y) / dir.y * dir.x;
            float lengthVecY = (staticMinX - playerLocalVec.x) / dir.x * dir.y;

            if (Mathf.Abs(lengthVecX) <= Mathf.Abs(playerLocalVec.x - staticMaxX))
            {
                transform.localPosition = new Vector3(playerLocalVec.x + lengthVecX, staticMinY,10);
            }
            else
            {
                transform.localPosition = new Vector3(staticMinX, playerLocalVec.y + lengthVecY, 10);
            }

        }
        else if (dir.x <= 0 && dir.y >= 0)
        {
            float lengthVecX = (staticMaxY - playerLocalVec.y) / dir.y * dir.x;
            float lengthVecY = (staticMinX - playerLocalVec.x) / dir.x * dir.y;

            if (Mathf.Abs(lengthVecX) <= Mathf.Abs(playerLocalVec.x - staticMaxX))
            {
                transform.localPosition = new Vector3(playerLocalVec.x + lengthVecX, staticMaxY, 10);
            }
            else
            {
                transform.localPosition = new Vector3(staticMinX, playerLocalVec.y + lengthVecY, 10);
            }

        }

    }


    void LookBoss()
    {
        Vector2 direction = (GameManager.instance.bossPosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 270, Vector3.forward);

        transform.rotation = q;
    }
}
