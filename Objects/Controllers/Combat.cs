namespace superautodungeon.Objects.Controllers;

using superautodungeon.Objects.Heroes;
using superautodungeon.Objects.Enemies;

public class Combat
{
    // For now this will only fight a hero and an enemy
    // It will eventually take in a party and a mob
    public Hero hero;
    public Enemy enemy;

    public Combat(Hero inputHero, Enemy inputEnemy)
    {
        hero = inputHero;
        enemy = inputEnemy;
    }

    public void FightOneStep()
    {
        hero.HP -= enemy.Attack;
        enemy.HP -= hero.Attack;
        if (enemy.HP <= 0)
        {
            enemy.Die();
        }
        if (hero.HP <= 0)
        {
            hero.Die();
        }
    }
}