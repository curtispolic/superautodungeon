namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Hero : Character
{
    public string Class,  Gender;
    public bool Active;
    // TODO Weapon
    // TODO Armour
    // TODO Trinket

    public Hero(MainGame inputParent, bool inputActive): base(inputParent)
    {
        // Active should only be false for manual creations
        Active = inputActive;
        LoadContent();
    }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public virtual void DrawShadowOnly(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position)
    {
        spriteBatch.Draw(ShadowTexture, position + new Vector2(16, 96), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
    }
}