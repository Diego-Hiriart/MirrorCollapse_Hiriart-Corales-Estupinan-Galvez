using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo Object", menuName = "Inventory/Items/Ammo")]
public class AmmoObject : ItemObject
{
    public float quantity;
    public float maxQuantity;

    public void Awake()
    {
        type = ItemType.Ammo;
        quantity = maxQuantity;
    }
}
