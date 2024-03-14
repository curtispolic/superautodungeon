namespace superautodungeon.Objects.Enemies;

using System;
using Microsoft.Xna.Framework.Graphics;

public class Skeleton : Enemy
{
    public Skeleton(MainGame inputParent): base(inputParent)
    {
        Description = "Quite spooky and/or scary.\nHas no special properties or abilities.";
        Name = "Skeleton";
        MaxHP = 6;
        CurrentHP = 6;
        BaseAttack = 2;
        CurrentAttack = 2;
        Active = true;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("Enemies/skeleton");
        base.LoadContent();
    }
}