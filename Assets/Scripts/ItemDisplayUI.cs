using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayUI : MonoBehaviour
{
    InventoryController inventory;

    private void Start()
    {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<InventoryController>();
    }

    public void SelectThisItem(GameObject itemImage)
    {
        if(inventory == null)
            inventory = GameObject.FindWithTag("Inventory").GetComponent<InventoryController>();

        inventory.SelectItem(itemImage);
    }
}
