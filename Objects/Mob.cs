namespace superautodungeon.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using superautodungeon.Objects.Enemies;

public class Mob
{
    public List<Enemy> EnemyList;
    public MainGame GameParent;
    public Vector2 Position;

    public Mob(MainGame inputParent, Vector2 inputPosition)
    {
        GameParent = inputParent;
        EnemyList = new()
        {
            new Skeleton(GameParent),
            new Skeleton(GameParent),
            new Skeleton(GameParent),
            new Skeleton(GameParent)
        };
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
            EnemyList[i].Position = Position + new Vector2(i*100, 0);
        }
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Only used for combat drawing
        for (int i = 0; i < EnemyList.Count; i++)
        {
            var enemy = EnemyList[i];
            enemy.CombatDraw(spriteBatch, gameTime, new Vector2(600 + 100 * i, 200));
        }
    }
}