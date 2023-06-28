using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public static Orb instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    enum TypeOrb
    {
        Fire,
        Electric,
        Wind
    }

    void SetStat(TypeOrb Type)
    {
        switch (Type)
        {
            case 0:
                break;
        }
    }
}
