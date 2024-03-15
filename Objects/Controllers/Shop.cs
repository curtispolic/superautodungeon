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
    public Vector2 PickupOffset;
    public Level LevelParent;
    public int ShopTier, RerollCost, UpgradeCost, PickedUp;
    public bool Active;

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
    }

    public void Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        if(RerollButton.Update(mouseState, graphics, gameTime))
            OnClickReRoll();
        
        if(UpgradeShopButton.Update(mouseState, graphics, gameTime))
            OnClickUpgradeShopTier();

        if(ExitButton.Update(mouseState, graphics, gameTime))
            ExitShop();
        
        // PickedUp prevents us from picking up multiple heroes
        PickedUp = -1;
        for (int i = 0; i < BuyableHeroes.Count; i++)
        {
            if (BuyableHeroes[i].PickedUp)
            {
                PickedUp = i;
                break;
            }
        }

        // Set the offset for the pickup so it's smooth
        for (int i = 0; i < BuyableHeroes.Count; i++)
        {
            if (PickedUp == -1)
                if (BuyableHeroes[i].Update(mouseState, graphics, gameTime))
                {
                    PickedUp = i;
                    PickupOffset = BuyableHeroes[i].Position - mouseState.Position.ToVector2();
                }
        }


    }

    public void BuyHero(int boughtHeroIndex, int draggedOntoIndex)
    {
        Hero draggedOnHero = LevelParent.GameParent.playerParty.HeroList[draggedOntoIndex];
        Hero inputHero = BuyableHeroes[boughtHeroIndex];
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
        }
        else
        {
            // Cancel the buy
            return;
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
        // This should handle the gold checking prior to actually rerolling
        ReRoll();
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
            Hero newHero = new();

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
        // This will handle the gold checking and tier checking prior to the method call
        UpgradeShopTier();
    }

    public void ExitShop()
    {
        Active = false;
        LevelParent.Active = true;
    }

    public void LoadContent()
    {
        // GP Icon to come
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

        // Checking for picked up hero for special drawing order
        // Also drawing non picked up hereoes
        PickedUpHero = new(LevelParent.GameParent, false);
        for (int i = 0; i < BuyableHeroes.Count; i++)
        {
            var hero = BuyableHeroes[i];
            if (hero.PickedUp)
            {
                PickedUpHero = hero;
            }
            else
            {
                hero.Draw(spriteBatch, gameTime);
            }
        }

        RerollButton.Draw(spriteBatch, gameTime);
        UpgradeShopButton.Draw(spriteBatch, gameTime);
        ExitButton.Draw(spriteBatch, gameTime);

        // Draw picked up hero last to show above all others
        if (PickedUpHero.Active)
            PickedUpHero.Draw(spriteBatch, gameTime);
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
    }
}