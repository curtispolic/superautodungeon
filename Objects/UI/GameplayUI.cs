using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using superautodungeon.Objects.Heroes;

namespace superautodungeon.Objects.UI;

public class GameplayUI
{
    public MainGame GameParent;
    public Party PlayerParty;
    public GameplayUI(MainGame inputParent, Party inputParty)
    {
        GameParent = inputParent;
        PlayerParty = inputParty;
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

        for (int i = 0; i < PlayerParty.HeroList.Count; i++)
        {
            Hero tempHero = PlayerParty.HeroList[i];
            tempHero.Draw(spriteBatch, gameTime, new Vector2(1285, 5 + i * 200));
        }
    }
}