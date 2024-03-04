namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework.Graphics;

public class Knight : Hero
{
    public Knight(MainGame inputParent): base(inputParent)
    {
        Random random  = new();
        Gender = random.Next(2) == 1 ? "Male" : "Female";
        Description = "A sturdy, shield bearing Knight. It will be difficult for enemies to get past his plate armour.";
        Class = "Knight";
        Name = Gender == "Male" ? GameParent.nameGenerator.CreateMaleName() : GameParent.nameGenerator.CreateFemaleName();
        HP = 12;
        Attack = 2;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("knight");
        HPTexture = GameParent.Content.Load<Texture2D>("heart");
        AttackTexture = GameParent.Content.Load<Texture2D>("attack");
        StatsFont = GameParent.Content.Load<SpriteFont>("statsFont");
        ShadowTexture = GameParent.Content.Load<Texture2D>("shadow50");
        DeathTexture = GameParent.Content.Load<Texture2D>("death");
    }
}