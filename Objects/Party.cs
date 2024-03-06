namespace superautodungeon.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using superautodungeon.Objects.Heroes;

public class Party
{
    public List<Hero> HeroList;
    public MainGame GameParent;

    public Party(MainGame inputParent)
    {
        GameParent = inputParent;
        HeroList = new();
        for (int i = 0; i < 4; i++)
        {
            HeroList.Add(new(GameParent, false));
            HeroList[i].Position = new(1285, 25 + i * 230);
        }
    }

    public bool Add(Hero inputHero, int inputIndex)
    {
        if (!HeroList[inputIndex].Active)
        {
            HeroList[inputIndex] = inputHero;
            HeroList[inputIndex].Position =  new Vector2(1285, 25 + inputIndex * 230);
            return true;
        }
        return false;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // This method is only used to draw in combat
        for (int i = 0; i < 4; i++)
        {
            var hero = HeroList[i];
            if (hero.Active)
                hero.CombatDraw(spriteBatch, gameTime, new Vector2(400 - 100 * i, 200));
            else
                hero.DrawShadowOnly(spriteBatch, gameTime, new Vector2(400 - 100 * i, 200));
        }
    }
}