namespace superautodungeon.Objects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

using superautodungeon.Objects.Heroes;

public class Party
{
    public List<Hero> HeroList;
    public Vector2 Position;
    public MainGame GameParent;

    public Party(MainGame inputParent, Vector2 inputPosition)
    {
        GameParent = inputParent;
        HeroList = new()
        {
            new(GameParent, false),
            new(GameParent, false),
            new(GameParent, false),
            new(GameParent, false)
        };
        Position = inputPosition;
    }

    public bool Add(Hero inputHero, int inputIndex)
    {
        if (HeroList[inputIndex].Active)
        {
            HeroList.Add(inputHero);
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
                hero.Position = Position - new Vector2(count*128, 0);
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