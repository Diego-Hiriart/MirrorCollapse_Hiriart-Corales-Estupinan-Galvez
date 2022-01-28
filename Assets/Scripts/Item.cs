using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class Item
{
    private bool weaponAmmo;
    private int ammoAmount;
    private string name;
    private bool pickable;
    private List<string> ids = new List<String>();//Name+level int + item instance, example: Donut11
    private bool health;
    public int amountUsed = 0;

    public Item(bool isWeaponAmmo, string name, bool isPickable)
    {
        this.weaponAmmo = isWeaponAmmo;
        this.name = name;
        this.pickable = isPickable;
    }

    public Item(bool isWeaponAmmo, string name, bool isPickable, string itemID) : this(isWeaponAmmo, name, isPickable)
    {
        this.ids.Add(itemID);
    }

    public string GetName()
    {
        return this.name;
    }

    public List<string> GetIds()
    {
        return this.ids;
    }

    public bool IsWeaponAmmo()
    {
        return this.weaponAmmo;
    }

    public void SetAmmoAmount(int amount)
    {
        this.ammoAmount = amount;
    }

    public int GetAmmoAmount()
    {
        return this.ammoAmount;
    }

    public bool IsPickable()
    {
        return this.pickable;
    }

    public void AddOne(string newID)
    {
        this.ids.Add(newID);  
    }

    //Add or substract ammo
    public void ChangeAmmo(int amount)
    {
        this.SetAmmoAmount(this.GetAmmoAmount()+amount);
    }
}