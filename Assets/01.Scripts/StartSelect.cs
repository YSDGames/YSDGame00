using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

public class StartSelect : MonoBehaviour
{
    bool trigger = true;
    
    public StartSelect selectOrb;
    public void Stright()
    {
        GameManager.instance.shootType = 0;

        selectOrb.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Spread()
    {
        GameManager.instance.shootType = 1;

        selectOrb.gameObject.SetActive(true);
        gameObject.SetActive(false);

    }
    public void Rotate()
    {
        GameManager.instance.shootType = 2;

        selectOrb.gameObject.SetActive(true);
        gameObject.SetActive(false);

    }

    public void SelectOrb() 
    {
        if (trigger)
        {
            GameManager.instance.playerOrb = GetComponent<Item>();

            transform.parent = GameObject.Find("UI/LevelUp/Panel/Items").transform;
            transform.localScale = Vector3.one * 0.9f;
            gameObject.SetActive(false);

            trigger = false;
            GameManager.instance.gameState = GameManager.GameState.ing;
            Time.timeScale = 1f;

            selectOrb.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
