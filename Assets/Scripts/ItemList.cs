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

    public void AddItem(Item item)
    {
        foreach (Item listItem in this.items)
        {
            if (listItem.GetName().Equals(item.GetName()))
            {
                List<string> toAdd = new List<string>();
                foreach (string id in item.GetIds())//There will always be only one if something is being picked up, but still better safe than sorry
                {
                    toAdd.Add(id);                 
                }
                foreach (string id in toAdd)
                {
                    listItem.AddOne(id);
                }
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

