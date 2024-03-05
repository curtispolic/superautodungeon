using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using superautodungeon.Objects.Heroes;
using superautodungeon.Objects.UI;

namespace superautodungeon.Objects.Controllers;

public class Shop
{
    public MainGame GameParent;
    public List<Hero> BuyableHeroes;
    public Button RerollButton, UpgradeShopButton, ExitButton;
    public int ShopTier, RerollCost, UpgradeCost;
    public Shop(MainGame inputParent, int inputTier)
    {
        GameParent = inputParent;
        ShopTier = inputTier;
        RerollCost = 50;
        UpgradeCost = 300;
        RerollButton = new Button(GameParent, $"Reroll: {RerollCost}GP", new Vector2(50, 250));
        UpgradeShopButton = new Button(GameParent, $"Upgrade Shop Tier: {UpgradeCost}GP", new Vector2(50, 300));
        ExitButton = new Button(GameParent, "Exit Shop", new Vector2(50, 350));
        LoadContent();
        ReRoll();
    }

    public void BuyHero(Hero inputHero, int inputIndex)
    {
        Hero draggedOnHero = GameParent.playerParty.HeroList[inputIndex];
        if (inputHero.Class == draggedOnHero.Class)
        {
            // Level up hero in the party
        }
        else if (draggedOnHero.Class == "None")
        {
            // Buy into that index
            GameParent.playerParty.HeroList[inputIndex] = inputHero;
        }
    }

    public void ReRoll()
    {
        RollEquipment();
        RollHeroes();
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
            BuyableHeroes.Add(random.Next(2) == 1 ? new Knight(GameParent) : new Wizard(GameParent));
        }
    }

    public void UpgradeShopTier()
    {
        GameParent.ShopTier++;
        ShopTier++;
        RollEquipment();
        RollHeroes();
    }

    public void LoadContent()
    {
        RerollButton.LoadContent();
        UpgradeShopButton.LoadContent();
        ExitButton.LoadContent();
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

        for (int i = 0; i < BuyableHeroes.Count; i++)
        {
            var hero = BuyableHeroes[i];
            hero.Draw(spriteBatch, gameTime, new Vector2(20 + i * 200, 20));
        }

        RerollButton.Draw(spriteBatch, gameTime);
        UpgradeShopButton.Draw(spriteBatch, gameTime);
        ExitButton.Draw(spriteBatch, gameTime);
    }
}