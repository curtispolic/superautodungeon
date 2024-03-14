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
    public Level level;
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
        level = new();

        playerParty = new(this);

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

        // Always update UI if it's needed
        if (gameplayUI.Active)
            gameplayUI.Update(graphics, gameTime);

        // Only update 1 thing that takes up the screen
        if (mainMenu.Active)
        {
            mainMenu.Update(graphics, gameTime);
        }
        else if (combat.Active)
        {
            if (fightTimer > combat.AnimationTime)
            {
                fightTimer = 0;
                combat.AnimationTime = combat.Update(true);
            }
        }
        else if (shop.Active)
        {
            shop.Update(graphics, gameTime);
        }
        else if (level.Active)
        {
            level.Update(graphics, gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Clear the screen and begin rendering
        GraphicsDevice.Clear(Color.LightPink);
        _spriteBatch.Begin();

        if (mainMenu.Active)
            mainMenu.Draw(_spriteBatch, gameTime);

        if (gameplayUI.Active)
            gameplayUI.Draw(_spriteBatch, gameTime);

        if (shop.Active)
            shop.Draw(_spriteBatch, gameTime);

        if (level.Active)
            level.Draw(_spriteBatch, gameTime);

        if (combat.Active)
            combat.Draw(_spriteBatch, gameTime);

        // Handle mouseovers last so they are on top
        if (gameplayUI.Active)
            gameplayUI.MouseoverDraw(_spriteBatch, gameTime);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
