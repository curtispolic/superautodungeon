using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using superautodungeon.Objects.Heroes;
using superautodungeon.Objects.Enemies;
using superautodungeon.Objects.Controllers;

using System;

namespace superautodungeon;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Hero testHero;
    private Enemy testEnemy;
    private Combat testCombat;
    private double testTimer;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        testHero = new();
        testEnemy = new();
        testHero.Position = new Vector2(100, 100);
        testEnemy.Position = new Vector2(300, 100);

        testHero.Attack = 2;
        testHero.HP = 20;
        testEnemy.Attack = 1;
        testEnemy.HP = 10;
        Console.WriteLine($"HHP: {testHero.HP}  EHP: {testEnemy.HP}");

        testCombat = new(testHero, testEnemy);

        testTimer = 0;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        testHero.Texture = Content.Load<Texture2D>("knight");
        testHero.HPTexture = Content.Load<Texture2D>("heart");
        testHero.AttackTexture = Content.Load<Texture2D>("attack");
        testHero.StatsFont = Content.Load<SpriteFont>("statsFont");
        testHero.ShadowTexture = Content.Load<Texture2D>("shadow50");

        testEnemy.Texture = Content.Load<Texture2D>("skeleton");
        testEnemy.HPTexture = Content.Load<Texture2D>("heart");
        testEnemy.AttackTexture = Content.Load<Texture2D>("attack");
        testEnemy.StatsFont = Content.Load<SpriteFont>("statsFont");
        testEnemy.ShadowTexture = Content.Load<Texture2D>("shadow50");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        testTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (testHero.HP > 0 && testEnemy.HP > 0 && testTimer > 2000)
        {
            testCombat.FightOneStep();
            Console.WriteLine($"HHP: {testHero.HP}  EHP: {testEnemy.HP}");
            testTimer = 0;
        }

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        testHero.Draw(_spriteBatch);
        testEnemy.Draw(_spriteBatch);

        // TODO: Add your drawing code here

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
