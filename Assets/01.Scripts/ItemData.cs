using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName ="Scriptable Object/ItemData")] 
public class ItemData : ScriptableObject
{
    public enum ItemType { Orb, Barrel, Core , Engine, Trash, Magnet, Accelerator}

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;
    


    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;


    [Header("# Effects")]
    public GameObject aura;
    public GameObject effect;
    public AudioClip hitSound;
}
