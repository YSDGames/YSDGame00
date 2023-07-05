using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public enum ItemType { Orb, Barrel, Core , Engine, SpaceTrash }

    [Header("# Main Info")]
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]

    [Header("# ItemImage")]
    public int a;
}
