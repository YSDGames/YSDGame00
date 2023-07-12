using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Toggles : MonoBehaviour
{
    Toggle toggle;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        if (PlayerPrefs.GetInt("ControlMode") == 0) toggle.isOn = true;
        else toggle.isOn = false;
    }
}
