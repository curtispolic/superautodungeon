using System.IO;

namespace superautodungeon.Objects.Controllers;

public class SaveHandler
{
    MainGame GameParent;
    readonly string filename = "savefile.sav";

    public SaveHandler(MainGame inputParent)
    {
        GameParent = inputParent;
    }

    public bool SaveGame()
    {
        string savetext = "";

        /* WIP Saving functionality
        savetext += inputParty.ConvertToSave();
        savetext += "|";
        savetext += inputLevel.ConvertToSave();
        */

        File.WriteAllText(filename, savetext);

        return true;
    }

    public bool LoadGame()
    {
        string savetext = File.ReadAllText(filename);

        GameParent.playerParty = LoadParty(savetext);
        GameParent.level = LoadLevel(savetext);

        return true;
    }

    public Party LoadParty(string savetext)
    {
        Party returnParty = new(GameParent);

        string methodtext = savetext[..savetext.IndexOf('|')];

        // returnParty.CreateFromSave(methodtext)

        return returnParty;
    }

    public Level LoadLevel(string savetext)
    {
        Level returnLevel = new(GameParent);

        string methodtext = savetext[savetext.IndexOf('|')..];

        // returnLevel.CreateFromSave(methodtext)

        return returnLevel;
    }
}