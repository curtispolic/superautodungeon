namespace superautodungeon.Objects.Enemies;

using Microsoft.Xna.Framework.Graphics;

public class Rat : Enemy
{
    public Rat(MainGame inputParent): base(inputParent)
    {
        Description = "A little rat scurrying around the dungeon.";
        Name = "Rat";
        MaxHP = 3;
        CurrentHP = 3;
        BaseAttack = 1;
        CurrentAttack = 1;
        Active = true;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("rat");
        base.LoadContent();
    }
}