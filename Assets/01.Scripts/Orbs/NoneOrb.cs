using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneOrb : Orb
{
    public override void Init()
    {
        addDamage = 0;
        addPiercingNum = 0;
    }

    public override void UpgradeStat()
    {
        if (GameManager.instance.playerLevel < 5)
        {
            hitEffect.transform.localScale = Vector3.one * 0.3f;
        }
        else if (GameManager.instance.playerLevel < 10)
        {
            hitEffect.transform.localScale = Vector3.one * 0.4f;
        }
        else if (GameManager.instance.playerLevel < 15)
        {
            hitEffect.transform.localScale = Vector3.one * 0.5f;
        }
        else if (GameManager.instance.playerLevel < 20)
        {
            hitEffect.transform.localScale = Vector3.one * 0.6f;
        }

    }

    public override void MakeSound() 
    {
        SoundManager.instance.SFXPlay("Hit", GameManager.instance.playerOrb.hitSound, 0.6f);
    }
}
