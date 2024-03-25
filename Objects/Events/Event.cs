namespace superautodungeon.Objects.Events;

public class Event
{
    public MainGame GameParent;
    public bool Active, Completed;

    public Event(MainGame inputParent)
    {
        GameParent = inputParent;
        Completed = false;
        Active = false;
    }
}