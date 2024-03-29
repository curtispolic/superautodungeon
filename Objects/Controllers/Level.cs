using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using superautodungeon.Objects.Rooms;

namespace superautodungeon.Objects.Controllers;

public class Level
{
    public MainGame GameParent;
    public Room[,] RoomGrid;
    public bool Active;

    public Level()
    {
        Active = false;
    }

    public Level(MainGame inputParent)
    {
        GameParent = inputParent;

        // Create 5x5 level
        RoomGrid = new Room [5,5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                RoomGrid[i,j] = new Room(this, i, j);
            }
        }

        Random random = new();

        // Make 5 combat rooms
        int combats = 0;
        while (combats < 5)
        {
            int x = random.Next(5);
            int y = random.Next(5);
            if (RoomGrid[x,y] is not CombatRoom)
            {
                RoomGrid[x,y] = new CombatRoom(this, x, y);
                combats++;
            }
        }

        // Make a shop
        bool shop = false;
        Room shopRoom = new();
        while (!shop)
        {
            int x = random.Next(5);
            int y = random.Next(5);
            if (RoomGrid[x,y] is not CombatRoom)
            {
                RoomGrid[x,y] = new ShopRoom(this, x, y);
                shopRoom = RoomGrid[x,y];
                shopRoom.Visible = true;
                RevealAroundRoom(x,y);
                shop = true;
            }
        }

        int events = 0;
        while (events < 3)
        {
            int x = random.Next(5);
            int y = random.Next(5);
            if (RoomGrid[x,y] is not CombatRoom && RoomGrid[x,y] is not ShopRoom)
            {
                RoomGrid[x,y] = new EventRoom(this, x, y);
                events++;
            }
        }

        // Start the player in the shop
        Active = true;
        shopRoom.Enter();
    }

    public virtual void Update(GraphicsDeviceManager graphics, GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        foreach (var room in RoomGrid)
        {
            if (room.Update(mouseState, graphics, gameTime))
                AttemptMoveRoom(room.X, room.Y);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Draw the rooms
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (RoomGrid[i,j].Visible)
                    RoomGrid[i,j].Draw(spriteBatch, gameTime);
            }
        }
    }

    public void RevealAroundRoom(int x, int y)
    {
        if (x > 0)
        {
            RoomGrid[x-1,y].Visible = true;
        }
        if (x < 4)
        {
            RoomGrid[x+1,y].Visible = true;
        }
        if (y > 0)
        {
            RoomGrid[x,y-1].Visible = true;
        }
        if (y < 4)
        {
            RoomGrid[x,y+1].Visible = true;
        }
    }

    public void AttemptMoveRoom(int x, int y)
    {
        if (x > 0)
        {
            if (RoomGrid[x-1,y].ContainsPlayer)
            {
                RoomGrid[x-1,y].ContainsPlayer = false;
                RevealAroundRoom(x,y);
                RoomGrid[x,y].Enter();
            }
        }
        if (x < 4)
        {
            if (RoomGrid[x+1,y].ContainsPlayer)
            {
                RoomGrid[x+1,y].ContainsPlayer = false;
                RevealAroundRoom(x,y);
                RoomGrid[x,y].Enter();
            }
        }
        if (y > 0)
        {
            if (RoomGrid[x,y-1].ContainsPlayer)
            {
                RoomGrid[x,y-1].ContainsPlayer = false;
                RevealAroundRoom(x,y);
                RoomGrid[x,y].Enter();
            }
        }
        if (y < 4)
        {
            if (RoomGrid[x,y+1].ContainsPlayer)
            {
                RoomGrid[x,y+1].ContainsPlayer = false;
                RevealAroundRoom(x,y);
                RoomGrid[x,y].Enter();
            }
        }
    }
}