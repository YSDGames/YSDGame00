using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void Retry()
    {
        SoundManager.instance.SFXPlay("Click", SoundManager.instance.Clip[1], 0.7f);
        SceneManager.LoadScene("InGame");
        GameManager.instance.gameState = GameManager.GameState.ing;
        Time.timeScale = 1.0f;
    }

    public void Out()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.instance.SFXPlay("Click", SoundManager.instance.Clip[1], 0.7f);



    }

    public void OnStart()
    {
        SceneManager.LoadScene("InGame");
        SoundManager.instance.SFXPlay("Click", SoundManager.instance.Clip[1], 0.7f);
    }
}
