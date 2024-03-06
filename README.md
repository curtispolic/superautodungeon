# Super Auto Dungeon
Inspired by Super Auto Pets, I plan to create a dungeon-delving auto battler.

Author: Curtis Polic

Current features:
- Basic Heroes vs Enemies combat
- Main Menu with half of the buttons working
- Base UI for gameplay
- 2 hero classes with no features

To Do in Rough Order:
- Add shop
- Add levels
- Add map and map movement
- Refine main menu graphics
- Add settings menu
- Add multiple enemy types
- Expand To Do list

Notes:

Position is a bad thinking variable for everything, since it is all going to be drawn in multiple places. Refactor to get rid of 
position as an attribute for objects. EDIT: Okay not everything, buttons rely on it for their handling now but stuff like combats 
do not need a position. EDIT2: Okay I think heroes will also require a position for drag handling but itll be okay.