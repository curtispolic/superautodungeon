namespace superautodungeon.Objects.Controllers;

using superautodungeon.Objects.Heroes;
using superautodungeon.Objects.Enemies;

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using superautodungeon.Objects.Rooms;

public class Combat
{
    public MainGame GameParent;
    public Party PlayerParty;
    public Mob EnemyMob;
    public Room RoomParent;
    public bool Begun, Finished, Active;
    public double AnimationTime;

    public Combat()
    {
        Active = false;
    }

    public Combat(MainGame inputParent, Room inputRoomParent, Party inputParty, Mob inputMob)
    {
        GameParent = inputParent;
        PlayerParty = inputParty;
        EnemyMob = inputMob;
        RoomParent = inputRoomParent;
        AnimationTime = 0;
        Begun = false;
        Finished = false;
        Active = true;
    }

    /*
    The goal is for combat to be handled as follows:
    - The two front units will melee each other
        - The enemy will take damage first, but if they die, the hero will still take damage
    - Effects will trigger in the following order:
        - On damage taken
        - On attack
            - Potentially another on damage taken, and so on...
        - Player effects will always resolve before enemy effects
    - Then moving down the party, each will resolve their abilities:
        - Player party first, in order
        - Enemy party second, in order
    - After each ability, damage taken effects may trigger
        - All triggers caused by an ability must be resolved before moving to the next
    - Death will be checked for after each trigger
        - If a character dies, it may trigger a damage taken trigger, or a death trigger, but that is all
    - Characters remain in place until the end of a combat round, then they will be shuffled forward
    */

    public double Update(bool updatingCombat)
    {
        // Only update combat once a round, but always check the buttons
        if (updatingCombat)
        {
            // Will return the number of milliseconds the game should wait before calling again
            double animationTime = 0;
            
            // Combat start animations (characters bobbing onto screen)
            if (!Begun)
            {
                animationTime += BeginCombat();
            }

            // Melee and associated triggers
            animationTime += MeleeHit(animationTime);

            // Combat steps for backline units
            foreach (var hero in PlayerParty.HeroList)
            {
                if (hero.Active)
                {
                    if (!hero.Dead && hero != PlayerParty.FrontHero())
                    {
                        animationTime += hero.CombatStep(animationTime);
                    }
                }
            }

            foreach (var enemy in EnemyMob.EnemyList)
            {
                if (enemy.Active)
                {
                    if (!enemy.Dead && enemy != EnemyMob.EnemyList[0])
                    {
                        animationTime += enemy.CombatStep(animationTime);
                    }
                }
            }

            animationTime += EndRound();

            return animationTime;
        }
        else 
        {
            return 0;
        }
    }

    public double BeginCombat()
    {
        // Just hard-coded 200 for now
        double animationTime = 200;

        // Handle start of combat stuff here
        // Make sure everything starts at zero mana
        foreach (var hero in PlayerParty.HeroList)
        {
            if (hero.Active)
            {
                hero.Mana = 0;
            }
        }

        Begun = true;
        return animationTime;
    }

    public double MeleeHit(double animationDelay)
    {
        // Base melee hit time is 500
        double animationTime = 500;

        Hero frontHero = PlayerParty.FrontHero();
        Enemy frontEnemy = EnemyMob.FrontEnemy();

        // Melee hit returns animation time but they happen simultaneously so just a base of 500 is used
        frontHero.MeleeHit(animationDelay);
        frontEnemy.MeleeHit(animationDelay);

        animationTime += frontEnemy.TakeDamage(frontHero.CurrentAttack, animationDelay + animationTime);
        animationTime += frontHero.TakeDamage(frontEnemy.CurrentAttack, animationDelay + animationTime);

        return animationTime;
    }

    public double EndRound()
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
            Active = false;
            RoomParent.Completed = true;
        }

        // Placeholder 500 to delay end round a touch
        return 500;
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