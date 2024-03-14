using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using superautodungeon.Objects.Controllers;

namespace superautodungeon.Objects.Rooms;

public class ShopRoom : Room
{
    public Shop RoomShop;
    public Texture2D Texture;

    public ShopRoom(Level inputParent, int inputX, int inputY) : base(inputParent, inputX, inputY)
    {
        RoomShop = new(LevelParent, LevelParent.GameParent.ShopTier);
        LoadContent();
    }

    public override void Enter()
    {
        base.Enter();
        RoomShop.Active = true;
        LevelParent.Active = false;
        LevelParent.GameParent.shop = RoomShop;
    }

    public void LoadContent()
    {
        Texture = LevelParent.GameParent.Content.Load<Texture2D>("UI/shop");
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        base.Draw(spriteBatch, gameTime);
        if (!Completed)
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(100f/Texture.Width, 100f/Texture.Height), SpriteEffects.None, 0f);
        DrawCharacter(spriteBatch, gameTime);
    }
}