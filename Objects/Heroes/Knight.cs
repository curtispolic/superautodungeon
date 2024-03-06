namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework.Graphics;

public class Knight : Hero
{
    public Knight(MainGame inputParent): base(inputParent, true)
    {
        Random random  = new();
        Gender = random.Next(2) == 1 ? "Male" : "Female";
        Description = "A sturdy, shield bearing Knight. It will be difficult for enemies to get past his plate armour.";
        Class = "Knight";
        Name = Gender == "Male" ? GameParent.nameGenerator.CreateMaleName() : GameParent.nameGenerator.CreateFemaleName();
        MaxHP = 12;
        CurrentHP = 12;
        Attack = 2;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("knight");
        base.LoadContent();
    }
}