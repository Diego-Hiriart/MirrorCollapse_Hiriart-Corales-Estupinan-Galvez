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
    private float minHealth = 0;
    private float maxHealth = 100;

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

    public void SetInventory(ItemList inventory)
    {
        this.inventory = inventory;
    }

    public ItemList GetInventory()
    {
        return this.inventory;
    }

    public void AddToInventory(Item item)
    {
        this.inventory.AddItem(item);
    }

    public float GetMinHealth()
    {
        return this.minHealth;
    }

    public float GetMaxHealth()
    {
        return this.maxHealth;
    }

    public Item GetAmmoItem()
    {
        foreach (Item item in this.inventory.GetItems())
        {
            if (item.IsWeaponAmmo())
            {
                return item;
                break;
            }
        }
        return null;//If there is no ammo in the inventory
    }
}

