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
        if (GameManager.instance.shootType == 0) GameManager.instance.shootType = 1;
        else if (GameManager.instance.shootType == 1) GameManager.instance.shootType = 2;
        else if (GameManager.instance.shootType == 2) GameManager.instance.shootType = 0;
    }
}
