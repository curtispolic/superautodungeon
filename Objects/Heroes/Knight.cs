namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework.Graphics;

public class Knight : Hero
{
    public Knight(MainGame inputParent): base(inputParent, true)
    {
        Random random  = new();
        Gender = random.Next(2) == 1 ? "Male" : "Female";
        Description = "A sturdy, shield bearing Knight.\nKnights will take 1/2/3 less damage from all instances";
        Class = "Knight";
        Name = Gender == "Male" ? GameParent.nameGenerator.CreateMaleName() : GameParent.nameGenerator.CreateFemaleName();
        MaxHP = 12;
        CurrentHP = 12;
        BaseAttack = 2;
        CurrentAttack = 2;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("knight");
        base.LoadContent();
    }

    public override double TakeDamage(int damage, double animationDelay)
    {
        damage -= 1;
        if (damage > 0)
        {
            return base.TakeDamage(damage, animationDelay);
        }
        else
        {
            return base.TakeDamage(0, animationDelay);
        }
    }
}