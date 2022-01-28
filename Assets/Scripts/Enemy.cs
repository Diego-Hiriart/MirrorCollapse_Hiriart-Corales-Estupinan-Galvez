using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class Enemy : Character
{
    private List<string> ids = new List<String>();//Name+level int + item instance, example: Monster11
    private string name;

    public Enemy(string name)
    {
        this.name = name;
    }

    public Enemy(string name, string itemID) : this(name)
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

    public void AddOne(string newID)
    {
        this.ids.Add(newID);
    }
}

