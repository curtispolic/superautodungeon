namespace superautodungeon.Objects.UI;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using superautodungeon.Objects.Heroes;
using superautodungeon.Objects.Enemies;

public class MainMenu
{
    public Button NewGameButton, LoadGameButton, SettingsButton, ExitButton;
    public Texture2D TitleTexture, BackgroundTexture;
    public MainGame GameParent;
    public bool leftMouseDown;
    public double leftMouseDownTime;

    public MainMenu(MainGame inputParent)
    {
        GameParent = inputParent;
        NewGameButton = new Button(GameParent, "New Game", new Vector2(50, 250));
        LoadGameButton = new Button(GameParent, "Load Game", new Vector2(50, 300));
        SettingsButton = new Button(GameParent, "Settings", new Vector2(50, 350));
        ExitButton = new Button(GameParent, "Exit", new Vector2(50, 400));
        leftMouseDown = false;
        LoadContent();
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
        spriteBatch.Draw(TitleTexture, new Vector2(50,50), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // The buttons
        NewGameButton.Draw(spriteBatch, gameTime);
        LoadGameButton.Draw(spriteBatch, gameTime);
        SettingsButton.Draw(spriteBatch, gameTime);
        ExitButton.Draw(spriteBatch, gameTime);
    }

    public void OnClickExit()
    {
        GameParent.Exit();
    }

    public void OnClickSettings()
    {
        Console.WriteLine("Settings button clicked - TODO: Add functionality");
    }

    public void OnClickLoadGame()
    {
        Console.WriteLine("Load Game button clicked - TODO: Add functionality");
    }

    public void OnClickNewGame()
    {
        Party testParty;
        Mob testMob;

        // Construct the party and mob
        testParty = new(new Vector2(100, 100));
        testMob = new(new Vector2(300, 100));

        // Populate the party
        for (int i = 0; i < 2; i++)
        {
            Knight newHero = new(GameParent);
            testParty.Add(newHero);
        }

        for (int i = 0; i < 2; i++)
        {
            Wizard newHero = new(GameParent);
            testParty.Add(newHero);
        }

        // Populate the mob
        for (int i = 0; i < 4; i++)
        {
            Enemy newEnemy = new(GameParent);
            testMob.Add(newEnemy);
        }

        GameParent.playerParty = testParty;

        GameParent.combat = new(GameParent, testParty, testMob);
        GameParent.CombatVisible = true;

        GameParent.gameplayUI = new(GameParent, GameParent.playerParty);
        GameParent.GameplayUIVisible = true;

        GameParent.MainMenuVisible = false;
    }
}