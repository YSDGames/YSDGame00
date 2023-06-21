using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieUI : MonoBehaviour
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
}
