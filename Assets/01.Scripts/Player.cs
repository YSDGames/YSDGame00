
using UnityEngine;

public class Player : MonoBehaviour
{
    int typeOrb;
    public GameObject shootpoint;
    public static Player instance;
    public GameObject hpBar;

    public float maxHp = 10;
    [HideInInspector] public float nowHp;
    [SerializeField] private float _speed = 1.0f;

    float mapMaxY = 15f;
    float mapMinY = -4.6f;
    float mapMaxX = 15.3f;

    Vector2 startTouch;
    Vector2 dragTouch;

    private Vector2 inputVec;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        nowHp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        TouchMove();
        KeyBoardMove();
        HpUpdate();
        Dead();
    }

    private void FixedUpdate()
    {

    }

    void HpUpdate()
    {
        nowHp = Mathf.Clamp(nowHp, 0, maxHp);
        hpBar.gameObject.GetComponent<Transform>().localScale = new Vector3(Mathf.Clamp(nowHp / maxHp, 0, 1), 1, 1);
    }

    void Dead()
    {
        if (nowHp <= 0)
        {
            SoundManager.instance.DieSound();
            gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
    }

    void KeyBoardMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        inputVec = new Vector2(x, y);
        transform.Translate(inputVec.normalized * Time.deltaTime * _speed);

        if (transform.position.y < mapMinY) transform.position = new Vector3(transform.position.x, mapMinY, 0f);
        if (transform.position.y > mapMaxY - 0.5f) transform.position = new Vector3(transform.position.x, mapMaxY - 0.5f, 0f);
        if (transform.position.x > mapMaxX - 0.5f) transform.position = new Vector3(mapMaxX - 0.5f, transform.position.y, 0f);
        if (transform.position.x < -mapMaxX + 0.5f) transform.position = new Vector3(-mapMaxX + 0.5f, transform.position.y, 0f);
    }

       // =======================마우스drag TEST용========================   

    //private void OnMouseDown()
    //{
    //    startTouch = Input.mousePosition;
    //}

    //private void OnMouseDrag()
    //{
    //    Vector3 vec = new Vector3(Input.mousePosition.x - startTouch.x, Input.mousePosition.y - startTouch.y, 0f);

    //    transform.Translate(vec.normalized * Mathf.Clamp(vec.magnitude/1000, -1, 1) * Time.deltaTime * _speed);
    //    //transform.Translate(vec * Time.deltaTime * 0.005f);

    //}
    void TouchMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            switch (touch.phase)
            {
                case UnityEngine.TouchPhase.Began:
                    startTouch = touch.position;
                    break;
                case UnityEngine.TouchPhase.Moved:
                    dragTouch = touch.position;

                    inputVec = dragTouch - startTouch;
                    //transform.Translate(inputVec * Time.deltaTime * _speed);
                    transform.Translate(inputVec.normalized * Mathf.Clamp(inputVec.magnitude / 1000, -1, 1) * Time.deltaTime * _speed);


                    if (transform.position.y < mapMinY) transform.position = new Vector3(transform.position.x, mapMinY, 0f);
                    if (transform.position.y > mapMaxY - 0.5f) transform.position = new Vector3(transform.position.x, mapMaxY - 0.5f, 0f);
                    if (transform.position.x > mapMaxX - 0.5f) transform.position = new Vector3(mapMaxX - 0.5f, transform.position.y, 0f);
                    if (transform.position.x < -mapMaxX + 0.5f) transform.position = new Vector3(-mapMaxX + 0.5f, transform.position.y, 0f);
                    break;

            }

        }
    }

    //void OnMove(InputValue value)
    //{
    //    inputVec = value.Get<Vector2>();
    //}
}
