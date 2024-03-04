namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Hero : Character
{
    public string Class;
    public string Gender;
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Hero(MainGame inputParent): base(inputParent)
    {
        // Intentionally blank
    }
}