using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using superautodungeon.Objects.UI;
using superautodungeon.Objects.Rooms;

namespace superautodungeon.Objects.Events;

public class MinorTreasureEvent : Event
{
    public string Text;
    public Button GoldButton;
    public Button GearButton;
    public Button BothButton;

    public MinorTreasureEvent(Room inputParent) : base(inputParent)
    {
        Text = "As you enter the room, you discover various treasure strewn across the floor.\n" +
        "You worry about your luck changing as you hear a rumbling above, the roof is beginning to collapse!\n\n" +
        "Unable to grab all the treasure, as you flee you grab...";

        GoldButton = new Button(RoomParent.LevelParent.GameParent, "Some gold and gems", new Vector2(50, 300));
        GearButton = new Button(RoomParent.LevelParent.GameParent, "A piece of equipment", new Vector2(50, 350));
        BothButton = new Button(RoomParent.LevelParent.GameParent, "Both", new Vector2(50, 400));

        LoadContent();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        // Only handle clicks inside the game window
        if (0 <= mouseState.X && mouseState.X <= graphics.PreferredBackBufferWidth && 0 <= mouseState.Y && mouseState.Y <= graphics.PreferredBackBufferHeight)
        {
            // Check for button presses first
            if(GoldButton.Update(mouseState, graphics, gameTime))
            {
                OnClickGoldButton();
            }        
            else if(GearButton.Update(mouseState, graphics, gameTime))
            {
                OnClickGearButton();
            }
            else if(BothButton.Update(mouseState, graphics, gameTime))
            {
                OnClickBothButton();
            }
        }
    }

    public void OnClickGoldButton()
    {
        RoomParent.LevelParent.GameParent.playerParty.GP += 100;
        Completed = true;
        Active = false;
        RoomParent.Completed = true;
        RoomParent.Active = false;
    }

    public void OnClickGearButton()
    {
        // TODO: Add gear gain
        Completed = true;
        Active = false;
        RoomParent.Completed = true;
        RoomParent.Active = false;
    }

    public void OnClickBothButton()
    {
        RoomParent.LevelParent.GameParent.playerParty.GP += 100;
        // TODO: Add gear gain

        // Take damage

        Completed = true;
        Active = false;
        RoomParent.Completed = true;
        RoomParent.Active = false;
    }

    public override void LoadContent()
    {
        base.LoadContent();
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Draw the base panel
        base.Draw(spriteBatch, gameTime);

        spriteBatch.DrawString(Font, Text, new Vector2(40, 40), Color.AntiqueWhite);

        GoldButton.Draw(spriteBatch, gameTime);
        GearButton.Draw(spriteBatch, gameTime);
        BothButton.Draw(spriteBatch, gameTime);
    }
}