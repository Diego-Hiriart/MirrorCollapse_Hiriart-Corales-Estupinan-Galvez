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
    private EnemyList defeatedEnemies = new EnemyList();
    private string level;

    public SaveData(){}

    public SaveData(string level, PlayerCharacter player, ItemList itemlists, EnemyList enemies)
    {
        this.level = level;
        this.player = player;
        this.items = itemlists;
        this.defeatedEnemies = enemies;
    }

    public PlayerCharacter GetPlayer()
    {
        return this.player;
    }

    public List<Item> GetPlayerInventory()
    {
        return this.player.GetInventory().GetItems();
    }

    public List<Enemy> GetPlayerDefeatedEnemies()
    {
        return this.defeatedEnemies.GetEnemies();
    }

    public string GetLevel()
    {
        return this.level;
    }
}

