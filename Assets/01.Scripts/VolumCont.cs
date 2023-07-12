using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumCont : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("GameVolum");
    }
    public void OnSetVolum(float sliderValue)
    {
        PlayerPrefs.SetFloat("GameVolum", sliderValue);
        mixer.SetFloat("Volum", Mathf.Log10(sliderValue) * 20);
    }

}
