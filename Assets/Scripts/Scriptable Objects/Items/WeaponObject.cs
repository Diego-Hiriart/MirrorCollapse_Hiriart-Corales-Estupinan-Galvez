using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public float atkDamage;
    public bool isGun;

    public void Awake()
    {
        type = ItemType.Weapon;
    }
}