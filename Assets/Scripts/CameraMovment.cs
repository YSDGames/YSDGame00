using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    public GameObject player;

    float mapX;
    float mapY;
    float hight;
    float width;
    float difX;
    float difY;
    // Start is called before the first frame update
    void Start()
    {
        mapX = 15.3f;
        mapY = 15f;

                //카메라 높이의 1/2
        hight = Camera.main.orthographicSize;
        width = hight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        difX = transform.position.x - player.transform.position.x;
        difY = transform.position.y - player.transform.position.y;

        // 왼쪽
        if (difX > width / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x - width / 3, transform.position.y, transform.position.z), 0.005f);
        // 오른쪽
        if (difX < -width / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + width / 3, transform.position.y, transform.position.z), 0.005f);
        // 아래방향
        if (difY > hight * 2 / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y + hight *2/ 3, transform.position.z),0.03f);
        // 위방향
        if (difY < -hight /3) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y - hight /3, transform.position.z), 0.03f);


        // transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.05f);


        {
            //카메라 이동영역 제한
            float clampX = Mathf.Clamp(transform.position.x, -mapX + width, mapX - width);
            float clampY = Mathf.Clamp(transform.position.y, 0, mapY - hight);


            transform.position = new Vector3(clampX, clampY, -10f);

        }
    }
}
