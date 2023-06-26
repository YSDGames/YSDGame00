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
        hight = Camera.main.orthographicSize;  //ī�޶� ������ 1/2
        width = hight * Screen.width / Screen.height;

        if (GameManager.instance.GetTime() >= cameraSizeUpTime)
        {
            if (Camera.main.orthographicSize <= 7)
                Camera.main.orthographicSize += Time.deltaTime *  2.5f / hight ; //ī�޶� scale�� 5�϶� 0.5������ ��������. �� 2.5/hight ��ŭ �ӵ�?

        }



        difX = transform.position.x - player.transform.position.x;
        difY = transform.position.y - player.transform.position.y;

        // ����
        if (difX > width / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x - width / 3, transform.position.y, transform.position.z), 0.01f);
        // ������
        if (difX < -width / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + width / 3, transform.position.y, transform.position.z), 0.01f);
        // �Ʒ�����
        if (difY > hight * 2 / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y + hight * 2 / 3, transform.position.z), 0.03f);
        // ������
        if (difY < -hight / 3) transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, player.transform.position.y - hight / 3, transform.position.z), 0.03f);


        // transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.05f);


        {
            //ī�޶� �̵����� ����
            float clampX = Mathf.Clamp(transform.position.x, -mapX + width, mapX - width);
            float clampY = Mathf.Clamp(transform.position.y, -5 + hight, mapY - hight); //-5�� BG�� ���οö󰡴°� ���̱��� �ּ���ġ


            transform.position = new Vector3(clampX, clampY, -10f);

        }
    }
}
