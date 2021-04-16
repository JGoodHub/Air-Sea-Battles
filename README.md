# Air-Sea-Battles

Recreation of the popular Betari game, Air Sea Battles. Play as a an anti air cannon as you try to shoot down increasingly difficult types of enemies to keep the skies safe.

## Inner workings

One of the key challenges posed by the specification was to avoid the creation of objects at runtime. To overcome this all objects in the scene that are dynamic (appear and disappear, e.g. bullets) exist in pools that are 'awoken' and 'slept' as the player needs them to keep resource usage low. Pools are references by a string ID where the result is then cached after the first call to keep pool access in constant time for subsequent fetches.

All enemies are placed in the generic pool "Aircraft" which allows for the quick creation of levels by just swapping out the contents of the pool. The game features three stages where the player must battle, planes, helicopters and fighter jets. Each enemy can be designed with a custom flight pattern across the screen using animation curves to make it more or less difficult to shoot them down, e.g. The helicopters start stop behaviour.

To account for the need to support multiple aspect rations some game elements are dynamically placed using a relative to screen size coordinates system at the start of the game. This ensures that no matter the screen shape the turret will always be at ScreenWidth/4 and the planes will always start off-screen and finish off-screen without going unnecessary far.

## Controls

In the main menu stages can be cycled using the Left and Right arrows keys, use Space to launch the currently selected stage.

In game the angle of the turret is controlled by holding the Up arrow key to aim at a 30º angle and Down arrow key to aim at a 90º angle. Holding nothing will return the turret to 60º angle.

THe escape key can be used to exit a level early.

All keys are set in the Input Manager so can be changed easily within the editor is needed.

## Extra Assets

A few additional assets were included beyond the resource pack provided, the helicopter enemy, explosion and background were created by myself in Pixlr. 
The font is AtartClassic found here: https://www.fontspace.com/atari-classic-font-f30342
The music was provided by Abstraction for free from here: https://tallbeard.itch.io/music-loop-bundle

## Timer Taken

Time spent on the projects equals roughly 16 hours of work