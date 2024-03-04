using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using superautodungeon.Objects;
using superautodungeon.Objects.Heroes;
using superautodungeon.Objects.Enemies;
using superautodungeon.Objects.Controllers;
using superautodungeon.Objects.UI;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace superautodungeon;

public class MainGame : Game
{
    public bool MainMenuVisible, CombatVisible, GameplayUIVisible;
    public MainMenu mainMenu;
    public Combat combat;
    public GameplayUI gameplayUI;
    public Party playerParty;
    public NameGenerator nameGenerator;
    public GraphicsDeviceManager graphics;
    private SpriteBatch _spriteBatch;
    private double fightTimer;

    public MainGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Setup the base graphics settings
        graphics.IsFullScreen = false;
        graphics.PreferredBackBufferWidth = 1600;
        graphics.PreferredBackBufferHeight = 900;
        graphics.ApplyChanges();

        // Set the main menu and make it visible
        mainMenu = new(this);
        MainMenuVisible = true;

        CombatVisible = false;
        GameplayUIVisible = false;

        fightTimer = 0;

        // Create the name generator
        nameGenerator = new();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
    }

    protected override void Update(GameTime gameTime)
    {
        fightTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (CombatVisible)
        {
            if (combat.Ongoing && fightTimer > 2000)
            {
                combat.BeginRound();
                combat.MeleeHit();
                combat.OnAttackTriggers();
                combat.HandleDeath();
                combat.EndRound();
                fightTimer = 0;
            }
        }

        if (MainMenuVisible)
            mainMenu.Update(graphics, gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clear the screen and begin rendering
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        if (MainMenuVisible)
            mainMenu.Draw(_spriteBatch, gameTime);
            
        if (CombatVisible)
            combat.Draw(_spriteBatch, gameTime);

        if (GameplayUIVisible)
            gameplayUI.Draw(_spriteBatch, gameTime);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
