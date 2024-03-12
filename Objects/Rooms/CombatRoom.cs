using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using superautodungeon.Objects.Controllers;

namespace superautodungeon.Objects.Rooms;

public class CombatRoom : Room
{
    public Mob RoomMob;
    public Texture2D Texture;

    public CombatRoom(Level inputParent, int inputX, int inputY) : base(inputParent, inputX, inputY)
    {
        RoomMob = new(LevelParent.GameParent);
        LoadContent();
    }

    public override void Enter()
    {
        base.Enter();
        if (!Completed)
            LevelParent.GameParent.combat = new(LevelParent.GameParent, this, LevelParent.GameParent.playerParty, RoomMob);
    }

    public void LoadContent()
    {
        Texture = LevelParent.GameParent.Content.Load<Texture2D>("death");
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        base.Draw(spriteBatch, gameTime);
        if (!Completed)
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(0, 0), new Vector2(100f/Texture.Width, 100f/Texture.Height), SpriteEffects.None, 0f); 
        DrawCharacter(spriteBatch, gameTime);
    }
}