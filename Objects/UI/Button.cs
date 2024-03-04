namespace superautodungeon.Objects.UI;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Button
{
    public string Text;
    public MainGame GameParent;
    public Texture2D Texture;
    public Vector2 Position;
    public SpriteFont Font;

    public Button(MainGame inputParent, string inputText, Vector2 inputPosition)
    {
        GameParent = inputParent;
        Position = inputPosition;
        Text = inputText;
    }

    public void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("button128x32");
        Font = GameParent.Content.Load<SpriteFont>("statsFont");
    }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Drawing Self
        spriteBatch.Draw(this.Texture, this.Position, null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // Drawing the text
        spriteBatch.DrawString(this.Font, this.Text, this.Position + new Vector2(10,4), Color.Black);
    }
}