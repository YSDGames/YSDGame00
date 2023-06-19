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
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if(transform.position.y < -10.2f)
        {
            transform.position = new Vector3(transform.position.x, 20.4f , transform.position.z);
        }
    }
}
