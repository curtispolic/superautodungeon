namespace superautodungeon.Objects;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Character
{
    public MainGame GameParent;
    public Vector2 Position;
    public Texture2D Texture, HPTexture, AttackTexture, ShadowTexture, DeathTexture;
    public SpriteFont StatsFont;
    public bool Dead, Dying, MouseOver, Active;
    public int MaxHP, CurrentHP, Attack;
    public double DeathTimer;
    public string Name, Description;
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Character()
    {

    }

    public Character(MainGame inputParent)
    {
        GameParent = inputParent;
        Dead = false;
        Dying = false;
        LoadContent();
    }

    public virtual bool Update(MouseState mouseState, GraphicsDeviceManager graphics, GameTime gameTime)
    {
        // This is just for handling mouseover status, say for a mouseover panel to show character information
        // Only handle active characters
        if (Active)
        {   
            // Handle instances where the mouse is inside the game window
            if (0 <= mouseState.X && mouseState.X <= graphics.PreferredBackBufferWidth && 0 <= mouseState.Y && mouseState.Y <= graphics.PreferredBackBufferHeight)
            {
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    // Hovering inside self
                    if (mouseState.X > Position.X && mouseState.X < Position.X + Texture.Width &&
                    mouseState.Y > Position.Y && mouseState.Y < Position.Y + Texture.Height)
                    {
                        // Mouseover effect, return asap to not hit the false at the end
                        MouseOver = true;
                        return false;
                    }
                }
            }
        }

        // Mouseover true is handled, so default to false
        MouseOver = false;
        return false;
    }

    public bool Die()
    {
        // Returns true if this is the call that killed the character
        if (!Dead && !Dying)
        {
            Dead = true;
            Dying = true;
            DeathTimer = 0;
            return true;
        }
        return false;
    }

    public virtual void LoadContent()
    {
        HPTexture = GameParent.Content.Load<Texture2D>("heart");
        AttackTexture = GameParent.Content.Load<Texture2D>("attack");
        StatsFont = GameParent.Content.Load<SpriteFont>("statsFont");
        ShadowTexture = GameParent.Content.Load<Texture2D>("shadow50");
        DeathTexture = GameParent.Content.Load<Texture2D>("death");
    }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        if (!Dead || Dying)
        {
            // Drawing shadow
            spriteBatch.Draw(this.ShadowTexture, this.Position + new Vector2(16, 96), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

            // Drawing Self
            spriteBatch.Draw(this.Texture, this.Position, null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

            // Drawing HP icon
            spriteBatch.Draw(this.HPTexture, this.Position + new Vector2(24, 128), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

            // Drawing attack
            spriteBatch.Draw(this.AttackTexture, this.Position + new Vector2(72, 128), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

            // Drawing HP text
            // Draws 4 offset versions for the black outline, then a white version on top
            spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), this.Position + new Vector2(31,131), Color.Black);
            spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), this.Position + new Vector2(29,129), Color.Black);
            spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), this.Position + new Vector2(31,129), Color.Black);
            spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), this.Position + new Vector2(29,131), Color.Black);

            spriteBatch.DrawString(this.StatsFont, this.CurrentHP.ToString(), this.Position + new Vector2(30,130), Color.White);

            // Attack text
            spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(83,129), Color.Black);
            spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(85,131), Color.Black);
            spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(85,129), Color.Black);
            spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(83,131), Color.Black);

            spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), this.Position + new Vector2(84,130), Color.White);
        }
        if (Dying)
        {
            spriteBatch.Draw(this.DeathTexture, this.Position + new Vector2(32, 32), null, new Color(255,255,255,(int)(DeathTimer/1000*255)), 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            DeathTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (DeathTimer > 1500)
            {
                Dying = false;
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position, bool drawShadow)
    {
        // Drawing shadow
        if (drawShadow)
            spriteBatch.Draw(this.ShadowTexture, position + new Vector2(16, 96), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

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
        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), position + new Vector2(83,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), position + new Vector2(85,131), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), position + new Vector2(85,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), position + new Vector2(83,131), Color.Black);

        spriteBatch.DrawString(this.StatsFont, this.Attack.ToString(), position + new Vector2(84,130), Color.White);
    }
}