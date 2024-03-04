namespace superautodungeon.Objects.UI;

using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class MainMenu
{
    public Button NewGameButton, LoadGameButton, SettingsButton, ExitButton;
    public Texture2D TitleTexture, BackgroundTexture;
    public MainGame GameParent;
    public bool leftMouseDown;
    public double leftMouseDownTime;

    public MainMenu(MainGame inputParent, GraphicsDeviceManager graphics)
    {
        GameParent = inputParent;
        int buttonLeft = graphics.PreferredBackBufferWidth / 2 - 64;
        NewGameButton = new Button(GameParent, "New Game", new Vector2(buttonLeft, 250));
        LoadGameButton = new Button(GameParent, "Load Game", new Vector2(buttonLeft, 300));
        SettingsButton = new Button(GameParent, "Settings", new Vector2(buttonLeft, 350));
        ExitButton = new Button(GameParent, "Exit", new Vector2(buttonLeft, 400));
        leftMouseDown = false;
    }

    public void Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var MouseState = Mouse.GetState();

        // Handle instances where the mouse is inside the game window
        if (0 <= MouseState.X && MouseState.X <= graphics.PreferredBackBufferWidth && 0 <= MouseState.Y && MouseState.Y <= graphics.PreferredBackBufferHeight)
        {
            // Left mouse down handling
            if (MouseState.LeftButton == ButtonState.Pressed && !leftMouseDown)
            {
                // Stored to determine dragging vs. clicking
                leftMouseDownTime = gameTime.TotalGameTime.TotalMilliseconds;
                leftMouseDown = true;

                // Clicking inside NewGameButton
                if (MouseState.X > NewGameButton.Position.X && MouseState.X < NewGameButton.Position.X + NewGameButton.Texture.Width &&
                MouseState.Y > NewGameButton.Position.Y && MouseState.Y < NewGameButton.Position.Y + NewGameButton.Texture.Height)
                {                    
                    OnClickNewGame();
                }

                // Clicking inside LoadGameButton
                if (MouseState.X > LoadGameButton.Position.X && MouseState.X < LoadGameButton.Position.X + LoadGameButton.Texture.Width &&
                MouseState.Y > LoadGameButton.Position.Y && MouseState.Y < LoadGameButton.Position.Y + LoadGameButton.Texture.Height)
                {                    
                    OnClickLoadGame();
                }

                // Clicking inside NewGameButton
                if (MouseState.X > SettingsButton.Position.X && MouseState.X < SettingsButton.Position.X + SettingsButton.Texture.Width &&
                MouseState.Y > SettingsButton.Position.Y && MouseState.Y < SettingsButton.Position.Y + SettingsButton.Texture.Height)
                {                    
                    OnClickSettings();
                }

                // Clicking inside NewGameButton
                if (MouseState.X > ExitButton.Position.X && MouseState.X < ExitButton.Position.X + ExitButton.Texture.Width &&
                MouseState.Y > ExitButton.Position.Y && MouseState.Y < ExitButton.Position.Y + ExitButton.Texture.Height)
                {                    
                    OnClickExit();
                }

            }
            else if (MouseState.LeftButton == ButtonState.Released &&  leftMouseDown)
            {
                leftMouseDown = false;
            }
        }
    }

    public void LoadContent()
    {
        TitleTexture = GameParent.Content.Load<Texture2D>("title");
        NewGameButton.LoadContent();
        LoadGameButton.LoadContent();
        SettingsButton.LoadContent();
        ExitButton.LoadContent();
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Drawing the title
        spriteBatch.Draw(this.TitleTexture, new Vector2(150,0), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // The buttons
        NewGameButton.Draw(spriteBatch, gameTime);
        LoadGameButton.Draw(spriteBatch, gameTime);
        SettingsButton.Draw(spriteBatch, gameTime);
        ExitButton.Draw(spriteBatch, gameTime);
    }

    public void OnClickExit()
    {

    }

    public void OnClickSettings()
    {

    }

    public void OnClickLoadGame()
    {

    }

    public void OnClickNewGame()
    {
    
    }
}