using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class SaveData
{
    private PlayerCharacter player;
    private List<ItemList> items;
    private List<EnemyList> enemies;

    public SaveData()
    {

    }

    public SaveData(PlayerCharacter player, List<ItemList> itemlists, List<EnemyList> enemyLists)
    {
        this.player = player;
        foreach (ItemList list in itemlists)
        {
            this.items.Add(list);
        }
        foreach (EnemyList list in enemyLists)
        {
            this.enemies.Add(list);
        }
    }
}

