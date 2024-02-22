# CSE3902 Team 6 - The Syntax Sorcerers

This is an awesome project and team!

-William Comer
### Documentation
A Trello board with the current task progress of the project can be found here: https://trello.com/invite/b/Ar5hStpb/ATTI3a6cf350ac3809fc8288e45a7a2d2e5bF2F4E672/cse3902-group-6

Much of the documentation for this project is saved in the form of GitHub pull requests. Other documentation, including code metrics, is kept below and in the "Docs" folder.

#### Code Review Locations - Sprint 4

William Comer: Maintainability PR #44, Readability PR #47

Ben Horstman: Maintainability PR #38, Readability PR #40

Jacob Uligian: Maintainability PR #47, Readability PR #42

Calvin Milush: Maintainability PR #43, Readability PR #46

Tyler Milush: Maintainability PR #47, Readability PR #45

#### Code Review Locations - Sprint 5

Ben Horstman: Maintainability PR #51, Readability PR #54

Wiliam Comer: Maintainability PR #54, Readability PR #48

Jacob Uligian: Maintainability PR #50, Readability PR #49

Calvin Milush: Maintainability PR #, Readability PR #56

Tyler Milush: Maintainability PR #, Readability PR #55

# Design Decisions
C# naturally generates a private backing variable for public properties, so such backing variables are not explicitly defined if the property has only standard get-set behavior.

Spritesheets are divided by row and column and indexed beginning at 1.

## Naming Conventions/Level Loading Object Creation
We elected to go with the name of the object in the code as the name required in the level loading files. This allows us to be very specific about what objects are going to be created
as well as handle which GameObject manager handles them. To support this, All Item classes are formated [Name of Item]Item, Enemies -> [Name of Enemy]Enemy, EnviromentBlocks -> [Name of Block]Block

Rooms that are constructed currently have the shape of 16 across, 10 down. THIS IS NOT A LIMIT ON THE SIZE OF A ROOM, just how many can be displayed at one with our current set up.
Our current room loading code has no restrictions on the size of a room and it will not fail to assign correct relative locations. However, a user should be careful
using very large room sizes because we convert the csv file into a 2D array in an intermediate step and this could become an expensive operation.

In Excel, this translates to A-P, 1-10. If creating in Excel and desiring an Empty space the keyword "[EMPTY]" should be used to ensure the empty spaces aren't discarded when creating the csv.

## GameObjectManagers Design Decision
The management of game objects were moved into a global static GameObjectMangers class that allows access to specific lists of game objects (ie enemies, enemyProjectiles, etc).
This was done to simplify the process of accesssing GameObjects by all there various updaters without passing around a reference to main. This also allowed for a type generic implementation
of GameObjectManager<Type> so that it will be easy to add additional managers in the future if desired.

## Player
### Keyboard Controls
The player can be moved up, down, left, and right using the W, S, A, and D keys respectively (or the corresponding arrow key). Pressing Z or N will have the player use their sword; E will cause them to take damage from the left. Pressing 1 will cause the player to use their secondary item. The arrow keys can be used to control Navi; press semicolon to take control of a nearby enemy.

The Q button may be pressed at any time to quit the game; the R button can be pressed to reset the game to its original state, restoring initial variables such as Link's health.

In the "Game Over" screen, press Up/Down to select an option and Enter to execute it.

### GamePad Controls
The player may be controlled using the left stick on a standard Xbox controller. Press A to use the sword and B to use the secondary item. Move Navi around with the right stick and control enemies with X. 

On the D-Pad, Up mutes the game, Left brings you to the character select screen, Right resets the game, and Down quits the game. Press Start to pause/unpause the game.

#### Special Inputs
Special moves can be accessed by performing a special input and then pressing the space bar. Press each button, one at a time, in short succession to perform the associated special move.
* W, D, S, A : Spawn Navi
* A, S, D : Hadoken
* W, W, S, S, A, D, A, D : Annihilation

These special moves are also linked to keys that do not appear on a standard English keyboard, but may appear on other keyboards, due to the design of the Command/Controller structure. Pressing those keys would also cause the associated special move to be performed.

On controller, the special move button is Y. Perform combos by moving the left stick in the associated direction with the keys listed above.

