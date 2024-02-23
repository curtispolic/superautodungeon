namespace superautodungeon.Objects.Heroes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Hero
{
    public Vector2 position;
    public Texture2D texture;

    public int HP;
    public int Attack;
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    Hero()
    {
        // Intentional blank for now
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        // TODO: Drawing
    }
}