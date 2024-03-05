namespace superautodungeon.Objects.Controllers;

using superautodungeon.Objects.Heroes;
using superautodungeon.Objects.Enemies;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Combat
{
    public MainGame GameParent;
    public Party PlayerParty;
    public Mob EnemyMob;
    public bool Ongoing, Active;

    public Combat()
    {
        Active = false;
    }

    public Combat(MainGame inputParent, Party inputParty, Mob inputMob)
    {
        GameParent = inputParent;
        PlayerParty = inputParty;
        EnemyMob = inputMob;
        Ongoing = true;
        Active = true;
    }

    public void Update()
    {

    }

    public void BeginRound()
    {
        // Move forward enemies in the list
        List<Enemy> tempList2 = new();
        foreach (var enemy in EnemyMob.EnemyList)
        {
            if (!enemy.Dead)
                tempList2.Add(enemy);
        }
        EnemyMob.EnemyList = tempList2;

        PlayerParty.Reposition();
        EnemyMob.Reposition();
    }

    public void MeleeHit()
    {
        Hero frontHero = new(GameParent);
        foreach (Hero hero in PlayerParty.HeroList)
        {
            if (!hero.Dead)
            {
                frontHero = hero;
                break;
            }
        }
        Enemy frontEnemy = EnemyMob.EnemyList[0];
        frontHero.CurrentHP -= frontEnemy.Attack;
        frontEnemy.CurrentHP -= frontHero.Attack;

        // Handle the animation
    }

    public void OnAttackTriggers()
    {
        return;
    }

    public void HandleDeath()
    {
        bool wasDeath;
        do
        {
            wasDeath = false;
            foreach (var hero in PlayerParty.HeroList)
            {
                if (hero.CurrentHP <= 0)
                {
                    wasDeath = hero.Die();
                }
            }
            foreach (var enemy in EnemyMob.EnemyList)
            {
                if (enemy.CurrentHP <= 0)
                {
                    wasDeath = enemy.Die();
                }
            }
        } while (wasDeath);
    }

    public void EndRound()
    {
        // Check if all heroes or all enemies are dead
        int heroCheck = 0; int enemyCheck = 0;
        foreach (var hero in PlayerParty.HeroList)
        {
            if (hero.Dead)
                heroCheck++;
        }
        foreach (var enemy in EnemyMob.EnemyList)
        {
            if (enemy.Dead)
                enemyCheck++;
        }
        if (enemyCheck == EnemyMob.EnemyList.Count || heroCheck == PlayerParty.HeroList.Count)
        {
            Ongoing = false;
            Active = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Temp texture to draw the outline of the panel
        Texture2D _texture;
        _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        _texture.SetData(new Color[] { new Color(150, 150, 150) });

        spriteBatch.Draw(_texture, new Rectangle(20, 20, 1240, 590), Color.White);

        // Temp texture to draw inner colour of the panel
        Texture2D _texture2;
        _texture2 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        _texture2.SetData(new Color[] { new Color(200, 200, 200) });

        spriteBatch.Draw(_texture2, new Rectangle(25, 25, 1230, 580), Color.White);

        // Draw both parties for the combat
        PlayerParty.Draw(spriteBatch, gameTime);
        EnemyMob.Draw(spriteBatch, gameTime);
    }
}