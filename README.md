# LK

Unity 2D heroic-fantasy adventure game demo.

My first attempt at making a 2D game demo with the Unity engine, my goal is to acheive a small but satisfying action oriented first level before expanding toward a more complexe metroidvania game style.

Feel free to contribute in any way you wish!

<img src="https://motheblackcat.github.io/assets/img/game.gif">

You can test the current build here: https://motheblackcat.github.io

What was done:

- Dynamic SWeapon UI

Upcoming Features / Improvements:

- Fancy Start Menu
- Options menu
- Fancy stage start (tbd)
- Inventory / Info Menu
- Secondary weapon system remake:
  - Switch Sweapon with player controls and menu
  - Make animations for Sweapons on impact depending on type of weapon / col?
  - Add air throw animation
  - Should SWeapons usage be limited? (Magic?)
- Bigger sword?
- Change Sword and Armors (And other equipment like speed boots or double jump jewelry?)

Known Bugs / Things to check:

- Reintroduce jump attack specific animation
- Sounds levels needs to be adjusted (music too low, some sfx too high)
- Attack sound played with no animation when spamming (check if attack is registered)
- Warning on animation param "watch" on npcs
- Check audio logic for enemies "Destroy (gameObject, source.clip.length)"
- Check global audio logic (is the implementation of an audio manager worthwhile?)
- The fixed duration for attack and throw should be set according to the animation clip length
- Character state is not kept between scenes (life, sWeapon)
- Damage boost on enemy (jump while taking damage)
- Hide UI during scene tansition?

Notes on Sweapons:

Sweapons parent class properties that will be shared with its children classes (one script). 
Sweapons child classes such as Axe or Dagger inherits and set these properties from the parent class (one script per child class).
Currently one script manages both the parent and child classes and thus values are set at the clone instanciation in a switch.
Having one script per Sweapon while adding files should:
- Make the project structure cleaner.
- There could be more properties added like an onHit animation clip or particule trail, thus it won't bloat the single manager script.
- Not having to set properties on Awake() like it's currently the case could be more efficient.

The player's Sweapons array (representing the Sweapons currently available) should start empty and be filled either:
- From the assets / resources?
- From an array containing all the possible Sweapons (were would it rest, it will have to live through the game)?
In any way it would allow the player to cycle through only the earned Sweapons.
Also as making progammatical display of the available Sweapons in the inventory menu easier.
