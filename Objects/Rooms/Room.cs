using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using superautodungeon.Objects.Controllers;

namespace superautodungeon.Objects.Rooms;

public class Room
{
    public Level LevelParent;
    public Vector2 Position;
    public int X, Y;
    public bool MouseOver, Active, leftMouseDown, ContainsPlayer, Completed;

    public Room()
    {
        Active = false;
    }

    public Room(Level inputParent, int inputX, int inputY)
    {
        ContainsPlayer = false;
        MouseOver = false;
        Active = true;
        leftMouseDown = false;
        LevelParent = inputParent;
        X = inputX;
        Y = inputY;
        Completed = false;
        Position = new Vector2(250 + X*120, 20 + Y*120);
    }

    public virtual void Enter()
    {
        ContainsPlayer = true;
    }

    public virtual bool Update(MouseState mouseState, GraphicsDeviceManager graphics, GameTime gameTime)
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
                if (mouseState.X > Position.X && mouseState.X < Position.X + 100 &&
                mouseState.Y > Position.Y && mouseState.Y < Position.Y + 100)
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
                if (mouseState.X > Position.X && mouseState.X < Position.X + 100 &&
                mouseState.Y > Position.Y && mouseState.Y < Position.Y + 100)
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

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Temp texture to draw the outline
        Texture2D texture;
        texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture.SetData(new Color[] {Color.Black});

        // Temp texture to draw inner colour
        Texture2D texture2;
        texture2 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture2.SetData(new Color[] { new Color(150, 150, 150) });

        // Temp texture to draw the inner colour for mouseover
        Texture2D texture3;
        texture3 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        texture3.SetData(new Color[] { new Color(200, 200, 200) });

        // Drawing outline then the inside
        spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, 100, 100), Color.White);
        if (MouseOver)
            spriteBatch.Draw(texture3, new Rectangle((int)Position.X + 5, (int)Position.Y + 5, 90, 90), Color.White);
        else
            spriteBatch.Draw(texture2, new Rectangle((int)Position.X + 5, (int)Position.Y + 5, 90, 90), Color.White);

        DrawCharacter(spriteBatch, gameTime);
    }

    public void DrawCharacter(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (ContainsPlayer)
        {
            if (LevelParent.GameParent.playerParty.FrontHero() is not null)
            {
            Texture2D playerTexture = LevelParent.GameParent.playerParty.FrontHero().Texture;
            spriteBatch.Draw(playerTexture, Position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(100f/playerTexture.Width, 100f/playerTexture.Height), SpriteEffects.None, 0f);
            }
        }
    }
}