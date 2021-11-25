using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PlayerCharacter : Character
{
    private ItemList inventory = new ItemList();

    public PlayerCharacter() {}

    public PlayerCharacter(SaveTransform transf, float health, ItemList items)
    {
        this.characterTransform = transf;
        this.characterHealth = health;
        this.inventory = items;
    }

    public List<Item> GetItems()
    {
        return this.inventory.GetItems();
    }

    public ItemList GetInventory()
    {
        return this.inventory;
    }

    public void AddToInventory(Item item, string newID)
    {
        this.inventory.AddItem(item, newID);
    }
    
}

