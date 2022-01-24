using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Health,
    Weapon,
    Ammo,
    Collectible
}

public abstract class ItemObject : ScriptableObject
{
    public int id;
    public GameObject uiDisplay;
    public ItemType type;
    public string itemName;
    [TextArea(15,20)] public string description;
}
