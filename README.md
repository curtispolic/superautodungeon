# Super Auto Dungeon
Inspired by Super Auto Pets, I plan to create a dungeon-delving auto battler.

Author: Curtis Polic

Current features:
- Basic Heroes vs Enemies combat
- Main Menu with half of the buttons working
- Base UI for gameplay
- 2 hero classes with no features
- Shop (this task was herculean)

To Do in Rough Order:
- Add levels
- Add map and map movement
- Refine main menu graphics
- Add settings menu
- Add multiple enemy types
- Expand To Do list

Known Bugs:
- First hero in the shop does not purchase correcly

Notes:

Map is going to be based on a Darkest Dungeon esque traversal of a map, moving through towards a known boss at the end of a floor. 
Going to have the following options as map tiles:
- Combat
- Event
- Fountain (heals the party)
- Shop (single use only)
- Treasure (will require key)
- Boss (to progress to next floor)

Class ideas with their passive:
- Knight (takes a reduced amount of damage)
- Scout (can let you preview a combat)
- Priest (does good healing)
- Paladin (heals when he melee attacks)
- Ranger (attacks from the backline)
- Mage (magic attacks from afar)
- Warlock
- Necromancer (high tier)

Boss idea to fight another player who reached that boss on the same level. Something like that to make there be a little bit of PVP.

Consider a typing system to make diversity important, like the OSRS combat triangle or a Pokemon system.

Implement adventuring party naming system like Super Auto Pets.

Roguelite persistent upgrades between runs.

Equipment will also have levels like heroes will. This will likely require an inventory space built in for the party.

Position is a bad thinking variable for everything, since it is all going to be drawn in multiple places. Refactor to get rid of 
position as an attribute for objects. 
EDIT: Okay not everything, buttons rely on it for their handling now but stuff like combats do not need a position. 
EDIT2: Okay I think heroes will also require a position for drag handling but itll be okay. 
EDIT3: somewhat through removing position

Use the layer float parameter of draw rather that insisting its all done in order.