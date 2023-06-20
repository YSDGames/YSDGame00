using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    // Update is called once per frame
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


            case "Boss":
                //  ������ġ-������ġ                  // 3�е��� �������� �ö� �� �ְ�.
                transform.position += Vector3.up * Time.deltaTime * (19f - (-7f)) / ( 3 * 60 );


                break;

        }



    }
}
