namespace superautodungeon.Objects.Heroes;

using System;
using Microsoft.Xna.Framework.Graphics;

public class Wizard : Hero
{
    public Wizard(MainGame inputParent): base(inputParent, true)
    {
        Random random  = new();
        Gender = random.Next(2) == 1 ? "Male" : "Female";
        Description = "A wise old wizard. Foes will tremble before their magical prowess.";
        Class = "Wizard";
        Name = Gender == "Male" ? GameParent.nameGenerator.CreateMaleName() : GameParent.nameGenerator.CreateFemaleName();
        MaxHP = 6;
        CurrentHP = 6;
        Attack = 1;
        LoadContent();
    }

    public override void LoadContent()
    {
        Texture = GameParent.Content.Load<Texture2D>("wizard");
        base.LoadContent();
    }

    public override int CombatStep()
    {
        // Combat steps will return the time in milliseconds the animation will require to play.
        if (Mana > 2)
        {
            Fireball();
            Mana -= 2;
            // placeholder animation values
            return 10;
        }
        else
        {
            Mana++;
            return 5;
        }
    }

    public void Fireball()
    {
        // Fireball targets a random enemy and does 2/4/6 damage, and half that to adjacent targets
        var enemyMob = GameParent.combat.EnemyMob;
        Random random = new();
        int target = random.Next(enemyMob.EnemyList.Count);

        // Just two for now until levelling is implemeneted
        enemyMob.EnemyList[target].CurrentHP -= 2;

        // Make sure we don't go out of range
        if (target > 0)
            enemyMob.EnemyList[target-1].CurrentHP -= 1;
        if (target < enemyMob.EnemyList.Count - 1)
            enemyMob.EnemyList[target+1].CurrentHP -= 1;
    }
}