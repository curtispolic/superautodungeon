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
        BaseAttack = 1;
        CurrentAttack = 1;
        Active = true;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("skeleton");
        base.LoadContent();
    }
}