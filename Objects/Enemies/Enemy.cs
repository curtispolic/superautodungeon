namespace superautodungeon.Objects.Enemies;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using superautodungeon.Objects.Controllers;

public class Enemy : Character
{
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Enemy(MainGame inputParent): base(inputParent)
    {
        MeleeTimer = 0;
        // Intentionally blank
    }

    public override void CombatDraw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        if (MeleeHitting)
        {
            // Draw just the character swinging forward for the hit
            if (MeleeTimer <= 250)
            {
                spriteBatch.Draw(Texture, position - new Vector2((float)(MeleeTimer/3), 0), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }
            else if (MeleeTimer > 250 && MeleeTimer < 500)
            {
                spriteBatch.Draw(Texture, position - new Vector2(83, 0) + new Vector2((float)((MeleeTimer-250)/3), 0), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }
            else
            {
                MeleeHitting = false;
            }

            MeleeTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        // Do all the general combat drawing
        base.CombatDraw(spriteBatch, gameTime, position);
    }
}