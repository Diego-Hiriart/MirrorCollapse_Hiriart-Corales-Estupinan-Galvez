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
}

