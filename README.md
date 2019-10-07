# LK

Unity 2D heroic-fantasy adventure game demo.

My first attempt at making a 2D game demo with the Unity engine, my goal is to acheive a small but satisfying action oriented first level before expanding toward a more complexe metroidvania game style.

Feel free to contribute in any way you wish!

<img src="https://motheblackcat.github.io/assets/img/game.gif">

You can test the current build in the releases.

Latest additions v1.0.1:

- Character state is kept between scenes (life, sWeapon) (Alpha)

Upcoming Features / Improvements:

- Use Tilemaps
- Overhaul controls with new input system
- Rain impact on ground
- Fancier Start Menu (animated cursor, option choices, selection action)
- Inventory / Info Menu
- Reintroduce jump attack animation
- Add animations for Sweapons on impact
- Add air throw animation
- Multiple Swords / Armors
- Options Menu

Known Issues:

- Remove duplicated instances of kept gameobjects (player, player UI)
- Player can attack while using an Sweapon
- Attack sound played with no animation when spamming (check if attack is registered)
- Damage boost on enemy (jump while taking damage)

Other notes and things to check or test:

- Add extra equipments such as speed boots, double jump necklace, etc?
- Should SWeapons usage be limited? (Magic? Ammo?)
- Different impact animations depending on type of weapon / target?
- Instantiate enemy corpses for easier control / death animations (simple animated sprite)?
- Check enemy death mecanics as it sometimes seems delayed
- Bigger sword to fix attack clunkiness?
- Reduce life bar and daggers size in UI
- Make auto start dialogs per npc instead of global from the UI (from player npc detection?)
- Hide UI during scene tansitions?
- The fixed duration for attack and throw should be set according to the animation clip length?
- Rain follows cam (move origin instead of transform? or make it global?)
- Check audio logic for enemies ex: "Destroy (gameObject, source.clip.length)"
- Check global audio logic (is the implementation of an audio manager worthwhile?)
- Refactor player attack (add more control while making it simpler)
- Warning on animation param "watch" on npcs
