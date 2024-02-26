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
        testHero.position = new Vector2(100, 100);

        testHero.Attack = 2;
        testHero.HP = 20;
        testEnemy.Attack = 1;
        testEnemy.HP = 10;
        Console.WriteLine($"HHP: {testHero.HP}  EHP: {testEnemy.HP}");

        Combat testCombat = new(testHero, testEnemy);

        while (testHero.HP > 0 && testEnemy.HP > 0)
        {
            testCombat.FightOneStep();
            Console.WriteLine($"HHP: {testHero.HP}  EHP: {testEnemy.HP}");
        }

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        testHero.texture = Content.Load<Texture2D>("ball");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        testHero.Draw(_spriteBatch);

        // TODO: Add your drawing code here

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
