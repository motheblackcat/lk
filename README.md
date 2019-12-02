# LK

Unity 2D heroic-fantasy adventure game demo.

A sandbox/pet project to learn 2D in Unity, advancing slowly due to work and life ofc.

<img src="https://motheblackcat.github.io/assets/img/game.gif">

You can test the current build at https://motheblackcat.github.io/assets/build/lk.rar or in the releases.

Patch notes v0.0.2:

- New Feature: Player's life and sWeapons state kept between scenes
- WIP: Simple backtrack/scenes navigation
- WIP: New scene to get the Axe sub-weapon from Bob The Lumberjack
- Improvement: The jump button (space bar / A button) is now also used for dialogs opening
- Improvement: Player flicking when hurt made as an animation instead of programaticaly
- Improvement: Scene transitions reworked for more stability and easier expansion
- Fix: Player couldn't move after closing a dialog (Player move conditions refactored)

Currently Known Issues:

- Attack sound can be heard without the animation when spamming attack (attack is registered though)
- Jump sound can be heard without jumping when spamming it along using an Sweapon
- Damage boost on enemy (Bigger jump while taking damage)
