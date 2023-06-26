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
        Time.timeScale = 1.0f;
    }

    public void Out()
    {
        SceneManager.LoadScene("SampleScene");
        
    }

    public void ChangShootType()
    {
        GameManager.instance.shootType = GameManager.instance.shootType == 0 ? 1 : 0;
    }
}
