namespace superautodungeon.Objects.Controllers;

using superautodungeon.Objects.Heroes;
using superautodungeon.Objects.Enemies;

public class Combat
{
    public Party PlayerParty;
    public Mob EnemyMob;
    public bool Ongoing;

    public Combat(Party inputParty, Mob inputMob)
    {
        PlayerParty = inputParty;
        EnemyMob = inputMob;
        Ongoing = true;
    }

    public void MeleeHit()
    {
        Hero frontHero = PlayerParty.HeroList[0];
        Enemy frontEnemy = EnemyMob.EnemyList[0];
        frontHero.HP -= frontEnemy.Attack;
        frontEnemy.HP -= frontHero.Attack;

        // Handle the animation
    }

    public void OnAttackTriggers()
    {
        return;
    }

    public void HandleDeath()
    {
        bool wasDeath;
        do
        {
            wasDeath = false;
            foreach (var hero in PlayerParty.HeroList)
            {
                if (hero.HP <= 0)
                {
                    if (hero.Die())
                    {
                        wasDeath = true;
                    }
                }
            }
            foreach (var enemy in EnemyMob.EnemyList)
            {
                if (enemy.HP <= 0)
                {
                    if (enemy.Die())
                    {
                        wasDeath = true;
                    }
                }
            }
        } while (wasDeath);
    }

    public void RoundFinish()
    {
        // Check if all heroes or all enemies are dead
        int heroCheck = 0; int enemyCheck = 0;
        foreach (var hero in PlayerParty.HeroList)
        {
            if (hero.Dead)
                heroCheck++;
        }
        foreach (var enemy in EnemyMob.EnemyList)
        {
            if (enemy.Dead)
                enemyCheck++;
        }
        if (enemyCheck == EnemyMob.EnemyList.Count || heroCheck == PlayerParty.HeroList.Count)
        {
            Ongoing = false;
        }
    }

    public void FightOneStep()
    {
        // Front two attack each other
        Hero frontHero = PlayerParty.HeroList[0];
        Enemy frontEnemy = EnemyMob.EnemyList[0];
        frontHero.HP -= frontEnemy.Attack;
        frontEnemy.HP -= frontHero.Attack;

        // Handle on attack triggers here
        
        // Death handling
        bool wasDeath = false;
        do
        {
            wasDeath = false;
            foreach (var hero in PlayerParty.HeroList)
            {
                if (hero.HP <= 0)
                {
                    if (hero.Die())
                    {
                        wasDeath = true;
                    }
                }
            }
            foreach (var enemy in EnemyMob.EnemyList)
            {
                if (enemy.HP <= 0)
                {
                    if (enemy.Die())
                    {
                        wasDeath = true;
                    }
                }
            }
        } while (wasDeath);

        if (PlayerParty.HeroList.Count == 0 || EnemyMob.EnemyList.Count == 0)
        {
            Ongoing = false;
        }
    }
}