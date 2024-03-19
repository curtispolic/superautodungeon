using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using superautodungeon.Objects.Heroes;
using superautodungeon.Objects.UI;

namespace superautodungeon.Objects.Controllers;

public class Shop
{
    public List<Hero> BuyableHeroes;
    public Hero PickedUpHero;
    public Button RerollButton, UpgradeShopButton, ExitButton;
    public SpriteFont Font;
    public Vector2 PickupOffset;
    public Level LevelParent;
    public int ShopTier, RerollCost, UpgradeCost, PickedUp;
    public bool Active, leftMouseDown;

    public Shop()
    {
        Active = false;
    }

    public Shop(Level inputLevel, int inputTier)
    {
        LevelParent = inputLevel;
        ShopTier = inputTier;
        RerollCost = 50;
        UpgradeCost = 300;
        RerollButton = new Button(LevelParent.GameParent, $"Reroll: {RerollCost}GP", new Vector2(50, 250));
        UpgradeShopButton = new Button(LevelParent.GameParent, $"Upgrade Shop Tier: {UpgradeCost}GP", new Vector2(50, 300));
        ExitButton = new Button(LevelParent.GameParent, "Exit Shop", new Vector2(50, 350));
        LoadContent();
        ReRoll();
        Active = true;
        leftMouseDown = false;
    }

    public void Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        if(RerollButton.Update(mouseState, graphics, gameTime))
        {
            OnClickReRoll();
        }        
        else if(UpgradeShopButton.Update(mouseState, graphics, gameTime))
        {
            OnClickUpgradeShopTier();
        }
        else if(ExitButton.Update(mouseState, graphics, gameTime))
        {
            ExitShop();
        }
        else if (mouseState.LeftButton == ButtonState.Pressed && !leftMouseDown)
        {
            // PickedUp prevents us from picking up multiple heroes
            PickedUp = -1;
            for (int i = 0; i < BuyableHeroes.Count; i++)
            {
                if (BuyableHeroes[i].PickedUp)
                {
                    PickedUp = i;
                    PickedUpHero = BuyableHeroes[i];
                    break;
                }
            }

            if (PickedUp == -1)
            {
                // Check for hero clicking
                for (int i = 0; i < BuyableHeroes.Count; i++)
                {
                    if (BuyableHeroes[i].Update(mouseState, graphics, gameTime) && !leftMouseDown)
                    {
                        leftMouseDown = true;
                        PickedUp = i;
                        PickupOffset = BuyableHeroes[i].Position - mouseState.Position.ToVector2();
                        BuyableHeroes[i].PickedUp = true;
                    }
                }
            }
            else
            {
                PickedUpHero.PickedUp = false;
                PickedUpHero = new(LevelParent.GameParent, false);
            }

            leftMouseDown = true;
        }

