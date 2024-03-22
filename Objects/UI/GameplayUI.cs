using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
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
    public bool Active, leftMouseDown;

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
        leftMouseDown = false;
    }

    public void LoadContent()
    {
        Font = GameParent.Content.Load<SpriteFont>("statsFont");
    }

    public void Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        // Only handle clicks inside the game window
        if (0 <= mouseState.X && mouseState.X <= graphics.PreferredBackBufferWidth && 0 <= mouseState.Y && mouseState.Y <= graphics.PreferredBackBufferHeight)
        {
            // Handle a single click and flag to not repeat
            if (mouseState.LeftButton == ButtonState.Pressed && !leftMouseDown)
            {
                bool handledClick = false;
                // If the shop is active and has a picked up hero, handle buying
                if (GameParent.shop.Active)
                {
                    if (GameParent.shop.PickedUp > -1)
                    {
                        for (int i = 0; i < PlayerParty.HeroList.Count; i++)
                        {
                            if (PlayerParty.HeroList[i].Update(mouseState, graphics, gameTime))
                            {
                                GameParent.shop.BuyHero(GameParent.shop.PickedUp, i);
                                handledClick = true;
                            }
                        }
                    }
                }

                // Don't allow changes during combat
                if (!GameParent.combat.Active && !handledClick)
                {
                    // Check for hero clicking
                    for (int i = 0; i < PlayerParty.HeroList.Count; i++)
                    {
                        if (PlayerParty.HeroList[i].Update(mouseState, graphics, gameTime))
                        {
                            if (PickedUp == -1)
                            {
                                PickedUp = i;
                                PickupOffset = PlayerParty.HeroList[i].Position - mouseState.Position.ToVector2();
                                PlayerParty.HeroList[i].PickedUp = true;
                                handledClick = true;
                            }
                            else if (PickedUp != i)
                            {
                                if (PlayerParty.HeroList[i].Class == PlayerParty.HeroList[PickedUp].Class)
                                {
                                    // Handle combining heroes
                                    Vector2 temp = PlayerParty.HeroList[PickedUp].Position;
                                    PlayerParty.HeroList[i].GainXP(PlayerParty.HeroList[PickedUp].XP + 1);
                                    PlayerParty.HeroList[PickedUp] = new(GameParent, false)
                                    {
                                        Position = temp
                                    };
                                    PickedUp = -1;
                                    PickedUpHero = new(GameParent, false);
                                }
                                else
                                {
                                    // Handle reordering heroes
                                    PartyReorder(PickedUp, i);
                                    handledClick = true;
                                    PickedUp = -1;
                                    PickedUpHero.PickedUp = false;
                                    PickedUpHero = new(GameParent, false);
                                }
                            }
                        }
                    }
                }

                // If nothing else matched, just drop the picked up if needed
                if (!handledClick)
                {
                    PickedUp = -1;
                    PickedUpHero.PickedUp = false;
                    PickedUpHero = new(GameParent, false);
                }

                leftMouseDown = true;
            }

            // Run updates on buyable heroes even without mouse click to handle mouseover status
            foreach (var hero in PlayerParty.HeroList)
            {
                hero.Update(mouseState, graphics, gameTime);
            }

            // Unflag when the click has been released
            if (mouseState.LeftButton == ButtonState.Released && leftMouseDown)
            {
                leftMouseDown = false;
            }
        }
    }

    public void PartyReorder(int index1, int index2)
    {
        Hero temp = PlayerParty.HeroList[index1];
        Vector2 tempPos = temp.Position;

        temp.Position = PlayerParty.HeroList[index2].Position;
        PlayerParty.HeroList[index2].Position = tempPos;

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
            if (hero.Active && !hero.PickedUp)
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
        for (int i = 0; i < PlayerParty.HeroList.Count; i++)
        {
            var hero = PlayerParty.HeroList[i];
            if (hero.PickedUp)
            {
                PickedUpHero = hero;
            }
        }

        // Draw picked up hero last to show above all others
        if (PickedUpHero.Active)
        {
            var mouseState = Mouse.GetState();
            PickedUpHero.MouseDraw(spriteBatch, gameTime, mouseState.Position.ToVector2() + PickupOffset);
        }
    }
}