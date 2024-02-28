namespace superautodungeon.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using superautodungeon.Objects.Enemies;

public class Mob
{
    public List<Enemy> EnemyList;
    public Vector2 Position;

    public Mob(Vector2 inputPosition)
    {
        EnemyList = new();
        Position = inputPosition;
    }

    public bool Add(Enemy inputEnemy)
    {
        if (EnemyList.Count < 4)
        {
            EnemyList.Add(inputEnemy);
            Reposition();
            return true;
        }
        return false;
    }

    public void Compress()
    {
        foreach (var enemy in EnemyList)
        {
            if (!enemy.Dead)
            {
                EnemyList.Remove(enemy);
            }
        }
        Reposition();
    }

    public void Reposition()
    {
        for (int i = 0; i<EnemyList.Count; i++)
        {
            EnemyList[i].Position = Position + new Vector2(i*128, 0);
        }
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var enemy in EnemyList)
        {
            enemy.Draw(spriteBatch, gameTime);
        }
    }
}