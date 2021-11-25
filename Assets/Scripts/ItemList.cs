using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class ItemList
{
    private int level;
    private List<Item> items = new List<Item>();

    public void AddItem(Item item, string newId)
    {
        foreach (Item listItem in this.items)
        {
            if (listItem.GetName().Equals(item.GetName()))
            {
                listItem.AddOne(newId);
                return;
            }
        }
        this.items.Add(item);
    }

    public int GetLevel()
    {
        return this.level;
    }

    public void RemoveItem(Item item)
    {
        this.items.Remove(item);
    }

    public List<Item> GetItems()
    {
        return this.items;
    }
}

