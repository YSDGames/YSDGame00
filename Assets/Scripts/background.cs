using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    float startPositionY;
    [SerializeField] float endPositionY = 19f;
    [SerializeField] float bossNum;

    private void Awake()
    {
        startPositionY = transform.position.y;
    }
    void Update()
    {
        switch (gameObject.transform.tag)
        {
            case "BackGround":

                transform.position += Vector3.down * moveSpeed * Time.deltaTime;

                if (transform.position.y < -10.2f)
                {
                    transform.position = new Vector3(transform.position.x, 20.4f, transform.position.z);
                }

                break;

            case "Object":

                transform.position += Vector3.up * moveSpeed * Time.deltaTime;

                if (transform.position.y > 19f)
                {
                    transform.position = new Vector3(transform.position.x, -10, transform.position.z);
                }
                break;


            case "BGBoss":
                                                                     //  ������ġ-������ġ          // bossNum*2�е��� �������� �ö� �� �ְ�.           
                transform.position += Vector3.up * Time.deltaTime * (endPositionY - startPositionY) / (bossNum * 2 * 60);


                break;

        }



    }
}
