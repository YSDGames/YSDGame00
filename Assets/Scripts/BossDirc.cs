using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDirc : MonoBehaviour
{
    Vector3 bossPosition;
    Vector3 dir;

    // ����� 0,0���� ������ ��,�Ʒ� �ִ� x,y��
    float staticMaxX = 2.82f;
    float staticMaxY = 3.92f;
    float staticMinX = -2.82f;
    float staticMinY = -3.92f;

    bool existBoss = true;

    private void Awake()
    {

    }
    void Update()
    {
        bossPosition = GameObject.FindWithTag("Boss").transform.position;

        SetActive();
        SetPosition();
        LookBoss();

        

    }
    void SetActive()
    {

        //������ �����Ҷ��� Ŵ.
        if (!GameObject.FindWithTag("Boss").activeSelf)
        {
            //ī�޶�ȿ����� ��.
            Vector3 isInCamerVec = bossPosition - GameManager.instance.mainCamera.transform.position;

            if (isInCamerVec.x < staticMaxX + 4 && isInCamerVec.x > staticMinX - 4 && isInCamerVec.y < staticMaxY + 4 && isInCamerVec.y > staticMinY - 4)
            {
                gameObject.SetActive(false);

            }
            else
            {
                gameObject.SetActive(true);
                
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

        

    }

    void SetPosition()
    {
        dir = bossPosition - GameManager.instance.player.transform.position;
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
        Vector2 direction = (bossPosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 270, Vector3.forward);

        transform.rotation = q;
    }
}
