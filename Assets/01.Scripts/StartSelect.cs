using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

public class StartSelect : MonoBehaviour
{
    bool trigger = true;
    
    public StartSelect selectOrb;

    private void Start()
    {
    }
    public void Stright()
    {
        SoundManager.instance.SFXPlay("Click", SoundManager.instance.Clip[1], 0.7f);

        GameManager.instance.shootType = 0;

        selectOrb.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Spread()
    {
        SoundManager.instance.SFXPlay("Click", SoundManager.instance.Clip[1], 0.7f);

        GameManager.instance.shootType = 1;

        selectOrb.gameObject.SetActive(true);
        gameObject.SetActive(false);

    }
    public void Rotate()
    {
        SoundManager.instance.SFXPlay("Click", SoundManager.instance.Clip[1], 0.7f);

        GameManager.instance.shootType = 2;

        selectOrb.gameObject.SetActive(true);
        gameObject.SetActive(false);

    }

    public void SelectOrb() 
    {
        SoundManager.instance.SFXPlay("Click", SoundManager.instance.Clip[1], 0.7f);

        if (trigger)
        {
            trigger = false;

            GameManager.instance.playerOrb = GetComponent<Item>();

            transform.parent = GameObject.Find("UI/LevelUp/Panel/Items").transform;
            transform.localScale = Vector3.one * 0.9f;

            GameManager.instance.gameState = GameManager.GameState.ing;
            Time.timeScale = 1f;

            selectOrb.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
