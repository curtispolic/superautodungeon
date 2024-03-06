using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using superautodungeon.Objects;
using superautodungeon.Objects.Controllers;
using superautodungeon.Objects.UI;

namespace superautodungeon;

public class MainGame : Game
{
    public int ShopTier;
    public MainMenu mainMenu;
    public Combat combat;
    public Shop shop;
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

        // Set the main menu
        mainMenu = new(this);

        /*
        Create default shells of each other game element
        These shells simply set .Active to false so they will not attempt to render
        When created for real, .Active will be true, so they will render and update
        */
        shop = new();
        gameplayUI = new();
        combat = new();

        playerParty = new(new Vector2(0,0));

        fightTimer = 0;

        // Create the name generator
        nameGenerator = new();

        // Set the shop tier
        ShopTier = 1;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        fightTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (combat.Active)
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

        if (mainMenu.Active)
            mainMenu.Update(graphics, gameTime);

        if (shop.Active)
            shop.Update(graphics, gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clear the screen and begin rendering
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        if (mainMenu.Active)
            mainMenu.Draw(_spriteBatch, gameTime);
            
        if (combat.Active)
            combat.Draw(_spriteBatch, gameTime);

        if (gameplayUI.Active)
            gameplayUI.Draw(_spriteBatch, gameTime);

        if (shop.Active)
            shop.Draw(_spriteBatch, gameTime);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
