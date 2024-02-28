using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using superautodungeon.Objects;
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
    private Party testParty;
    private Enemy testEnemy;
    private Mob testMob;
    private Combat testCombat;
    private double fightTimer;

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        testHero = new();
        testEnemy = new();
        testHero.Position = new Vector2(100, 100);
        testEnemy.Position = new Vector2(300, 100);

        testHero.Attack = 2;
        testHero.HP = 20;
        testEnemy.Attack = 1;
        testEnemy.HP = 10;
        Console.WriteLine($"HHP: {testHero.HP}  EHP: {testEnemy.HP}");

        testParty = new(new Vector2(100, 100));
        testParty.Add(testHero);

        testMob = new(new Vector2(300, 100));
        testMob.Add(testEnemy);

        testCombat = new(testParty, testMob);

        fightTimer = 0;

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
        testHero.DeathTexture = Content.Load<Texture2D>("death");

        testEnemy.Texture = Content.Load<Texture2D>("skeleton");
        testEnemy.HPTexture = Content.Load<Texture2D>("heart");
        testEnemy.AttackTexture = Content.Load<Texture2D>("attack");
        testEnemy.StatsFont = Content.Load<SpriteFont>("statsFont");
        testEnemy.ShadowTexture = Content.Load<Texture2D>("shadow50");
        testEnemy.DeathTexture = Content.Load<Texture2D>("death");

    }

    protected override void Update(GameTime gameTime)
    {
        fightTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (testCombat.Ongoing && fightTimer > 2000)
        {
            testCombat.MeleeHit();
            testCombat.OnAttackTriggers();
            testCombat.HandleDeath();
            testCombat.RoundFinish();
            fightTimer = 0;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        testParty.Draw(_spriteBatch, gameTime);
        testMob.Draw(_spriteBatch, gameTime);

        // TODO: Add your drawing code here

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
