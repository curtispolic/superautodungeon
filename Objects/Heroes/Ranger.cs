namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Ranger : Hero
{
    public Texture2D ArrowTexture;
    public double ArrowTimer;
    public int ArrowTargetIndex;
    
    public static int ARROW_TIME = 750;

    public Ranger(MainGame inputParent): base(inputParent, true)
    {
        Random random  = new();
        Gender = random.Next(2) == 1 ? "Male" : "Female";
        Description = "A ranger of the wilds.\nIf not in melee, will shoot a random enemy target each turn.\nArrows deal 1/2/3 damage.";
        Class = "Ranger";
        Name = Gender == "Male" ? GameParent.nameGenerator.CreateMaleName() : GameParent.nameGenerator.CreateFemaleName();
        MaxHP = 8;
        CurrentHP = 8;
        BaseAttack = 1;
        CurrentAttack = 1;
        ArrowTimer = ARROW_TIME + 1;
        Cost = 50;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("Heroes/ranger");
        ArrowTexture = GameParent.Content.Load<Texture2D>("Effects/arrow");
        base.LoadContent();
    }

    public override double CombatStep(double animationDelay)
    {
        // Combat steps will return the time in milliseconds the animation will require to play.
        double animationTime = 0;

        var enemies = GameParent.combat.EnemyMob.AliveEnemies();

        if (enemies.Count > 0)
        {
            Random random = new();
            ArrowTargetIndex = random.Next(enemies.Count);
            ArrowTimer = 0 - animationDelay;
            animationTime = ARROW_TIME;
            animationTime += enemies[ArrowTargetIndex].TakeDamage(1, animationDelay + animationTime);
        }

        return animationTime;
    }

    public override void CombatDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        if (ArrowTimer < ARROW_TIME && ArrowTimer > 0)
        {
            // Using Math.Sin() to make the fireball arc
            double degrees = Math.PI * (ArrowTimer/ARROW_TIME*180) / 180.0;

            // Offset uses the same vector Mob uses to draw the enemy position + a little bit to look good
            Vector2 arrowOffset = new Vector2(600 + 100 * ArrowTargetIndex, 200) - position;
            arrowOffset.X *= (float)(ArrowTimer / ARROW_TIME);
            arrowOffset.X += 40;
            arrowOffset.Y = 0 - (float)Math.Sin(degrees) * 150;

            spriteBatch.Draw(ArrowTexture, position + arrowOffset, null, Color.White, -0.5f + (float)(ArrowTimer / ARROW_TIME), new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }

        ArrowTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        base.CombatDraw(spriteBatch, gameTime, position);
    }
}