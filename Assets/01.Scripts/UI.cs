using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
        GameManager.instance.gameState = GameManager.GameState.ing;
        Time.timeScale = 1.0f;
    }

    public void Out()
    {
        SceneManager.LoadScene("SampleScene");
        
    }
}
