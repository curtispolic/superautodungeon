namespace superautodungeon.Objects.Heroes;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Hero : Character
{
    public string Class,  Gender;
    public bool Buyable, PickedUp;
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Hero(MainGame inputParent, bool inputActive): base(inputParent)
    {
        // Active should only be false for manual creations
        Active = inputActive;
        Buyable = Active;
        PickedUp = false;
        LoadContent();
    }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override bool Update(MouseState mouseState, GraphicsDeviceManager graphics, GameTime gameTime)
    {
        // Handle actives
        if (Active)
        {
            // Handle instances where the mouse is inside the game window
            if (0 <= mouseState.X && mouseState.X <= graphics.PreferredBackBufferWidth && 0 <= mouseState.Y && mouseState.Y <= graphics.PreferredBackBufferHeight)
            {
                // Left mouse down handling, if clicked while buyable and not picked up
                if (mouseState.LeftButton == ButtonState.Pressed && !PickedUp && Buyable &&
                mouseState.X > Position.X && mouseState.X < Position.X + Texture.Width &&
                mouseState.Y > Position.Y && mouseState.Y < Position.Y + Texture.Height)
                {
                    PickedUp = true;
                    return PickedUp;
                }
                // NOT ACTIVE
                else if (mouseState.LeftButton == ButtonState.Pressed && !PickedUp && Buyable && !Active &&
                mouseState.X > Position.X && mouseState.X < Position.X + Texture.Width &&
                mouseState.Y > Position.Y && mouseState.Y < Position.Y + Texture.Height)
                {
                    return true;
                }
            }

            // This will handle mouseover
            base.Update(mouseState, graphics, gameTime);

            return false;
        }
        // Non active
        else
        {
            // Handle instances where the mouse is inside the game window
            if (0 <= mouseState.X && mouseState.X <= graphics.PreferredBackBufferWidth && 0 <= mouseState.Y && mouseState.Y <= graphics.PreferredBackBufferHeight)
            {
                // Left mouse down handling, if clicked on the shadow
                // Shadow offset is (16, 96) so
                Vector2 tempVector = Position + new Vector2(16, 96);
                if (mouseState.LeftButton == ButtonState.Pressed &&
                mouseState.X > tempVector.X && mouseState.X < tempVector.X + ShadowTexture.Width &&
                mouseState.Y > tempVector.Y && mouseState.Y < tempVector.Y + ShadowTexture.Height)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (!Active)
        {
            DrawShadowOnly(spriteBatch, gameTime, Position);
        }
        else if (!PickedUp)
        {        
            base.Draw(spriteBatch, gameTime);
        }
        else
        {
            var mouseState = Mouse.GetState();
            Vector2 mousePos = mouseState.Position.ToVector2();
            base.Draw(spriteBatch, gameTime, mousePos, false);
            DrawShadowOnly(spriteBatch, gameTime, Position);
        }
    }

    public virtual void DrawShadowOnly(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        spriteBatch.Draw(ShadowTexture, position + new Vector2(16, 96), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
    }
}