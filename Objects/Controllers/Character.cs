namespace superautodungeon.Objects.Controllers;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using superautodungeon.Objects.UI;

public class Character
{
    public MainGame GameParent;
    public Vector2 Position;
    public Texture2D Texture, HPTexture, AttackTexture, ManaTexture, ShadowTexture, DeathTexture, ManaAnimationSheet;
    public SpriteFont StatsFont;
    public CharacterHoverPanel HoverPanel;
    public bool Dead, Dying, MouseOver, Active, MeleeHitting;
    public int MaxHP, CurrentHP, BaseAttack, CurrentAttack, Mana;
    // Despite the name, damage amounts and timers is also for healing
    public List<int> LastDamageAmounts;
    public double DeathTimer, MeleeTimer, ManaGainTimer;
    public List<double> DamageAnimationTimers;
    public string Name, Description;

    public static int DEATH_TIME = 1000;
    public static int DAMAGE_SHOW_TIME = 250;
    public static int MANA_GAIN_TIME = 250;
    public static Vector2 HP_OFFSET = new(30,130);
    public static Vector2 ATTACK_OFFSET = new(84,130);
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
        HoverPanel = new();
        ManaGainTimer = MANA_GAIN_TIME + 1;
        LoadContent();
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
                        HoverPanel = new(this, graphics, new Vector2(mouseState.X, mouseState.Y));
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

