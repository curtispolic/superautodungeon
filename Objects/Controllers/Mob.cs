namespace superautodungeon.Objects.Controllers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using superautodungeon.Objects.Enemies;
using Microsoft.Xna.Framework.Input;

public class Mob
{
    public List<Enemy> EnemyList;
    public MainGame GameParent;

    public Mob(MainGame inputParent)
    {
        GameParent = inputParent;
        EnemyList = new()
        {
            new Rat(GameParent),
            new Rat(GameParent),
            new Goblin(GameParent),
            new Skeleton(GameParent)
        };
    }

    public void CombatUpdate(MouseState mouseState, GraphicsDeviceManager graphics, GameTime gameTime)
    {
        // Mouseover handling
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            var enemy = EnemyList[i];
            if (enemy.Active && (!enemy.Dead || enemy.Dying))
            {
                enemy.CombatUpdate(mouseState, graphics, gameTime, new Vector2(600 + 100 * count, 200));
                count++;
            }
        }
    }

    public Enemy FrontEnemy()
    {
        foreach (var enemy in EnemyList)
        {
            if (enemy.Active)
            {
                if (!enemy.Dead)
                {
                    return enemy;
                }
            }
        }

        return null;
    }

    public List<Enemy> AliveEnemies()
    {
        List<Enemy> tempList = new();
        foreach (var enemy in EnemyList)
        {
            if (enemy.Active)
            {
                if (!enemy.Dead)
                {
                    tempList.Add(enemy);
                }
            }
        }

        return tempList;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // This method is only used to draw in combat
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            var enemy = EnemyList[i];
            if (enemy.Active && (!enemy.Dead || enemy.Dying))
            {
                enemy.CombatDraw(spriteBatch, gameTime, new Vector2(600 + 100 * count, 200));
                count++;
            }
        }
    }
}