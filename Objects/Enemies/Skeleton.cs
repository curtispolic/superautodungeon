namespace superautodungeon.Objects.Enemies;

using System;
using Microsoft.Xna.Framework.Graphics;

public class Skeleton : Enemy
{
    public Skeleton(MainGame inputParent): base(inputParent)
    {
        Description = "Quite spooky and/or scary.";
        Name = "Skeleton";
        MaxHP = 5;
        CurrentHP = 5;
        Attack = 1;
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