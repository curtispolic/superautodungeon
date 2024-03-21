# Super Auto Dungeon
Inspired by Super Auto Pets, I plan to create a dungeon-delving auto battler.

Author: Curtis Polic

## Current features:
- Basic Heroes vs Enemies combat
- Main Menu with half of the buttons working
- Base UI for gameplay
- 2 hero classes
- Shop
- Levels, rooms, filled combats
- Combat steps for backline units
- Feature complete mouseover and descriptions
- Animations for various updates
- Party reordering

## To Do in Rough Order:
- Add 2 or 3 more enemies (goblin, dark wizard, ogre)
- Selling units
- Levelling up
- Add a boss to the level
- Fog of war for the level
- Expand To Do list

## Known Bugs:
- Combat crashes when player party is defeated
- Mouseover panels can get stuck on the screen during a transistion
- Mouseovers broken in shop and UI

## Notes:

Map is going to be based on a Darkest Dungeon esque traversal of a map, moving through towards a known boss at the end of a floor. 
Going to have the following options as map tiles:
- Combat
- Event
- Fountain (heals the party)
- Shop (single use only)
- Treasure (will require key)
- Boss (to progress to next floor)

Class ideas with their passive:
- Scout (can let you preview a combat)
- Paladin (heals when he melee attacks)
- Warlock
- Necromancer (high tier)

Boss idea to fight another player who reached that boss on the same level. Something like that to make there be a little bit of PVP.

Consider a typing system to make diversity important, like the OSRS combat triangle or a Pokemon system.

Implement adventuring party naming system like Super Auto Pets.

Roguelite persistent upgrades between runs.

Equipment will also have levels like heroes will. This will likely require an inventory space built in for the party.

Use the layer float parameter of draw rather that insisting its all done in order.
EDIT: Somewhat too far gone to complete this task, but will keep in mind.