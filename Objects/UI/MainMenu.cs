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
    public bool leftMouseDown, Active;
    public double leftMouseDownTime;

    public MainMenu()
    {
        Active = false;
    }

    public MainMenu(MainGame inputParent)
    {
        GameParent = inputParent;
        NewGameButton = new Button(GameParent, "New Game", new Vector2(50, 250));
        LoadGameButton = new Button(GameParent, "Load Game", new Vector2(50, 300));
        SettingsButton = new Button(GameParent, "Settings", new Vector2(50, 350));
        ExitButton = new Button(GameParent, "Exit", new Vector2(50, 400));
        leftMouseDown = false;
        LoadContent();
        Active = true;
    }

    public void Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        if (NewGameButton.Update(mouseState, graphics, gameTime))
            OnClickNewGame();

        if (LoadGameButton.Update(mouseState, graphics, gameTime))
            OnClickLoadGame();

        if (SettingsButton.Update(mouseState, graphics, gameTime))
            OnClickSettings();

        if (ExitButton.Update(mouseState, graphics, gameTime))
            OnClickExit();
    }

    public void LoadContent()
    {
        TitleTexture = GameParent.Content.Load<Texture2D>("UI/title");
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
        GameParent.gameplayUI = new(GameParent, GameParent.playerParty);
        GameParent.level = new(GameParent);

        Active = false;
    }
}