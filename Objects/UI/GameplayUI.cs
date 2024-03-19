using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using superautodungeon.Objects.Controllers;
using superautodungeon.Objects.Heroes;

namespace superautodungeon.Objects.UI;

public class GameplayUI
{
    public MainGame GameParent;
    public Party PlayerParty;
    public SpriteFont Font;
    public Vector2 PickupOffset;
    public Hero PickedUpHero;
    public int PickedUp;
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
        PickedUp = -1;
        PickedUpHero = new(GameParent, false);
    }

    public void LoadContent()
    {
        Font = GameParent.Content.Load<SpriteFont>("statsFont");
    }

    public int Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        bool clickHandled = false;
        for (int i = 0; i < 4; i++)
        {
            if (PlayerParty.HeroList[i].Update(mouseState, graphics, gameTime))
            {
                // Handle clicks on the party
                // This is specifically for shop buying
                if (GameParent.shop.Active)
                {
                    if (GameParent.shop.PickedUp > -1)
                    {
                        GameParent.shop.BuyHero(GameParent.shop.PickedUp, i);
                    }
                    // If nothing picked up in the shop or UI, pickup the hero
                    else if (PickedUp == -1)
                    {
                        PickedUp = i;
                        PickedUpHero = PlayerParty.HeroList[i];
                        PickupOffset = PlayerParty.HeroList[i].Position - mouseState.Position.ToVector2();
                        clickHandled = true;
                    }
                    else if (i != PickedUp)
                    {
                        PartyReorder(PickedUp, i);
                        clickHandled = true;
                        PickedUp = -1;
                        PickedUpHero = new(GameParent, false);
                    }
                }
                // Don't allow clicking during combat
                else if (!GameParent.combat.Active)
                {
                    // no hero picked up
                    if (PickedUp == -1)
                    {
                        PickedUp = i;
                        PickedUpHero = PlayerParty.HeroList[i];
                        PickupOffset = PlayerParty.HeroList[i].Position - mouseState.Position.ToVector2();
                        clickHandled = true;
                    }
                    // swapping heroes
                    else if (i != PickedUp)
                    {
                        PartyReorder(PickedUp, i);
                        clickHandled = true;
                        PickedUp = -1;
                        PickedUpHero = new(GameParent, false);
                    }
                }
            }
        }

        // Drop the hero if it's an unhandled click
        if (PickedUp != -1 && mouseState.LeftButton == ButtonState.Pressed && !clickHandled)
        {
            PickedUp = -1;
            PickedUpHero = new(GameParent, false);
        }

        return -1;
    }

    public void PartyReorder(int index1, int index2)
    {
        Hero temp = PlayerParty.HeroList[index1];
        PlayerParty.HeroList[index1] = PlayerParty.HeroList[index2];
        PlayerParty.HeroList[index2] = temp;
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
                // Draw the name
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(11, -16), Color.Black);
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(9, -14), Color.Black);
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(9, -16), Color.Black);
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(11, -14), Color.Black);
                spriteBatch.DrawString(Font, hero.Name, hero.Position + new Vector2(10, -15), Color.White);

                hero.Draw(spriteBatch, gameTime);

                if (hero.Dead)
                    spriteBatch.Draw(hero.DeathTexture, hero.Position + new Vector2(32, 32), null, Color.White, 0f, new Vector2(0, 0), Vector2.One, SpriteEffects.None, 0f);
            }
            else
            {
                hero.DrawShadowOnly(spriteBatch, gameTime, hero.Position);
            }
        }
    }

    public void MouseoverDraw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Handle mouseover here to make sure it's drawn at the front
        foreach (var hero in PlayerParty.HeroList)
        {
            if (hero.MouseOver && PickedUp == -1)
            {
                if (GameParent.shop.Active)
                {
                    // If a shop is active, only draw mouseover if no picked up hero
                    if (GameParent.shop.PickedUp == -1)
                    {
                        hero.HoverPanel.Draw(spriteBatch, gameTime);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    hero.HoverPanel.Draw(spriteBatch, gameTime);
                }
                
            }
        }

        // Checking for picked up hero for special drawing order
        PickedUpHero = new(GameParent, false);
        foreach (var hero in PlayerParty.HeroList)
        {
            if (hero.PickedUp)
            {
                PickedUpHero = hero;
                break;
            }
        }

        // Draw picked up hero last to show above all others
        if (PickedUpHero.Active)
            PickedUpHero.Draw(spriteBatch, gameTime);
    }
}