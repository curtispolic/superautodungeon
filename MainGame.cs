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

namespace superautodungeon;

public class MainGame : Game
{
    private MainMenu mainMenu;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Party testParty;
    private Mob testMob;
    private Combat testCombat;
    private Button testButton;
    private double fightTimer;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Random random = new();

        mainMenu = new(this, _graphics);

        /*
        // Construct the party and mob
        testParty = new(new Vector2(100, 100));
        testMob = new(new Vector2(300, 100));

        // Populate the party
        for (int i = 0; i < 4; i++)
        {
            Hero newHero = new()
            {
                Attack = random.Next(2, 5),
                HP = random.Next(10, 20)
            };
            testParty.Add(newHero);
        }

        // Populate the mob
        for (int i = 0; i < 4; i++)
        {
            Enemy newEnemy = new()
            {
                Attack = random.Next(1, 4),
                HP = random.Next(5, 15)
            };
            testMob.Add(newEnemy);
        }

        testCombat = new(testParty, testMob);

        fightTimer = 0;
        */

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        mainMenu.LoadContent();

        /*
        foreach (var hero in testParty.HeroList)
        {
            hero.Texture = Content.Load<Texture2D>("knight");
            hero.HPTexture = Content.Load<Texture2D>("heart");
            hero.AttackTexture = Content.Load<Texture2D>("attack");
            hero.StatsFont = Content.Load<SpriteFont>("statsFont");
            hero.ShadowTexture = Content.Load<Texture2D>("shadow50");
            hero.DeathTexture = Content.Load<Texture2D>("death");
        }

        foreach (var enemy in testMob.EnemyList)
        {
            enemy.Texture = Content.Load<Texture2D>("skeleton");
            enemy.HPTexture = Content.Load<Texture2D>("heart");
            enemy.AttackTexture = Content.Load<Texture2D>("attack");
            enemy.StatsFont = Content.Load<SpriteFont>("statsFont");
            enemy.ShadowTexture = Content.Load<Texture2D>("shadow50");
            enemy.DeathTexture = Content.Load<Texture2D>("death");
        }
        */
    }

    protected override void Update(GameTime gameTime)
    {

        // Handle instances where the mouse is inside the game window
        /*
        if (0 <= MouseState.X && MouseState.X <= _graphics.PreferredBackBufferWidth && 0 <= MouseState.Y && MouseState.Y <= _graphics.PreferredBackBufferHeight)
        {
            // Left mouse down handling
            if (MouseState.LeftButton == ButtonState.Pressed && !leftMouseDown)
            {
                // Stored to determine dragging vs. clicking
                leftMouseDownTime = gameTime.TotalGameTime.TotalMilliseconds;
                leftMouseDown = true;

                foreach (var button in buttonList)
                {
                    // Clicking inside the bounds of a button
                    if (MouseState.X > button.Position.X && MouseState.X < button.Position.X + button.Texture.Width &&
                    MouseState.Y > button.Position.Y && MouseState.Y < button.Position.Y + button.Texture.Height)
                    {
                        // Handle button click
                        

                    }
                }
            }
            else if (MouseState.LeftButton == ButtonState.Released &&  leftMouseDown)
            {
                leftMouseDown = false;
            }
        }
        */

        /*
        fightTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (testCombat.Ongoing && fightTimer > 2000)
        {
            testCombat.BeginRound();
            testCombat.MeleeHit();
            testCombat.OnAttackTriggers();
            testCombat.HandleDeath();
            testCombat.EndRound();
            fightTimer = 0;
        }
        */

        mainMenu.Update(_graphics, gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        mainMenu.Draw(_spriteBatch, gameTime);

        /*
        testParty.Draw(_spriteBatch, gameTime);
        testMob.Draw(_spriteBatch, gameTime);

        foreach (var button in buttonList)
        {
            button.Draw(_spriteBatch, gameTime);
        }
        */

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
