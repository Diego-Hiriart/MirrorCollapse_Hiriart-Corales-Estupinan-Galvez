using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    [SerializeField] GameObject itemPanel;
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescText;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    private void Start()
    {
        DeleteDisplayedItems();
        CreateDisplay();
    }

    private void Update()
    {
        HandleDisplay();
    }

    public void DeleteDisplayedItems()
    {
        foreach (Transform child in itemPanel.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void CreateDisplay()
    {
        DeleteDisplayedItems();

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.uiDisplay, Vector3.zero, Quaternion.identity, itemPanel.transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            obj.name = inventory.Container[i].item.itemName;
        }
    }

    public void HandleDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var item = inventory.Container[i];
            if(itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[item].GetComponentInChildren<TextMeshProUGUI>().text = item.amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.uiDisplay, Vector3.zero, Quaternion.identity, itemPanel.transform);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                obj.name = inventory.Container[i].item.itemName;
                itemsDisplayed.Add(item, obj);
            }
        }
    }
    
}
