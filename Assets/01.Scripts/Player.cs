using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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

    

    private Vector3 inputVec;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        nowHp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HpUpdate();
        Dead();
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
            gameObject.SetActive(false);
            GameManager.instance.GameOver();
        }
    }

    void Move()
    {
        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");

        inputVec = inputVec.magnitude > 1 ? inputVec.normalized : inputVec;

        transform.Translate(inputVec * _speed * Time.deltaTime);

        if (transform.position.y < mapMinY) transform.position = new Vector3(transform.position.x, mapMinY, 0f);
        if (transform.position.y > mapMaxY - 0.5f) transform.position = new Vector3(transform.position.x, mapMaxY - 0.5f, 0f);
        if (transform.position.x > mapMaxX - 0.5f) transform.position = new Vector3(mapMaxX - 0.5f, transform.position.y, 0f);
        if (transform.position.x < -mapMaxX + 0.5f) transform.position = new Vector3(-mapMaxX + 0.5f, transform.position.y, 0f);
    }
    public void MakeSound()
    {
        SoundManager.instance.SFXPlay("Hit", GameManager.instance.playerOrb.clip, 0.4f);
        
    }

    public void MakeEffect(Collider2D collposition)
    {
        StartCoroutine(GetEffect(collposition));
    }

    IEnumerator GetEffect(Collider2D collposition)
    {
        GameObject _effect = GameManager.instance.effectPool.GetPool(GameManager.instance.playerOrb.effID, collposition.transform.position, GameManager.instance.playerOrb.data.effect.transform.rotation);
        yield return new WaitForSeconds(1.5f);

        if (_effect != null)
            _effect.SetActive(false);
    }
}
