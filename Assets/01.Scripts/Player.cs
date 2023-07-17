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
        Ball.magRange = 0.75f;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameState.ing)
            return;

        HpUpdate();
        Dead();
    }


    private void FixedUpdate()
    {
        if (GameManager.instance.gameState != GameManager.GameState.ing)
            return;

        // =============================이동관련================================
        TouchMove();

        KeyBoardMove();
        MoveLimit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Skill"))
        {
            nowHp -= 30;
        }
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
            SoundManager.instance.UISounds(SoundManager.UISound.die);
            gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
    }

    //void InputMove()
    //{
    //    transform.Translate(inputVec * Time.deltaTime * _speed);
    //}

    void TouchMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouch = touch.position;

                    if (PlayerPrefs.GetInt("ControlMode") == 1)
                        GameManager.instance.moveGuide.transform.position = Camera.main.ScreenToWorldPoint(startTouch) + Vector3.forward * 20;
                    else if (PlayerPrefs.GetInt("ControlMode") == 0)
                        GameManager.instance.joyBG.transform.position = startTouch;

                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    dragTouch = touch.position;
                    inputVec = dragTouch - startTouch;

                    if (PlayerPrefs.GetInt("ControlMode") == 0)
                        GameManager.instance.joyStick.transform.localPosition = inputVec.normalized * Mathf.Clamp(inputVec.magnitude, 0, 80);

                    // 0.08f => 패드 위치에따라 바꿔주면댐 지금은 80  이속최대값바뀌는거^^
                    transform.Translate(inputVec.normalized * Mathf.Clamp(inputVec.magnitude / 1000, 0, 0.08f) * Time.deltaTime * _speed);
                    break;
            }
        }
        else if (Input.touchCount == 0)
        {
            if (PlayerPrefs.GetInt("ControlMode") == 0)
            {
                GameManager.instance.joyStick.transform.localPosition = new Vector3(-5, 8, 0);
                GameManager.instance.joyBG.transform.localPosition = new Vector3(0, -780, 20);
            }
            else if (PlayerPrefs.GetInt("ControlMode") == 1)
                GameManager.instance.moveGuide.transform.position = new Vector3(100, 100, 0);
        }
    }
    void KeyBoardMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        inputVec = new Vector2(x, y);
        transform.Translate(inputVec.normalized * Time.deltaTime * _speed / 10);
    }

    void MoveLimit()
    {
        if (transform.position.y < mapMinY) transform.position = new Vector3(transform.position.x, mapMinY, 0f);
        if (transform.position.y > mapMaxY - 0.5f) transform.position = new Vector3(transform.position.x, mapMaxY - 0.5f, 0f);
        if (transform.position.x > mapMaxX - 0.5f) transform.position = new Vector3(mapMaxX - 0.5f, transform.position.y, 0f);
        if (transform.position.x < -mapMaxX + 0.5f) transform.position = new Vector3(-mapMaxX + 0.5f, transform.position.y, 0f);
    }

    // =======================마우스drag TEST용========================   
    //private void OnMouseDown()
    //{
    //    startTouch = Input.mousePosition;

    //    if (PlayerPrefs.GetInt("ControlMode") == 1)
    //        GameManager.instance.moveGuide.transform.position = Camera.main.ScreenToWorldPoint(startTouch) + Vector3.forward * 20;
    //    else if (PlayerPrefs.GetInt("ControlMode") == 0)
    //        GameManager.instance.joyBG.transform.position = startTouch;
    //}

    //private void OnMouseDrag()
    //{
    //    Vector3 vec = new Vector3(Input.mousePosition.x - startTouch.x, Input.mousePosition.y - startTouch.y, 0f);

    //    transform.Translate(vec.normalized * Mathf.Clamp(vec.magnitude / 1000, 0, 0.08f) * Time.deltaTime * _speed);

    //    if (PlayerPrefs.GetInt("ControlMode") == 0)
    //        GameManager.instance.joyStick.transform.localPosition = vec.normalized * Mathf.Clamp(vec.magnitude, 0, 80);
    //}
    //private void OnMouseUp()
    //{
    //    if (PlayerPrefs.GetInt("ControlMode") == 0)
    //    {
    //        GameManager.instance.joyStick.transform.localPosition = new Vector3(-5, 8, 0);
    //        GameManager.instance.joyBG.transform.localPosition = new Vector3(0, -780, 20);
    //    }
    //    else if (PlayerPrefs.GetInt("ControlMode") == 1)
    //        GameManager.instance.moveGuide.transform.position = new Vector3(100, 100, 0); //안보이는곳으로 보내버려
    //}
}
