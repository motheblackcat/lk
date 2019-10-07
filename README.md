# LK

Unity 2D heroic-fantasy adventure game demo.

My first attempt at making a 2D game demo with the Unity engine, my goal is to acheive a small but satisfying action oriented first level before expanding toward a more complexe metroidvania game style.

Feel free to contribute in any way you wish!

<img src="https://motheblackcat.github.io/assets/img/game.gif">

You can test the current build in the releases.

Latest additions v1.0.1:

- Character state is kept between scenes (life, sWeapon) (Alpha)

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

Known Issues:

- Remove duplacte instance of gameobjects (player)
- Player can attack while using an Sweapon
- Attack sound played with no animation when spamming (check if attack is registered)
- Damage boost on enemy (jump while taking damage)
- Reintroduce jump attack animation

Other Notes:

- Make auto start dialogs per npc instead of global from the UI
- Check scriptable objects for SWeapons
- Hide UI during scene tansition?
- The fixed duration for attack and throw should be set according to the animation clip length?
- Rain follows cam (move origin instead of transform?)
- Check audio logic for enemies "Destroy (gameObject, source.clip.length)"
- Check global audio logic (is the implementation of an audio manager worthwhile?)
- Sweapons call in Awake() at each instance to check
- Refactor player attack
- Warning on animation param "watch" on npcs
- Rain impact on ground
