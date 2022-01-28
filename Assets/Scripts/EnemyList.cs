using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class EnemyList
{
    private int level;
    private List<Enemy> enemies = new List<Enemy>();

    public void AddEnemy(Enemy enemy)
    {
        foreach (Enemy listEnemy in this.enemies)
        {
            if (listEnemy.GetName().Equals(enemy.GetName()))
            {
                List<string> toAdd = new List<string>();
                foreach (string id in enemy.GetIds())//There will always be only one if something is being picked up, but still better safe than sorry
                {
                    toAdd.Add(id);
                }
                foreach (string id in toAdd)
                {
                    listEnemy.AddOne(id);
                }
                return;
            }
        }
        this.enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        this.enemies.Remove(enemy);
    }

    public int GetLevel()
    {
        return this.level;
    }

    public List<Enemy> GetEnemies()
    {
        return this.enemies;
    }
}

