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

Rather than having a boolean for each element being visible, make it be contained in the object. If it is made but a default 
no parameter constructor, the bool will be false and dont worry about the rest of the object. If it is made for real, it'll be
true so it will be visible.

Position is a bad thinking variable for everything, since it is all going to be drawn in multiple places. Refactor to get rid of 
position as an attribute for objects.