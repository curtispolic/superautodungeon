using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using superautodungeon.Objects.Controllers;
using superautodungeon.Objects.Events;

namespace superautodungeon.Objects.Rooms;

public class EventRoom : Room
{
    public Event RoomEvent;
    public Texture2D Texture;

    public EventRoom(Level inputParent, int inputX, int inputY) : base(inputParent, inputX, inputY)
    {
        RoomEvent = new(LevelParent.GameParent);
        LoadContent();
    }

    public override void Enter()
    {
        base.Enter();
        // Handle event active here
    }

    public void LoadContent()
    {
        Texture = LevelParent.GameParent.Content.Load<Texture2D>("UI/event");
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        base.Draw(spriteBatch, gameTime);
        if (!Completed)
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(100f/Texture.Width, 100f/Texture.Height), SpriteEffects.None, 0f); 
        DrawCharacter(spriteBatch, gameTime);
    }
}