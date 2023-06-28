using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricOrb : Orb
{
    public override void Init()
    {
        addDamage = 0;
        addPiercingNum = 1;
    }

    public override void UpgradeStat()
    {
        if (GameManager.instance.playerLevel < 5)
        {
            addPiercingNum = 1;
            hitEffect.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else if (GameManager.instance.playerLevel < 10)
        {
            addPiercingNum = 2;
            hitEffect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        else if (GameManager.instance.playerLevel < 15)
        {
            addPiercingNum = 3;
            hitEffect.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        }
        else if (GameManager.instance.playerLevel < 20)
        {
            addPiercingNum = 4;
            hitEffect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        }
    }
    public override void MakeSound()
    {
        SoundManager.instance.SFXPlay("Hit", GameManager.instance.playerOrb.hitSound, 0.6f);
    }
}