        if (leftMouseDown && mouseState.LeftButton == ButtonState.Released)
        {
            leftMouseDown = false;
        }

    }

    public void BuyHero(int boughtHeroIndex, int draggedOntoIndex)
    {
        Hero draggedOnHero = LevelParent.GameParent.playerParty.HeroList[draggedOntoIndex];
        Hero inputHero = BuyableHeroes[boughtHeroIndex];
        if (LevelParent.GameParent.playerParty.GP >= inputHero.Cost)
        {
            if (inputHero.Class == draggedOnHero.Class)
            {
                // Level up hero in the party
            }
            else if (!draggedOnHero.Active)
            {
                // Buy into that index
                inputHero.Buyable = false;
                inputHero.PickedUp = false;
                LevelParent.GameParent.playerParty.Add(inputHero, draggedOntoIndex);
                LevelParent.GameParent.playerParty.GP -= inputHero.Cost;
            }
            else
            {
                // Cancel the buy
                return;
            }
        }
        else
        {
            // Not enough money handling
        }

        // Replace bought hero with inactive
        BuyableHeroes[boughtHeroIndex] = new Hero(LevelParent.GameParent, false)
        {
            Position = new Vector2(20 + boughtHeroIndex * 200, 20)
        };
    }

    public void ReRoll()
    {
        RollEquipment();
        RollHeroes();
    }

    public void OnClickReRoll()
    {
        if (LevelParent.GameParent.playerParty.GP >= RerollCost)
        {
            LevelParent.GameParent.playerParty.GP -= RerollCost;
            ReRoll();
        }
        else
        {
            // Not enough money handling
        }
    }

    public void RollEquipment()
    {
        // TODO
        return;
    }

    public void RollHeroes()
    {
        BuyableHeroes = new();
        Random random = new();
        for (int i = 0; i < 5; i++)
        {
            // Will contian logic for rolling from valid shop tiers
            int heroClass = random.Next(4);
            Hero newHero = new(LevelParent.GameParent, false);

            switch (heroClass)
            {
                case 0:
                    newHero = new Knight(LevelParent.GameParent);
                    break;
                case 1:
                    newHero = new Wizard(LevelParent.GameParent);
                    break;
                case 2:
                    newHero = new Ranger(LevelParent.GameParent);
                    break;
                case 3:
                    newHero = new Priest(LevelParent.GameParent);
                    break;
            }

            BuyableHeroes.Add(newHero);
            BuyableHeroes[i].Position = new Vector2(20 + i * 200, 20);
        }
    }

    public void UpgradeShopTier()
    {
        LevelParent.GameParent.ShopTier++;
        ShopTier++;
        ReRoll();
    }

    public void OnClickUpgradeShopTier()
    {
        if (LevelParent.GameParent.playerParty.GP >= UpgradeCost)
        {
            LevelParent.GameParent.playerParty.GP -= UpgradeCost;
            UpgradeShopTier();
        }
        else
        {
            // Not enough money handling
        }
    }

    public void ExitShop()
    {
        Active = false;
        LevelParent.Active = true;
    }

    public void LoadContent()
    {
        // GP Icon to come
        Font = LevelParent.GameParent.Content.Load<SpriteFont>("statsFont");
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Temp texture to draw the outline of the panel
        Texture2D _texture;
        _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        _texture.SetData(new Color[] { new Color(150, 150, 150) });

        spriteBatch.Draw(_texture, new Rectangle(20, 20, 1240, 590), Color.White);

        // Temp texture to draw inner colour of the panel
        Texture2D _texture2;
        _texture2 = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        _texture2.SetData(new Color[] { new Color(200, 200, 200) });

        spriteBatch.Draw(_texture2, new Rectangle(25, 25, 1230, 580), Color.White);

        // Button drawing
        RerollButton.Draw(spriteBatch, gameTime);
        UpgradeShopButton.Draw(spriteBatch, gameTime);
        ExitButton.Draw(spriteBatch, gameTime);

        spriteBatch.DrawString(Font, "GP: " + LevelParent.GameParent.playerParty.GP.ToString(), new Vector2(400, 400), Color.Black);

        foreach (var hero in BuyableHeroes)
        {
            if (!hero.PickedUp)
            {
                hero.Draw(spriteBatch, gameTime);
                spriteBatch.DrawString(Font, "Cost: " + hero.Cost.ToString() + "GP", hero.Position + new Vector2(20, 170), Color.Black);
            }
        }
    }

    public void MouseoverDraw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Draw the mouseover panel here so it's over all elements
        foreach (var hero in BuyableHeroes)
        {
            if (hero.MouseOver && !PickedUpHero.Active)
            {
                hero.HoverPanel.Draw(spriteBatch, gameTime);
            }
        }

        // Checking for picked up hero for special drawing order
        PickedUpHero = new(LevelParent.GameParent, false);
        for (int i = 0; i < BuyableHeroes.Count; i++)
        {
            var hero = BuyableHeroes[i];
            if (hero.PickedUp)
            {
                PickedUpHero = hero;
            }
        }

        // Draw picked up hero last to show above all others
        if (PickedUpHero.Active)
            PickedUpHero.Draw(spriteBatch, gameTime);
    }
}