    public virtual bool CombatUpdate(MouseState mouseState, GraphicsDeviceManager graphics, GameTime gameTime, Vector2 combatPos)
    {
        // This is just for handling mouseover status, say for a mouseover panel to show character information
        // Specifically in combat
        // Only handle active characters
        if (Active)
        {   
            // Handle instances where the mouse is inside the game window
            if (0 <= mouseState.X && mouseState.X <= graphics.PreferredBackBufferWidth && 0 <= mouseState.Y && mouseState.Y <= graphics.PreferredBackBufferHeight)
            {
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    // Hovering inside self
                    if (mouseState.X > combatPos.X && mouseState.X < combatPos.X + Texture.Width &&
                    mouseState.Y > combatPos.Y && mouseState.Y < combatPos.Y + Texture.Height)
                    {
                        // Mouseover effect, return asap to not hit the false at the end
                        HoverPanel = new(this, graphics, new Vector2(mouseState.X, mouseState.Y));
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

    public virtual double GainMana(int mana, double animationDelay)
    {
        double animationTime = MANA_GAIN_TIME;
        Mana += mana;
        ManaGainTimer = 0 - animationDelay;

        return animationTime;
    }

    public virtual double TakeDamage(int damage, double animationDelay)
    {
        double animationTime = 0;
        CurrentHP -= damage;
        LastDamageAmounts.Add(0 - damage);
        DamageAnimationTimers.Add(0 - animationDelay);

        // Handle damage taking effects here, equipment blah blah
        
        if (CurrentHP <= 0)
        {
            animationTime += Die(animationTime + animationDelay);
        }

        return animationTime;
    }

    public virtual double ReceiveHealing(int healing, double animationDelay)
    {
        double animationTime = 0;

        if (CurrentHP < MaxHP)
        {
            CurrentHP += healing;
            if (CurrentHP > MaxHP)
            {
                int temp = healing - (CurrentHP - MaxHP);
                CurrentHP = MaxHP;
                LastDamageAmounts.Add(temp);
                DamageAnimationTimers.Add(0 - animationDelay);
            }
            else
            {
                LastDamageAmounts.Add(healing);
                DamageAnimationTimers.Add(0 - animationDelay);
            }
            // Handle healing recieved effects here
        }

        return animationTime;
    }

    public virtual double MeleeHit(double animationDelay)
    {
        MeleeHitting = true;
        MeleeTimer = 0 - animationDelay;

        return 500;
    }

    public virtual void LoadContent()
    {
        HPTexture = GameParent.Content.Load<Texture2D>("UI/heart");
        AttackTexture = GameParent.Content.Load<Texture2D>("UI/attack");
        StatsFont = GameParent.Content.Load<SpriteFont>("statsFont");
        ShadowTexture = GameParent.Content.Load<Texture2D>("Effects/shadow50");
        DeathTexture = GameParent.Content.Load<Texture2D>("Effects/death");
        ManaTexture = GameParent.Content.Load<Texture2D>("UI/mana");
        ManaAnimationSheet = GameParent.Content.Load<Texture2D>("Effects/mana_gain");
    }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Drawing shadow
        spriteBatch.Draw(ShadowTexture, Position + new Vector2(16, 96), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // Drawing Self
        spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // Drawing HP icon
        spriteBatch.Draw(HPTexture, Position + new Vector2(24, 128), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // Drawing attack
        spriteBatch.Draw(AttackTexture, Position + new Vector2(72, 128), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

        // Drawing HP text
        // Offset the text to be central to the icon
        Vector2 offset = new(10 - StatsFont.MeasureString(CurrentHP.ToString()).X / 2, 10 - StatsFont.MeasureString(CurrentHP.ToString()).Y / 2);

        // Draws 4 offset versions for the black outline, then a white version on top
        spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), Position + HP_OFFSET + offset + new Vector2(1, 1), Color.Black);
        spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), Position + HP_OFFSET + offset + new Vector2(1, -1), Color.Black);
        spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), Position + HP_OFFSET + offset + new Vector2(-1, 1), Color.Black);
        spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), Position + HP_OFFSET + offset + new Vector2(-1, -1), Color.Black);

        spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), Position + HP_OFFSET + offset, Color.White);

        // Attack text
        offset = new(6 - StatsFont.MeasureString(CurrentAttack.ToString()).X / 2, 10 - StatsFont.MeasureString(CurrentAttack.ToString()).Y / 2);

        spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), Position + ATTACK_OFFSET + offset + new Vector2(1, 1), Color.Black);
        spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), Position + ATTACK_OFFSET + offset + new Vector2(1, -1), Color.Black);
        spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), Position + ATTACK_OFFSET + offset + new Vector2(-1, 1), Color.Black);
        spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), Position + ATTACK_OFFSET + offset + new Vector2(-1, -1), Color.Black);

        spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), Position + ATTACK_OFFSET + offset, Color.White);

        if (Dead)
        {
            spriteBatch.Draw(DeathTexture, Position + new Vector2(32, 32), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
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
            // Offset the text to be central to the icon
            Vector2 offset = new(10 - StatsFont.MeasureString(CurrentHP.ToString()).X / 2, 10 - StatsFont.MeasureString(CurrentHP.ToString()).Y / 2);

            // Draws 4 offset versions for the black outline, then a white version on top
            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + HP_OFFSET + offset + new Vector2(1, 1), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + HP_OFFSET + offset + new Vector2(1, -1), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + HP_OFFSET + offset + new Vector2(-1, 1), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + HP_OFFSET + offset + new Vector2(-1, -1), Color.Black);

            spriteBatch.DrawString(StatsFont, CurrentHP.ToString(), position + HP_OFFSET + offset, Color.White);

            // Attack text
            offset = new(6 - StatsFont.MeasureString(CurrentAttack.ToString()).X / 2, 10 - StatsFont.MeasureString(CurrentAttack.ToString()).Y / 2);

            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + ATTACK_OFFSET + offset + new Vector2(1, 1), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + ATTACK_OFFSET + offset + new Vector2(1, -1), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + ATTACK_OFFSET + offset + new Vector2(-1, 1), Color.Black);
            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + ATTACK_OFFSET + offset + new Vector2(-1, -1), Color.Black);

            spriteBatch.DrawString(StatsFont, CurrentAttack.ToString(), position + ATTACK_OFFSET + offset, Color.White);
        }
        // If not doing any of the specially handled character drawing animations, do it here
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
                // Stops mouseover from hanging when something dies while hovered over
                MouseOver = false;
                Dying = false;
            }
        }

        // Drawing damage icons (also healing)
        for (int i = 0; i < DamageAnimationTimers.Count; i++)
        {
            if (DamageAnimationTimers[i] <= DAMAGE_SHOW_TIME && DamageAnimationTimers[i] > 0)
            {
                float slidingUp = (float)DamageAnimationTimers[i] / DAMAGE_SHOW_TIME * -50;
                string text = LastDamageAmounts[i].ToString();
                if (LastDamageAmounts[i] > 0)
                    text = "+" + text;
                Vector2 offset = new(10 - StatsFont.MeasureString(text).X / 2, 10 - StatsFont.MeasureString(text).Y / 2);

                // Drawing HP icon
                spriteBatch.Draw(HPTexture, position + new Vector2(54, slidingUp), null, new Color(255,255,255,128), 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

                // Draw the amount of damage taken
                spriteBatch.DrawString(StatsFont, text, position + offset + new Vector2(61,3 + slidingUp), Color.Black);
                spriteBatch.DrawString(StatsFont, text, position + offset + new Vector2(59,1 + slidingUp), Color.Black);
                spriteBatch.DrawString(StatsFont, text, position + offset + new Vector2(61,1 + slidingUp), Color.Black);
                spriteBatch.DrawString(StatsFont, text, position + offset + new Vector2(59,3 + slidingUp), Color.Black);

                spriteBatch.DrawString(StatsFont, text, position + offset + new Vector2(60,2 + slidingUp), Color.White);
            }

            DamageAnimationTimers[i] += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        // Animation for gaining mana
        if (ManaGainTimer > 0 && ManaGainTimer < MANA_GAIN_TIME)
        {
            Rectangle animationRectangle;

            if (ManaGainTimer > MANA_GAIN_TIME / 3 * 2)
            {
                animationRectangle = new(0, 128, 128, 64);
            }
            else if (ManaGainTimer > MANA_GAIN_TIME / 3)
            {
                animationRectangle = new(0, 64, 128, 64);
            }
            else
            {
                animationRectangle = new(0, 0, 128, 64);
            }

            spriteBatch.Draw(ManaAnimationSheet, position + new Vector2(0, -30), animationRectangle, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }

        // Timer handling
        ManaGainTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        DeathTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        MeleeTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
    }

    public virtual void ManaDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        // Draw mana icon and text
        if (!Dead || Dying)
        {
            // Icon
            spriteBatch.Draw(ManaTexture, position + new Vector2(48, 156), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);

            // Offset the text to be central to the icon
            Vector2 offset = new(8 - StatsFont.MeasureString(Mana.ToString()).X / 2, 0);

            // Text
            spriteBatch.DrawString(StatsFont, Mana.ToString(), position + offset + new Vector2(55,159), Color.Black);
            spriteBatch.DrawString(StatsFont, Mana.ToString(), position + offset + new Vector2(55,157), Color.Black);
            spriteBatch.DrawString(StatsFont, Mana.ToString(), position + offset + new Vector2(53,159), Color.Black);
            spriteBatch.DrawString(StatsFont, Mana.ToString(), position + offset + new Vector2(53,157), Color.Black);

            spriteBatch.DrawString(StatsFont, Mana.ToString(), position + offset + new Vector2(54,158), Color.White);
        }
    }
}