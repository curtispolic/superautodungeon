namespace superautodungeon.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using superautodungeon.Objects.Heroes;

public class Party
{
    public List<Hero> HeroList;
    public Vector2 Position;

    public Party(Vector2 inputPosition)
    {
        HeroList = new();
        Position = inputPosition;
    }

    public bool Add(Hero inputHero)
    {
        if (HeroList.Count < 4)
        {
            HeroList.Add(inputHero);
            Reposition();
            return true;
        }
        return false;
    }

    public void Compress()
    {
        foreach (var hero in HeroList)
        {
            if (hero.Dead)
            {
                HeroList.Remove(hero);
            }
        }
        Reposition();
    }

    // TODO: Keep rendering dead party members until they die

    public void Reposition()
    {
        for (int i = 0; i<HeroList.Count; i++)
        {
            HeroList[i].Position = Position - new Vector2(i*128, 0);
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