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

    public Hero()
    {
        // Intentional blank for now
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        // TODO: Drawing
        spriteBatch.Draw(
            this.texture,
            this.position,
            null,
            Color.White,
            0f,
            new Vector2(0, 0),
            Vector2.One,
            SpriteEffects.None,
            0f
        );
    }
}