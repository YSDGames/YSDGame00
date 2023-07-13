
using UnityEngine;
using UnityEngine.InputSystem;


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
        if (GameManager.instance.gameState != GameManager.GameState.ing)
            return;
        if (PlayerPrefs.GetInt("ControlMode") == 1) TouchMove();
        else if (PlayerPrefs.GetInt("ControlMode") == 0) InputMove();
        //KeyBoardMove();

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
            SoundManager.instance.UISounds(SoundManager.UISound.die);
            gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
    }

    void InputMove()
    {
        transform.Translate(inputVec * Time.deltaTime * _speed);

        if (transform.position.y < mapMinY) transform.position = new Vector3(transform.position.x, mapMinY, 0f);
        if (transform.position.y > mapMaxY - 0.5f) transform.position = new Vector3(transform.position.x, mapMaxY - 0.5f, 0f);
        if (transform.position.x > mapMaxX - 0.5f) transform.position = new Vector3(mapMaxX - 0.5f, transform.position.y, 0f);
        if (transform.position.x < -mapMaxX + 0.5f) transform.position = new Vector3(-mapMaxX + 0.5f, transform.position.y, 0f);
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

    // =======================���콺drag TEST��========================   


    //private void OnMouseDown()
    //{
    //    startTouch = Input.mousePosition;
    //    Debug.Log(startTouch);
    //    GameManager.instance.moveGuide.transform.position = Camera.main.ScreenToWorldPoint(startTouch) + Vector3.forward * 20;
    //    Debug.Log(GameManager.instance.moveGuide.transform.position);


    //}

    //private void OnMouseDrag()
    //{
    //    Vector3 vec = new Vector3(Input.mousePosition.x - startTouch.x, Input.mousePosition.y - startTouch.y, 0f);

    //    transform.Translate(vec.normalized * Mathf.Clamp(vec.magnitude / 1000, -0.3f, 0.3f) * Time.deltaTime * 4f * _speed);
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
                    GameManager.instance.moveGuide.transform.position = Camera.main.ScreenToWorldPoint(startTouch) + Vector3.forward * 20;
                    break;
                case UnityEngine.TouchPhase.Moved:
                    dragTouch = touch.position;

                    inputVec = dragTouch - startTouch;
                    //transform.Translate(inputVec * Time.deltaTime * _speed);
                    transform.Translate(inputVec.normalized * Mathf.Clamp(inputVec.magnitude / 1000, -0.3f, 0.3f) * Time.deltaTime * 4f * _speed);

                    if (transform.position.y < mapMinY) transform.position = new Vector3(transform.position.x, mapMinY, 0f);
                    if (transform.position.y > mapMaxY - 0.5f) transform.position = new Vector3(transform.position.x, mapMaxY - 0.5f, 0f);
                    if (transform.position.x > mapMaxX - 0.5f) transform.position = new Vector3(mapMaxX - 0.5f, transform.position.y, 0f);
                    if (transform.position.x < -mapMaxX + 0.5f) transform.position = new Vector3(-mapMaxX + 0.5f, transform.position.y, 0f);
                    break;
                case UnityEngine.TouchPhase.Canceled:
                    GameManager.instance.moveGuide.transform.position = new Vector3(100, 100, 0); //�Ⱥ��̴°����� ��������
                    break;

            }

        }
    }

    void OnNewaction(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
