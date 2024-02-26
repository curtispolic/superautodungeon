namespace superautodungeon.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Character
{
    public Vector2 Position;
    public Texture2D Texture;
    public Texture2D HPTexture;
    public Texture2D AttackTexture;
    public Texture2D ShadowTexture;
    public SpriteFont StatsFont;
    public int HP;
    public int Attack;
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Character()
    {
        // Intentional blank for now
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        // Drawing shadow
        spriteBatch.Draw(
            this.ShadowTexture,
            this.Position + new Vector2(0, 96),
            null,
            Color.White,
            0f,
            new Vector2(0, 0),
            Vector2.One,
            SpriteEffects.None,
            0f
        );

        // Drawing Self
        spriteBatch.Draw(
            this.Texture,
            this.Position,
            null,
            Color.White,
            0f,
            new Vector2(0, 0),
            Vector2.One,
            SpriteEffects.None,
            0f
        );

        // Drawing HP icon
        spriteBatch.Draw(
            this.HPTexture,
            this.Position + new Vector2(24, 128),
            null,
            Color.White,
            0f,
            new Vector2(0, 0),
            Vector2.One,
            SpriteEffects.None,
            0f
        );

        // Drawing attack
        spriteBatch.Draw(
            this.AttackTexture,
            this.Position + new Vector2(72, 128),
            null,
            Color.White,
            0f,
            new Vector2(0, 0),
            Vector2.One,
            SpriteEffects.None,
            0f
        );

        // Drawing HP text
        // Draws 4 offset versions for the black outline, then a white version on top
        spriteBatch.DrawString(this.StatsFont, this.HP.ToString(), this.Position + new Vector2(31,131), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.HP.ToString(), this.Position + new Vector2(29,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.HP.ToString(), this.Position + new Vector2(31,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.HP.ToString(), this.Position + new Vector2(29,131), Color.Black);

        spriteBatch.DrawString(this.StatsFont, this.HP.ToString(), this.Position + new Vector2(30,130), Color.White);

        // Attack text
        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(83,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(85,131), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(85,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(83,131), Color.Black);

        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(84,130), Color.White);
    }
}