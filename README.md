# LK

Unity 2D heroic-fantasy adventure game demo.

My first attempt at making a 2D game demo with the Unity engine, my goal is to acheive a small but satisfying action oriented first level before expanding toward a more complexe metroidvania game style.

Feel free to contribute in any way you wish!

<img src="https://motheblackcat.github.io/assets/img/game.gif">

You can test the current build here: https://motheblackcat.github.io

Upcoming Features / Improvements:

- Fancy Start Menu
- Options menu
- Fancy stage start (tbd)
- Secondary weapon system remake:
    * Get prefab
    * UI for sweapon with selectable sweapon from keys / menu
    * Make animations for Sweapons on impact depending on type of weapon / col?

Known Bugs / Things to check:

- Jump animation from anystate prevent attack animation from being fully played during a jump
- Attack sound played with no animation when spamming (check if attack is registered)
- Warning on animation param "watch" on npcs
- Check audio logic for enemies "Destroy (gameObject, source.clip.length)"
- Check global audio logic (is the implementation of an audio manager worthwhile?)
- The fixed duration for attack and throw should be set according to the animation clip length
- Character state is not kept between scenes (life)
- Damage boost on enemy (jump while taking damage)