namespace superautodungeon.Objects.Enemies;

using Microsoft.Xna.Framework.Graphics;

public class Rat : Enemy
{
    public Rat(MainGame inputParent): base(inputParent)
    {
        Description = "A little rat scurrying around.\nHas no special properties or abilities.";
        Name = "Rat";
        Class = "Rat";
        MaxHP = 4;
        VisibleHP = MaxHP;
        CurrentHP = MaxHP;
        BaseAttack = 1;
        CurrentAttack = 1;
        Active = true;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("Enemies/rat");
        base.LoadContent();
    }
}