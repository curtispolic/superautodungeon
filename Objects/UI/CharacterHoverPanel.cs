namespace superautodungeon.Objects.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using superautodungeon.Objects.Controllers;

public class CharacterHoverPanel
{
    public Character CharacterParent;
    public Texture2D CharacterTexture;
    public SpriteFont Font;
    public Vector2 Position, Size, TextSize;
    public string Text;

    public CharacterHoverPanel()
    {

    }

    public CharacterHoverPanel(Character inputParent,GraphicsDeviceManager graphics, Vector2 inputPosition)
    {
        CharacterParent = inputParent;
        LoadContent();

        // Get details from parent and size for it
        Text = CharacterParent.Name + "\n"
            + "Level " + CharacterParent.Level.ToString() + " " + CharacterParent.Class + "\n"
            + CharacterParent.Description;
        TextSize = Font.MeasureString(Text);
        Size = TextSize + new Vector2(20, 20);

        if (inputPosition.X > graphics.PreferredBackBufferWidth / 2)
        {
            Position = inputPosition;
            Position.X -= Size.X;
        }
        else
        {
            Position = inputPosition;
        }
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

        // Text
        Vector2 textOffset = (Size - TextSize) / 2;
        spriteBatch.DrawString(Font, Text, Position + textOffset, Color.Black);
    }
}