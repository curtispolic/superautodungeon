using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using superautodungeon.Objects.Controllers;

namespace superautodungeon.Objects.UI;

public class GameplayUI
{
    public MainGame GameParent;
    public Party PlayerParty;
    private SpriteFont Font;
    public bool Active;

    public GameplayUI()
    {
        Active = false;
    }

    public GameplayUI(MainGame inputParent, Party inputParty)
    {
        GameParent = inputParent;
        PlayerParty = inputParty;
        LoadContent();
        Active = true;
    }

    public void LoadContent()
    {
        Font = GameParent.Content.Load<SpriteFont>("statsFont");
    }

    public int Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        for (int i = 0; i < 4; i++)
        {
            if (PlayerParty.HeroList[i].Update(mouseState, graphics, gameTime))
            {
                // Handle clicks on the party
                // This is specifically for shop buying
                if (GameParent.shop.Active)
                {
                    if (GameParent.shop.PickedUp > 0)
                    {
                        GameParent.shop.BuyHero(GameParent.shop.PickedUp, i);
                    }
                }
            }
        }

        return -1;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Temp texture to draw the outline of the panel
        Texture2D _texture;
        _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        _texture.SetData(new Color[] { new Color(150, 150, 150) });

        // Temp texture to draw inner colour of the panel
        Texture2D _texture2;
        _texture2 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        _texture2.SetData(new Color[] { new Color(200, 200, 200) });

        // Draw the bottom panel
        spriteBatch.Draw(_texture, new Rectangle(0, 630, 1285, 270), Color.White);
        spriteBatch.Draw(_texture2, new Rectangle(5, 635, 1280, 260), Color.White);

        // Draw the side panel
        spriteBatch.Draw(_texture, new Rectangle(1280, 0, 320, 900), Color.White);
        spriteBatch.Draw(_texture2, new Rectangle(1285, 5, 310, 890), Color.White);

        // TODO: Draw text explaining what is meant to be in each panel

        // Draw the player's party on the side panel
        foreach (var hero in PlayerParty.HeroList)
        {
            // Draw the hero
            if (hero.Active)
            {
                hero.Draw(spriteBatch, gameTime);

                // Draw the name
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(11, -16), Color.Black);
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(9, -14), Color.Black);
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(9, -16), Color.Black);
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(11, -14), Color.Black);
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(10, -15), Color.White);

                if (hero.Dead)
                    spriteBatch.Draw(hero.DeathTexture, hero.Position + new Vector2(32, 32), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }
            else
            {
                hero.DrawShadowOnly(spriteBatch, gameTime, hero.Position);
            }
        }
    }
}