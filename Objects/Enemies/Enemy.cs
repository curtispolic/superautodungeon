namespace superautodungeon.Objects.Enemies;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using superautodungeon.Objects.Controllers;

public class Enemy : Character
{
    public bool MeleeHitting;
    public double AnimationTimer;
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Enemy(MainGame inputParent): base(inputParent)
    {
        AnimationTimer = 0;
        // Intentionally blank
    }

    public void MeleeHit()
    {
        MeleeHitting = true;
        AnimationTimer = 0;
    }

    public override void CombatDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
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
            spriteBatch.DrawString(StatsFont, Attack.ToString(), position + new Vector2(83,129), Color.Black);
            spriteBatch.DrawString(StatsFont, Attack.ToString(), position + new Vector2(85,131), Color.Black);
            spriteBatch.DrawString(StatsFont, Attack.ToString(), position + new Vector2(85,129), Color.Black);
            spriteBatch.DrawString(StatsFont, Attack.ToString(), position + new Vector2(83,131), Color.Black);

            spriteBatch.DrawString(StatsFont, Attack.ToString(), position + new Vector2(84,130), Color.White);
        }
        if (Dying)
        {
            spriteBatch.Draw(DeathTexture, position + new Vector2(32, 32), null, new Color(255,255,255,(int)(DeathTimer/1000*255)), 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            DeathTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (DeathTimer > 1500)
            {
                Dying = false;
            }
        }
        if (MeleeHitting)
        {
            // Draw just the character swinging forward for the hit
            if (AnimationTimer <= 250)
            {
                spriteBatch.Draw(Texture, position - new Vector2((float)(AnimationTimer/3), 0), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }
            else if (AnimationTimer > 250 && AnimationTimer < 500)
            {
                spriteBatch.Draw(Texture, position - new Vector2(83, 0) + new Vector2((float)((AnimationTimer-250)/3), 0), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }
            else
            {
                MeleeHitting = false;
                spriteBatch.Draw(Texture, position, null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }

            AnimationTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        else
        {
            // If not melee hitting, draw in the normal location
            spriteBatch.Draw(Texture, position, null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
        }
    }
}