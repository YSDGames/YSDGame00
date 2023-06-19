using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class spaceship : MonoBehaviour
{
    public GameObject bullet0;
    public GameObject shootpoint;
    public static spaceship instance;
    public float hp = 10;



    [SerializeField]
    private float _speed = 1.0f;

    float mapMaxY = 15f;
    float mapMinY = -4.6f;

    float mapMaxX = 15.3f;

    private Vector3 inputVec;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;


    }

    // Update is called once per frame
    void Update()
    {
        Move();

        
    }

    void Dead()
    {
        
    }
    void Shoot()
    {
        Instantiate(bullet0, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);
    }

    void Move()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");

        inputVec = inputVec.magnitude > 1 ? inputVec.normalized : inputVec;

        transform.Translate(inputVec * _speed * Time.deltaTime);

        if (transform.position.y < mapMinY) transform.position = new Vector3(transform.position.x, mapMinY, 0f);
        if (transform.position.y > mapMaxY-0.5f) transform.position = new Vector3(transform.position.x, mapMaxY-0.5f, 0f);
        if (transform.position.x > mapMaxX-0.5f) transform.position = new Vector3(mapMaxX-0.5f, transform.position.y, 0f);
        if (transform.position.x < -mapMaxX+0.5f) transform.position = new Vector3(-mapMaxX+0.5f, transform.position.y, 0f);


    }
}
