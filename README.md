# LK

Unity 2D heroic-fantasy adventure game demo.

My first attempt at making a 2D game demo with the Unity engine, my goal is to acheive a small but satisfying action oriented first level before expanding toward a more complexe metroidvania game style.

Feel free to contribute in any way you wish!

<img src="https://motheblackcat.github.io/assets/img/game.gif">

You can test the current build here: https://motheblackcat.github.io

What was done:

- Dynamic SWeapons UI
- Dynamic selection of Sweapons (reflected on player and ui)
- Switched to new 2D Lights

Upcoming Features / Improvements:

- Overhaul controls with new input system?
- Fancy Start Menu
- Options menu
- Inventory / Info Menu
- Secondary weapon system remake:
  - Make animations for Sweapons on impact depending on type of weapon / col?
  - Add air throw animation
  - Should SWeapons usage be limited? (Magic?)
- Bigger sword?
- Multiple Swords / Armors (And other equipments speed boots, double jump necklace, etc)
- Instantiate enemy cadavers for easier control / death animations
- Use Tilemaps

Known Bugs / Things to check:

- Reintroduce jump attack animation
- Attack sound played with no animation when spamming (check if attack is registered)
- Warning on animation param "watch" on npcs
- Check audio logic for enemies "Destroy (gameObject, source.clip.length)"
- Check global audio logic (is the implementation of an audio manager worthwhile?)
- The fixed duration for attack and throw should be set according to the animation clip length?
- Character state is not kept between scenes (life, sWeapon)
- Damage boost on enemy (jump while taking damage)
- Hide UI during scene tansition?
- Sweapons have no "life timer" (dagger)
- Sweapons call in Awake() at each instance to check
- Rain follows cam (move origin instead of transform?)
- Rain impact on ground

Notes on Sweapons and possible ineritance usage:

- Sweapons parent class properties shared with child classes such as Axe or Dagger (one script per parent / child class).
- There could be more properties added (onHit animation clip, particule trail, etc) the single manager script will be bloated.
