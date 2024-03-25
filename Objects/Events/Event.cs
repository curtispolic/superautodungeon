using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using superautodungeon.Objects.Rooms;

namespace superautodungeon.Objects.Events;

public class Event
{
    public Room RoomParent;
    public SpriteFont Font;
    public bool Active, Completed;

    public Event()
    {
        Active = false;
    }

    public Event(Room inputParent)
    {
        RoomParent = inputParent;
        Completed = false;
        Active = false;
    }

    public virtual void Start()
    {
        // Inteneded override
        Active = true;
    }

    public virtual void Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        
    }

    public virtual void LoadContent()
    {
        // Intended override
        Font = RoomParent.LevelParent.GameParent.Content.Load<SpriteFont>("statsFont");
    }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Just draws the base window, specific events will draw the buttons
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
    }
}