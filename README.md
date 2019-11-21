# LK

Unity 2D heroic-fantasy adventure game demo.

My first attempt at making a 2D game demo with the Unity engine, my goal is to acheive a small but satisfying action oriented first level before expanding toward a more complexe metroidvania game style.

Feel free to contribute in any way you wish!

<img src="https://motheblackcat.github.io/assets/img/game.gif">

You can test the current build in the releases.

Latest additions v0.0.2:

- Character's life is kept between scenes

Upcoming Features / Improvements:

- Use Tilemaps
- Overhaul controls with new input system
- Rain impact on ground
- Fancier Start Menu (animated cursor, option choices and select action)
- Inventory / Info Menu
- Reintroduce jump attack animation
- Add animations for Sweapons on impact
- Add air throw animation
- Multiple Swords / Armors
- Options Menu

Known Issues:

- Player can't move after a dialog outside of the intro
- Player can attack while using an Sweapon
- Attack sound played with no animation when spamming (check if attack is registered)
- Damage boost on enemy (jump while taking damage)

TBD:

- Add keys to Sweapon UI (currently only arrows)
- Add extra equipments such as speed boots, double jump necklace, etc
- Should SWeapons usage be limited (Magic? Ammo?)
- Different impact animations depending on type of weapon / target
- Instantiate enemy corpses for easier control / death animations (simple animated sprite)
- Bigger sword to fix attack clunky feel
- Reduce life bar and daggers size in UI (temp UI)
- Hide UI during scene tansitions
- The fixed duration for attack and throw should be set according to the animation clip length
- Rain follows cam (move origin instead of transform or make it global)
- Global audio logic (is the implementation of an audio manager worth)

TBC:

- Check enemy death mecanics as it sometimes seems delayed
- Make auto start dialogs per npc instead of global from the UI (from player npc detection?)
- Check audio logic for enemies ex: "Destroy (gameObject, source.clip.length)"
- Refactor player attack (add more control while making it simpler)
- Warning on animation param "watch" on npcs
