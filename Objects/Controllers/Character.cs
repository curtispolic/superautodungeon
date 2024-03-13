namespace superautodungeon.Objects.Controllers;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.ConstrainedExecution;
using System.Collections.Generic;
using superautodungeon.Objects.UI;

public class Character
{
    public MainGame GameParent;
    public Vector2 Position;
    public Texture2D Texture, HPTexture, AttackTexture, ShadowTexture, DeathTexture;
    public SpriteFont StatsFont;
    public CharacterHoverPanel HoverPanel;
    public bool Dead, Dying, MouseOver, Active, MeleeHitting;
    public int MaxHP, CurrentHP, BaseAttack, CurrentAttack, Mana;
    public List<int> LastDamageAmounts;
    public double DeathTimer, AnimationTimer;
    public List<double> DamageAnimationTimers;
    public string Name, Description;

    public static int DEATH_TIME = 1000;
    public static int DAMAGE_SHOW_TIME = 250;
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Character()
    {

    }

    public Character(MainGame inputParent)
    {
        // This constructor should only run for active characters
        GameParent = inputParent;
        Mana = 0;
        Dead = false;
        Dying = false;
        DeathTimer = 0;
        DamageAnimationTimers = new();
        LastDamageAmounts = new();
        MeleeHitting = false;
        LoadContent();

        // Hover panels inherit textures so should be called after loadcontent
        HoverPanel = new(this);
    }

    public virtual double CombatStep(double animationDelay)
    {
        // Overriden by units that have combat triggers that have animations
        return 0;
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
                        HoverPanel.Position = new(mouseState.X, mouseState.Y);
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

    public virtual double Die(double animationDelay)
    {
        Dead = true;
        Dying = true;
        DeathTimer = 0 - animationDelay;
        return 1000;
    }

    public virtual double TakeDamage(int damage, double animationDelay)
    {
        double animationTime = 0;
        CurrentHP -= damage;
        LastDamageAmounts.Add(damage);
        DamageAnimationTimers.Add(0 - animationDelay);
        // Handle damage taking effects here, equipment blah blah
        if (CurrentHP <= 0)
        {
            animationTime += Die(animationTime + animationDelay);
        }

        return animationTime;
    }

    public virtual double MeleeHit(double animationDelay)
    {
        MeleeHitting = true;
        AnimationTimer = 0 - animationDelay;

        return 500;
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
        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), this.Position + new Vector2(83,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), this.Position + new Vector2(85,131), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), this.Position + new Vector2(85,129), Color.Black);
        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), this.Position + new Vector2(83,131), Color.Black);

        spriteBatch.DrawString(this.StatsFont, this.CurrentAttack.ToString(), this.Position + new Vector2(84,130), Color.White);

        if (Dead)
        {
            spriteBatch.Draw(DeathTexture, Position + new Vector2(32, 32), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }

        if (MouseOver)
        {
            HoverPanel.Draw(spriteBatch, gameTime);
        }
    }

    public virtual void CombatDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        // If alive or dying, draw the essentials (shadow and stats)
        if (!Dead || Dying)
        {
            // Drawing shadow
            spriteBatch.Draw(ShadowTexture, position + new Vector2(16, 96), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

            // Drawing HP icon
            spriteBatch.Draw(HPTexture, position + new Vector2(24, 128), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

            // Drawing attack
            spriteBatch.Draw(AttackTexture, position + new Vector2(72, 128), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

            // Drawing HP text
            // Draws 4 offset versions for the black outline, then a white version on top
            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + new Vector2(31,131), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + new Vector2(29,129), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + new Vector2(31,129), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + new Vector2(29,131), Color.Black);

            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + new Vector2(30,130), Color.White);

            // Attack text
            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + new Vector2(83,129), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + new Vector2(85,131), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + new Vector2(85,129), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + new Vector2(83,131), Color.Black);

            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + new Vector2(84,130), Color.White);
        }
        // If not doing any on the specially handled character drawing animations, do it here
        if (!MeleeHitting)
        {
            spriteBatch.Draw(Texture, position, null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }
        // If dying, draw the skull, after a suitable delay to account for animations
        if (Dying && DeathTimer >= 0)
        {
            spriteBatch.Draw(DeathTexture, position + new Vector2(32, 32), null, new Color(255,255,255,(int)(DeathTimer/(DEATH_TIME/2)*255)), 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            if (DeathTimer > DEATH_TIME)
            {
                Dying = false;
            }
        }

        // Mouseover panels
        if (MouseOver)
        {
            HoverPanel.Draw(spriteBatch, gameTime);
        }

        // Drawing damage icons
        for (int i = 0; i < DamageAnimationTimers.Count; i++)
        {
            if (DamageAnimationTimers[i] <= DAMAGE_SHOW_TIME && DamageAnimationTimers[i] > 0)
            {
                float slidingUp = (float)DamageAnimationTimers[i] / DAMAGE_SHOW_TIME * -50;

                // Drawing HP icon
                spriteBatch.Draw(HPTexture, position + new Vector2(54, slidingUp), null, new Color(255,255,255,128), 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

                // Draw the amount of damage taken
                spriteBatch.DrawString(StatsFont, "-" + LastDamageAmounts[i].ToString(), position + new Vector2(61,3 + slidingUp), Color.Black);
                spriteBatch.DrawString(StatsFont, "-" + LastDamageAmounts[i].ToString(), position + new Vector2(59,1 + slidingUp), Color.Black);
                spriteBatch.DrawString(StatsFont, "-" + LastDamageAmounts[i].ToString(), position + new Vector2(61,1 + slidingUp), Color.Black);
                spriteBatch.DrawString(StatsFont, "-" + LastDamageAmounts[i].ToString(), position + new Vector2(59,3 + slidingUp), Color.Black);

                spriteBatch.DrawString(StatsFont, "-" + LastDamageAmounts[i].ToString(), position + new Vector2(60,2 + slidingUp), Color.White);
            }

            DamageAnimationTimers[i] += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        // Timer handling
        DeathTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        AnimationTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
    }
}