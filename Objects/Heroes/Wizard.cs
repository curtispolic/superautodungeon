namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Wizard : Hero
{
    public Texture2D FireballTexture;
    public double FireballTimer;
    public int FireballTargetIndex;
    
    public static int FIREBALL_TIME = 1500;

    public Wizard(MainGame inputParent): base(inputParent, true)
    {
        Random random  = new();
        Gender = random.Next(2) == 1 ? "Male" : "Female";
        Description = "A wise old wizard.\nWizards will spend 2 mana to cast a fireball if not in melee.\nFireball does 2 damage per level to a random target.\nWill deal half to adjacent targets.";
        Class = "Wizard";
        Name = Gender == "Male" ? GameParent.nameGenerator.CreateMaleName() : GameParent.nameGenerator.CreateFemaleName();
        MaxHP = 6;
        VisibleHP = MaxHP;
        CurrentHP = MaxHP;
        BaseAttack = 1;
        CurrentAttack = 1;
        FireballTimer = FIREBALL_TIME + 1;
        Cost = 50;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("Heroes/wizard");
        FireballTexture = GameParent.Content.Load<Texture2D>("Effects/fireball");
        base.LoadContent();
    }

    public override double CombatStep(double animationDelay)
    {
        // Combat steps will return the time in milliseconds the animation will require to play.
        double animationTime = 0;

        // Make sure there's a target and have mana
        if (Mana >= 2 && GameParent.combat.EnemyMob.AliveEnemies().Count > 0)
        {
            animationTime += Fireball(animationDelay + animationTime);
            Mana -= 2;
            return animationTime;
        }
        else
        {
            animationTime += GainMana(1, animationDelay + animationTime);
            return animationTime;
        }
    }

    public double Fireball(double animationDelay)
    {
        // Fireball targets a random enemy and does 2/4/6 damage, and half that to adjacent targets
        double animationTime = FIREBALL_TIME;
        Random random = new();
        var enemies = GameParent.combat.EnemyMob.AliveEnemies();
        FireballTargetIndex = random.Next(enemies.Count);
        FireballTimer = 0 - animationDelay;

        animationTime += enemies[FireballTargetIndex].TakeDamage(Level * 2, animationDelay + animationTime);

        // Make sure we don't go out of range
        if (FireballTargetIndex > 0)
        {
            animationTime += enemies[FireballTargetIndex-1].TakeDamage(Level, animationDelay + animationTime);
        }
        if (FireballTargetIndex < enemies.Count - 1)
        {
            animationTime += enemies[FireballTargetIndex+1].TakeDamage(Level, animationDelay + animationTime);
        }

        return animationTime;
    }

    public override void CombatDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        if (FireballTimer < FIREBALL_TIME && FireballTimer > 0)
        {
            // Using Math.Sin() to make the fireball arc
            double degrees = Math.PI * (FireballTimer/FIREBALL_TIME*180) / 180.0;

            // Offset uses the same vector Mob uses to draw the enemy position + a little bit to look good
            Vector2 fireballOffset = new Vector2(600 + 20 + 100 * FireballTargetIndex, 200) - position;
            fireballOffset.X *= (float)(FireballTimer / FIREBALL_TIME);
            fireballOffset.Y = 0 - (float)Math.Sin(degrees) * 150;

            spriteBatch.Draw(FireballTexture, position + fireballOffset, null, Color.White, -0.5f + (float)(FireballTimer / FIREBALL_TIME), new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }

        ManaDraw(spriteBatch, gameTime, position);

        FireballTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        base.CombatDraw(spriteBatch, gameTime, position);
    }
}