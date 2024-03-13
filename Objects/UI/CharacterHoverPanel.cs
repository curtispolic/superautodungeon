namespace superautodungeon.Objects.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using superautodungeon.Objects.Controllers;

public class CharacterHoverPanel
{
    public Character CharacterParent;
    public Texture2D CharacterTexture;
    public SpriteFont Font;
    public Vector2 Position, Size;

    public CharacterHoverPanel(Character inputParent)
    {
        CharacterParent = inputParent;
        Size = new(100, 100);
    }

     public virtual void LoadContent()
    {
        CharacterTexture = CharacterParent.Texture;
        Font = CharacterParent.StatsFont;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Temp texture to draw the outline of the panel
        Texture2D texture;
        texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture.SetData(new Color[] {Color.Black});

        // Temp texture to draw inner colour of the panel
        Texture2D texture2;
        texture2 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture2.SetData(new Color[] { new Color(200, 200, 200) });

        // Drawing outline then the inside
        spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);
        spriteBatch.Draw(texture2, new Rectangle((int)Position.X + 5, (int)Position.Y + 5, (int)Size.X - 10, (int)Size.Y - 10), Color.White);
    }
}