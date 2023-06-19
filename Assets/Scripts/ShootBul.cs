using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootBul : MonoBehaviour
{
    public float shootSpeed = 0.2f;

    public static ShootBul Instance = null;
    float timer = 0;
    public enum Name
    {
        Lev1,
        Lev2
    }

    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        

        if (timer > shootSpeed)
        {
            switch (GameManager.instance.playerLevel)
            {
                case 1:
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev1), transform.position, Quaternion.identity);
                    timer = 0;
                    break;
                case 2:
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev1), new Vector2(transform.position.x + 0.1f, transform.position.y), Quaternion.identity);
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev1), new Vector2(transform.position.x - 0.1f, transform.position.y), Quaternion.identity);
                    timer = 0;
                    break;
                case 3:
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev1), new Vector2(transform.position.x + 0.15f, transform.position.y), Quaternion.identity);
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev1), new Vector2(transform.position.x - 0.15f, transform.position.y), Quaternion.identity);
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev1), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    timer = 0;
                    break;
                case 4:
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev2), transform.position, Quaternion.identity);
                    timer = 0;
                    break;
                case 5:
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev2), new Vector2(transform.position.x + 0.1f, transform.position.y), Quaternion.identity);
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev2), new Vector2(transform.position.x - 0.1f, transform.position.y), Quaternion.identity);
                    timer = 0;
                    break;
                default:
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev2), new Vector2(transform.position.x + 0.15f, transform.position.y), Quaternion.identity);
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev2), new Vector2(transform.position.x - 0.15f, transform.position.y), Quaternion.identity);
                    Instantiate(KindOfBullet.instance.Kind((int)Name.Lev2), new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                    timer = 0;
                    break;
                


            }
        }


    }



}
