namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Priest : Hero
{
    public Texture2D HealTexture;
    public double HealTimer;
    
    public static int HEAL_TIME = 1000;
    public static Vector2 HEAL_POSITION = new(400, 70);

    public Priest(MainGame inputParent): base(inputParent, true)
    {
        Random random  = new();
        Gender = random.Next(2) == 1 ? "Male" : "Female";
        Description = "A devout priest of a non-specific monotheistic religion.\nPriests will spend 3 mana to heal if not in melee.\nWill heal for 2/4/6 health.\nWill only heal the front unit and only if damaged.";
        Class = "Priest";
        Name = Gender == "Male" ? GameParent.nameGenerator.CreateMaleName() : GameParent.nameGenerator.CreateFemaleName();
        MaxHP = 6;
        CurrentHP = 6;
        BaseAttack = 1;
        CurrentAttack = 1;
        HealTimer = HEAL_TIME + 1;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("Heroes/priest");
        HealTexture = GameParent.Content.Load<Texture2D>("Effects/priest_heal");
        base.LoadContent();
    }

    public override double CombatStep(double animationDelay)
    {
        // Combat steps will return the time in milliseconds the animation will require to play.
        double animationTime = 0;
        var hero = GameParent.playerParty.FrontHero();

        // Make sure damaged front hero and have mana
        if (Mana >= 3 && hero.CurrentHP < hero.MaxHP)
        {
            animationTime += Heal(animationDelay + animationTime);
            Mana -= 3;
            return animationTime;
        }
        else
        {
            animationTime += GainMana(1, animationDelay + animationTime);
            return animationTime;
        }
    }

    public double Heal(double animationDelay)
    {
        double animationTime = HEAL_TIME;
        var healTarget = GameParent.playerParty.FrontHero();

        HealTimer = 0 - animationDelay;
        animationTime += healTarget.ReceiveHealing(2, animationDelay + animationTime / 2);

        return animationTime;
    }

    public override void CombatDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        if (HealTimer < HEAL_TIME && HealTimer > 0)
        {
            spriteBatch.Draw(HealTexture, HEAL_POSITION, null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }

        ManaDraw(spriteBatch, gameTime, position);

        HealTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        base.CombatDraw(spriteBatch, gameTime, position);
    }
}