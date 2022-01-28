using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemObject itemObject;

    private Item item;
    [SerializeField] private bool isPickable;
    [SerializeField] private bool isWeaponAmmo;
    public float ammoQuantity = 15;
    [SerializeField] private string itemName;
    private LevelController level;
    [SerializeField] private string itemID;

    // Start is called before the first frame update
    void Start()
    {
        this.item = new Item(isWeaponAmmo, itemName, isPickable, this.itemID);
        if (this.isWeaponAmmo)
        {           
            (this.itemObject as AmmoObject).quantity = ammoQuantity;
        }
        this.level = GetComponentInParent<LevelController>();
        this.AddThisToLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddThisToLevel()
    {
        this.level.AddItem(this);
    }

    public void PickItemUp()
    {
        if (this.item.IsPickable())
        {                     
            level.AddToPlayerInventory(this.item);
        }
    }

    public Item GetItem()
    {
        return this.item;
    }

    public string GetItemID()
    {
        return this.itemID;
    }
}
