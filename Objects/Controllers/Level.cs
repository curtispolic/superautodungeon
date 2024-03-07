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
        // Create 5x5 room
        RoomGrid = new Room [5,5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                RoomGrid[i,j] = new Room(this, i, j);
            }
        }

        // Put the player in a random room
        Random random = new();
        RoomGrid[random.Next(5), random.Next(5)].ContainsPlayer = true;

        GameParent = inputParent;
        Active = true;
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
                RoomGrid[i,j].Draw(spriteBatch, gameTime);
            }
        }
    }

    public void AttemptMoveRoom(int x, int y)
    {
        if (x > 0)
        {
            if (RoomGrid[x-1,y].ContainsPlayer)
            {
                RoomGrid[x-1,y].ContainsPlayer = false;
                RoomGrid[x,y].Enter();
            }
        }
        if (x < 4)
        {
            if (RoomGrid[x+1,y].ContainsPlayer)
            {
                RoomGrid[x+1,y].ContainsPlayer = false;
                RoomGrid[x,y].Enter();
            }
        }
        if (y > 0)
        {
            if (RoomGrid[x,y-1].ContainsPlayer)
            {
                RoomGrid[x,y-1].ContainsPlayer = false;
                RoomGrid[x,y].Enter();
            }
        }
        if (y < 4)
        {
            if (RoomGrid[x,y+1].ContainsPlayer)
            {
                RoomGrid[x,y+1].ContainsPlayer = false;
                RoomGrid[x,y].Enter();
            }
        }
    }
}