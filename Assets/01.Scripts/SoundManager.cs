using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgSound;
    public AudioClip[] bgClip;
    public AudioClip[] Clip;

    public enum UISound
    { 
        menuClick,
        itemSelect,
        levelUp,
        die,
        hitted,
        healSound
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerPrefs.SetFloat("GameVolum", 1f);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgClip.Length; i++)
        {
            if (arg0.name == bgClip[i].name)
                BgSoundPlay(bgClip[i]);
        }
    }
    public void SFXPlay(string str, AudioClip clip, float volume)
    {
        GameObject go = new GameObject(str + "Sound");
        go.transform.parent = transform;

        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.5f;
        bgSound.Play();
    }

    public void UISounds(UISound sound)
    {
        SFXPlay(sound.ToString(), Clip[(int)sound], 0.5f);
    }
}
