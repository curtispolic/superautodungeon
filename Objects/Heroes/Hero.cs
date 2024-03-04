namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Hero : Character
{
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Hero(MainGame inputParent): base(inputParent)
    {
        Random random = new();
        Attack = random.Next(2, 5);
        HP = random.Next(10, 20);
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