using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SaveData
{
    private PlayerCharacter player;
    private ItemList items = new ItemList();
    private List<EnemyList> enemies = new List<EnemyList>();
    private string level;

    public SaveData(){}

    public SaveData(string level, PlayerCharacter player, ItemList itemlists, EnemyList enemyLists)
    {
        this.level = level;
        this.player = player;
        this.items = itemlists;
        this.enemies.Add(enemyLists);
    }

    public PlayerCharacter GetPlayer()
    {
        return this.player;
    }

    public List<Item> GetPlayerInventory()
    {
        return this.player.GetInventory().GetItems();
    }

    public string GetLevel()
    {
        return this.level;
    }
}

