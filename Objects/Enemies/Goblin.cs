namespace superautodungeon.Objects.Enemies;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Goblin : Enemy
{
    public Texture2D SpearTexture;
    public double SpearTimer;
    public int SpearTargetIndex;

    public static int SPEAR_TIME = 750;

    public Goblin(MainGame inputParent): base(inputParent)
    {
        Description = "A small green menace.\nThrows a spear for 1 damage at a random target if not in melee.";
        Name = "Goblin";
        MaxHP = 6;
        VisibleHP = MaxHP;
        CurrentHP = MaxHP;
        BaseAttack = 1;
        CurrentAttack = 1;
        Active = true;
        LoadContent();
    }

    public override double CombatStep(double animationDelay)
    {
        // Combat steps will return the time in milliseconds the animation will require to play.
        double animationTime = 0;

        var heroes = GameParent.combat.PlayerParty.AliveHeroes();

        if (heroes.Count > 0)
        {
            Random random = new();
            SpearTargetIndex = random.Next(heroes.Count);
            SpearTimer = 0 - animationDelay;
            animationTime = SPEAR_TIME;
            animationTime += heroes[SpearTargetIndex].TakeDamage(1, animationDelay + animationTime);
        }

        return animationTime;
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("Enemies/goblin");
        SpearTexture = GameParent.Content.Load<Texture2D>("Effects/arrow");
        base.LoadContent();
    }

    public override void CombatDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        if (SpearTimer < SPEAR_TIME && SpearTimer > 0)
        {
            // Using Math.Sin() to make the fireball arc
            double degrees = Math.PI * (SpearTimer/SPEAR_TIME*180) / 180.0;

            Vector2 arrowOffset = new Vector2(400 - 100 * SpearTargetIndex, 200) - position;
            arrowOffset.X *= (float)(SpearTimer / SPEAR_TIME);
            arrowOffset.X += 40;
            arrowOffset.Y = 0 - (float)Math.Sin(degrees) * 150;

            spriteBatch.Draw(SpearTexture, position + arrowOffset, null, Color.White, -0.5f + (float)(SpearTimer / SPEAR_TIME), new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }

        SpearTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        base.CombatDraw(spriteBatch, gameTime, position);
    }
}