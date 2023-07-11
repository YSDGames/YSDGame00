using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    enum Achive { UnlockPotato, UnlockBean }
    Achive[] achives;

    WaitForSecondsRealtime wait;

    //����Ƽ���� edit - Clear All PlayerPrefs ������ �ѹ��� ���Ӿ��� �ʱ�ȭ���� ����!
    private void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);
        if (PlayerPrefs.HasKey("MyData"))
            Init();
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }
    private void Start()
    {
        UnlockCharactor();
    }

    void UnlockCharactor()
    {
        for (int i = 0; i < lockCharacter.Length; i++)
        {
            string achivaName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achivaName) == 1;
            lockCharacter[i].SetActive(false);
            unlockCharacter[i].SetActive(true);
        }
    }

    private void LateUpdate()
    {
        foreach (Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockPotato:
                isAchive = GameManager.instance.playerLevel >= 10;
                break;
            case Achive.UnlockBean:
                isAchive = GameManager.instance.GetTime() >= 12;
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);


            for (int i = 0; i < uiNotice.transform.childCount; i++)
            {
                //bool isActive = uiNotice.transform.GetChild(i).name == achive.ToString(); �̷����ϸ� �ȵǳ�?
               bool isActive = i == (int)achive;  //Notice �ڳ����� achive �迭������ �°�.
                uiNotice.transform.GetChild(i).gameObject.SetActive(isActive);
            }

            NoticeRoutine();
        }
    }
    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);

        yield return wait;

        uiNotice.SetActive(false);

    }
}
