namespace superautodungeon.Objects.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Button
{
    public string Text;
    public bool MouseOver, leftMouseDown;
    public MainGame GameParent;
    public Vector2 Position, Size, TextSize;
    public SpriteFont Font;

    public Button(MainGame inputParent, string inputText, Vector2 inputPosition)
    {
        // Constructor with no size will make the button slightly larger than the text
        GameParent = inputParent;
        Position = inputPosition;
        Text = inputText;
        LoadContent();
        TextSize = Font.MeasureString(Text);
        Size = TextSize + new Vector2(20, 20);
        MouseOver = false;
        // set this to true at first to prevent held down buttoning
        leftMouseDown = true;
    }

    public Button(MainGame inputParent, string inputText, Vector2 inputPosition, Vector2 inputSize)
    {
        // Constructor with size will stricly match that size
        GameParent = inputParent;
        Position = inputPosition;
        Text = inputText;
        Size = inputSize;
        LoadContent();
        TextSize = Font.MeasureString(Text);
        MouseOver = false;
        leftMouseDown = true;
    }

    public bool Update(MouseState mouseState, GraphicsDeviceManager graphics, GameTime gameTime)
    {
        // Update function will return true when clicked on

        // Handle instances where the mouse is inside the game window
        if (0 <= mouseState.X && mouseState.X <= graphics.PreferredBackBufferWidth && 0 <= mouseState.Y && mouseState.Y <= graphics.PreferredBackBufferHeight)
        {
            // Left mouse down handling
            if (mouseState.LeftButton == ButtonState.Pressed && !leftMouseDown)
            {
                // To ensure only one click goes through
                leftMouseDown = true;

                // Clicking inside self
                if (mouseState.X > Position.X && mouseState.X < Position.X + Size.X &&
                mouseState.Y > Position.Y && mouseState.Y < Position.Y + Size.Y)
                { 
                    return true;
                }
            }
            // Handling release of mouse button when it was previously held down
            else if (mouseState.LeftButton == ButtonState.Released &&  leftMouseDown)
            {
                leftMouseDown = false;
            }
            // Handling no left click without prior held down state
            else if (mouseState.LeftButton == ButtonState.Released)
            {
                // Hovering inside self
                if (mouseState.X > Position.X && mouseState.X < Position.X + Size.X &&
                mouseState.Y > Position.Y && mouseState.Y < Position.Y + Size.Y)
                {
                    // Mouseover effect, return asap to not hit the false at the end
                    MouseOver = true;
                    return false;
                }
            }
        }

        // Mouseover true is handled, so default to false
        MouseOver = false;
        return false;
    }

    public void LoadContent()
    {
        // Loads the generic font
        Font = GameParent.Content.Load<SpriteFont>("statsFont");
    }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Temp texture to draw the outline of the button
        Texture2D texture;
        texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture.SetData(new Color[] {Color.Black});

        // Temp texture to draw inner colour of the button
        Texture2D texture2;
        texture2 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture2.SetData(new Color[] { new Color(150, 150, 150) });

        // Temp texture to draw the inner colour for mouseover
        Texture2D texture3;
        texture3 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture3.SetData(new Color[] { new Color(200, 200, 200) });

        // Drawing outline then the inside
        spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);
        if (MouseOver)
            spriteBatch.Draw(texture3, new Rectangle((int)Position.X + 5, (int)Position.Y + 5, (int)Size.X - 10, (int)Size.Y - 10), Color.White);
        else
            spriteBatch.Draw(texture2, new Rectangle((int)Position.X + 5, (int)Position.Y + 5, (int)Size.X - 10, (int)Size.Y - 10), Color.White);

        // Drawing the text
        Vector2 textOffset = (Size - TextSize) / 2;
        spriteBatch.DrawString(Font, Text, Position + textOffset, Color.Black);
    }
}