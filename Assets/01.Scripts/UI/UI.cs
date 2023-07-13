using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject option; 
    public void Retry()
    {
        SoundManager.instance.UISounds(SoundManager.UISound.menuClick);
        SceneManager.LoadScene("InGame");
        GameManager.instance.gameState = GameManager.GameState.ing;
        Time.timeScale = 1.0f;
    }

    public void Out()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.instance.UISounds(SoundManager.UISound.menuClick);

    }

    public void OnStart()
    {
        SceneManager.LoadScene("InGame");
        SoundManager.instance.UISounds(SoundManager.UISound.menuClick);
    }

    public void OnOption()
    {
        SoundManager.instance.UISounds(SoundManager.UISound.menuClick);
        option.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Close()
    {
        SoundManager.instance.UISounds(SoundManager.UISound.menuClick);
        option.SetActive(false);
    }

    public void Open()
    {
        SoundManager.instance.UISounds(SoundManager.UISound.menuClick);
        option.SetActive(true);
    }
    public void OnOptionClose()
    {
        SoundManager.instance.UISounds(SoundManager.UISound.menuClick);
        option.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void JoyToggle(bool mode)
    {
        if (mode) PlayerPrefs.SetInt("ControlMode",0);
        else PlayerPrefs.SetInt("ControlMode", 1);
    }
}
