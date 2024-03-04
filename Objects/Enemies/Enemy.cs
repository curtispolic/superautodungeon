namespace superautodungeon.Objects.Enemies;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Enemy : Character
{
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Enemy(MainGame inputParent): base(inputParent)
    {
        Random random = new();
        Attack = random.Next(1, 4);
        HP = random.Next(5, 15);
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("skeleton");
        HPTexture = GameParent.Content.Load<Texture2D>("heart");
        AttackTexture = GameParent.Content.Load<Texture2D>("attack");
        StatsFont = GameParent.Content.Load<SpriteFont>("statsFont");
        ShadowTexture = GameParent.Content.Load<Texture2D>("shadow50");
        DeathTexture = GameParent.Content.Load<Texture2D>("death");
    }
}