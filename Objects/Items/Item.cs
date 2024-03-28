using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace superautodungeon.Objects.Items;

public class Item
{
    public MainGame GameParent;
    public bool Active;

    public Item()
    {
        Active = false;
    }

    public Item(MainGame inputParent)
    {
        GameParent = inputParent;
        Active = true;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        
    }
}