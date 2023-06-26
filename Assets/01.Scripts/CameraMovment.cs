using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    public static CameraMovment instance;

    public GameObject player;

    float mapX;
    float mapY;
    public float hight;
    public float width;
    float difX;
    float difY;

    float cameraSizeUpTime;

    void Start()
    {
        instance = this;

        mapX = 15.3f;
        mapY = 15f;

        cameraSizeUpTime = 60 * 2 - 5;

    }

    void Update()
    {
        hight = Camera.main.orthographicSize;  //카메라 높이의 1/2
        width = hight * Screen.width / Screen.height;

        if (GameManager.instance.GetTime() >= cameraSizeUpTime)
        {
            if (Camera.main.orthographicSize <= 7)
                Camera.main.orthographicSize += Time.deltaTime *  2.5f / hight ; //카메라 scale이 5일때 0.5정도가 적당했음. 즉 2.5/hight 만큼 속도?

        }



        difX = transform.position.x - player.transform.position.x;
        difY = transform.position.y - player.transform.position.y;

        // 왼쪽
        if (difX > width / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x - width / 3, transform.position.y, transform.position.z), 0.01f);
        // 오른쪽
        if (difX < -width / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + width / 3, transform.position.y, transform.position.z), 0.01f);
        // 아래방향
        if (difY > hight * 2 / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y + hight * 2 / 3, transform.position.z), 0.03f);
        // 위방향
        if (difY < -hight / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y - hight / 3, transform.position.z), 0.03f);


        // transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.05f);


        {
            //카메라 이동영역 제한
            float clampX = Mathf.Clamp(transform.position.x, -mapX + width, mapX - width);
            float clampY = Mathf.Clamp(transform.position.y, -5 + hight, mapY - hight); //-5는 BG가 위로올라가는게 보이기전 최소위치


            transform.position = new Vector3(clampX, clampY, -10f);

        }
    }
}