### Design Choices
There is known high coupling between the Player and AbstractPlayerState classes. This is due to the fact that having two separate classes for one set of functions is unnecessary; however, the classes are kept separate to distinguish between things that a player "has" and things a player "does". As a general rule, things "had" by the player (such as items, position, and health) should be kept in Player while things "done" by the player (moving, using items) should be kept in the Player States.

The player sprites are intentionally divided into rows by which direction the player is facing and columns by what the player is doing. Any magic numbers related to player sprite frame identification are considered to be named by virtue of this system.

The player sprite intentionally does not revert to a "neutral" state when the player is not moving, instead staying on the last idle sprite - this is true to the original game.

If multiple movement keys are held down, the player will move in the direction of the key whose holding began most recently. If one movement key is released and multiple movement keys are still being held, one of the remaining keys will be arbitrarily selected as having begun most recently.

Projectiles are managed via a singleton that the Player States know about. Some condition checking is excluded from the projectile manager under the assumption that methods may only be called from the correct states, i.e., such condition checking is handled in the state pattern.

Default variables that do not depend on constructor parameters are still initialized in the constructor (as opposed to at declaration time) to distinguish them as default and to avoid confusion with constants.

Player death does not currently end the game or cause it to reset for testing purposes. As a result, the player has an unusually long invincible time to prevent becoming stuck in a corner by an enemy.
## Projectiles
Projectiles the player uses will handle creating an explosion effect inside the projectile class that would cause it. This helps simplify the projectile manager.

Link's bomb does not damage the player; this is reflective of the original game. 

Projectiles that experience collision must have a StruckSomething property that is activated upon collision. This property is not used by all projectiles, but it is still in IPlayerProjectile to abstract some collision response away from the collision detection manager and reduce cyclomatic complexity.
## Sprites
All sprite drawing was done in a more generic way, without having to create a class for each individual sprite. This was done to allow greater performance, as changing sprites can be handled within an enemy without requiring an outside class, and secondly to reduce clutter. One generic sprite class can be applied to all sprites,
so there is much less content to worry about.

A sprite "factory" is used, though in a slightly different way, as all factory methods access a singleton that returns any individual spritesheet that in needed by a certain object.

Since all objects carry their whole spritesheet, magic numbers appear often, as the object must know about what sprite needs to be active given a certain state. For example, the goriya will know that each row in the sheet is a facing direction, and so the correct sprite can be assigned based on the direction stored in the enemy class.

## Enemies
Projectiles created by enemies and drawn and updated within that enemy class. Though this does increase coupling with the enemy, it also reduces excess coupling to the game class
and allows these projectiles to be managed by the enemy that creates them without need for the game to know about them at all.

The item drop code in the original game is very complex, but some details carry to this recreation. The amount of items that drop should be the same as the regular game, but items are randomized rather than based on a kill counter.

The Dodongo does not have the functionality to eat bombs, but will retain its reaction to getting hit by a bomb.

## Collision
Collision is handled via a series of singletons that have access to the managers of each object type. Due to the amount of subdivision, collision response (object's response to a type of collision) is by default handled in the same class as collision detection. If an object has the same collision response to two or more other object types, the common object may hold the collision response code. (e.g. Player reacts the same way whether it takes damage from an enemy or an enemy projectile; this functionality is thus kept in the appropriate Player class.)

Collision is primarily checked with rectangle overlap and objects' moving direction; the ICollidable interface reflects that. Common functionality has been factored out into the CollisionHelpers class.

## Sound
Where possible, sound effects are created by factory and played by the classes that would make them (e.g., an enemy would make its death cry so PlayEnemyDie is called in the Enemy class instead of a collision manager).

## HUD
Pausing the game to get to the pause screen is done using 'P'. A lot of magic numbers were used for positioning since values for them weren't repeated and original game positions appeared arbitrary.
To choose between the Bow & Arrow, Boomerang, or Bomb you can press 'A' or LeftArrow to go left and 'D' or RightArrow to go right while on the pause screen.

Press "C" to enter a character select screen, allowing you to play as a Goriya or a Stalfos.

## Sprint 5
Features that were added in Sprint 5 include Dungeon 2, Gamepad/controller input support, Player 2/Co-op via Navi, character selection, and special moves.
