using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Orb : MonoBehaviour
{
    [SerializeField] public AudioClip hitSound;
    [SerializeField] public GameObject hitEffect;

    public float soundVol;
    [HideInInspector] public float addDamage;
    [HideInInspector] public int addPiercingNum;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        UpgradeStat();
    }

    public abstract void Init();
    public abstract void UpgradeStat();

    public abstract void MakeSound();
    public void MakeEffect(Collider2D collposition)
    {
        GameObject effect = Instantiate(hitEffect, collposition.transform.position, hitEffect.transform.rotation, GameObject.Find("OrbManager").transform);
        Destroy(effect, 1.5f);
    }






}
