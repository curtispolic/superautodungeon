namespace superautodungeon.Objects.Controllers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using superautodungeon.Objects.Enemies;

public class Mob
{
    public List<Enemy> EnemyList;
    public MainGame GameParent;

    public Mob(MainGame inputParent)
    {
        GameParent = inputParent;
        EnemyList = new()
        {
            new Skeleton(GameParent),
            new Skeleton(GameParent),
            new Skeleton(GameParent),
            new Skeleton(GameParent)
        };
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