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
            Reposition();
            return true;
        }
        return false;
    }

    public void Reposition()
    {
        int count = 0;
        foreach (var hero in HeroList)
        {
            if (!hero.Dead)
            {
                hero.Position =  new Vector2(1285, 25 + count * 230);
                count++;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var hero in HeroList)
        {
            hero.Draw(spriteBatch, gameTime);
        }
    }
}