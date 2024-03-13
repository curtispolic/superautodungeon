namespace superautodungeon.Objects.Heroes;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using superautodungeon.Objects.Controllers;

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
        AnimationTimer = 0;
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
            // Inactive heroes are just for placeholder shadows
            DrawShadowOnly(spriteBatch, gameTime, Position);
        }
        else if (PickedUp)
        {        
            // If picked up we want to draw on the mouse and shadow in position
            var mouseState = Mouse.GetState();
            Vector2 mousePos = mouseState.Position.ToVector2();
            DrawShadowOnly(spriteBatch, gameTime, Position);
            MouseDraw(spriteBatch, gameTime, mousePos + GameParent.shop.PickupOffset);
        }
        else
        {
            // Default case can just use the base character draw method
            base.Draw(spriteBatch, gameTime);
        }
    }

    public override void CombatDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        if (MeleeHitting)
        {
            // Draw just the character swinging forward for the hit
            if (AnimationTimer <= 250)
            {
                spriteBatch.Draw(Texture, position + new Vector2((float)(AnimationTimer/3), 0), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }
            else if (AnimationTimer > 250 && AnimationTimer < 500)
            {
                spriteBatch.Draw(Texture, position + new Vector2(83, 0) - new Vector2((float)((AnimationTimer-250)/3), 0), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }
            else
            {
                MeleeHitting = false;
            }

            AnimationTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        // Do all the general combat drawing
        base.CombatDraw(spriteBatch, gameTime, position);
    }

    public virtual void MouseDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        // For drawing under the mouse when picked up
        // Drawing Self
        spriteBatch.Draw(this.Texture, position, null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // Drawing HP icon
        spriteBatch.Draw(this.HPTexture, position + new Vector2(24, 128), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // Drawing attack
        spriteBatch.Draw(this.AttackTexture, position + new Vector2(72, 128), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // Drawing HP text
        // Draws 4 offset versions for the black outline, then a white version on top
        spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), position + new Vector2(31,131), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), position + new Vector2(29,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), position + new Vector2(31,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), position + new Vector2(29,131), Color.Black);

        spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), position + new Vector2(30,130), Color.White);

        // Attack text
        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), position + new Vector2(83,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), position + new Vector2(85,131), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), position + new Vector2(85,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), position + new Vector2(83,131), Color.Black);

        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), position + new Vector2(84,130), Color.White);
    }

    public virtual void DrawShadowOnly(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        spriteBatch.Draw(ShadowTexture, position + new Vector2(16, 96), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
    }
}