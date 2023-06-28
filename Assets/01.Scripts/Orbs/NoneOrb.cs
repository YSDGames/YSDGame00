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

    public override void UpgradeStat(){}
    public override void MakeSound() 
    {
        SoundManager.instance.SFXPlay("Hit", GameManager.instance.playerOrb.hitSound, 0.6f);
    }
}
