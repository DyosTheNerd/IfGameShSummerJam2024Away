## Version 0.2.3. 2024-07-28 scoring cubes on collection
  - added scoring cubes that are spawned when a player collects a pickup

## Version 0.2.2. 2024-07-27 added ammo ui update
  - refactored weapons to Prefabs
  - Shooting now uses ammo and remaining ammo is shown on screen
    - known bug, only works for rockets correctly, because somehow high Value, should be 1000 for beam is not set.

## Version 0.2.1 2024-07-27 Merged the merge
  - Fixed some small bugs due to removed / refactored functionality
  - introduced a new button / action to switch weapons (Keyboard E, north buttons)
  - added initial ammo implementation

## Version 0.2 2024-07-24  (MERGED INPUT INTO MAIN)
  - Merged Input branch into main.

## Version 0.1.6 2024-07-24  (INPUT BRANCH)
  - Deactivated the Old Input Manager from the project.
  - Added Control Schemes for: 
    -- Switch Pro Controllers
    -- XBox Controllers
    -- Playstation Controllers
    -- Generic Gamepads (???)
    -- Keyboard Left (WASD)
    -- Keyboard Right (Arrows)

  - Refactored PlayerMovement and ShipToolManager to use the PlayerInput component.
  - Added a StartTheGame scene that does Binding of users and devices.
      --- Press Shoot on the devices you wanna use. (square, Y, X, shift keys)

  - PlayerManager class is responsible for managing Players over the duration of the play session.
   -- For example, keeping track of active players.

   -- Currently, it handles scene transition into game when lobby is full. (not ideal. but we dont have a game state controller yet) <<----

  - Started a PlayerData module to track Player information over time. Ideally we bind a "User" to a set of player parameters and devices.
  - Added Temp UI stuff just to get the StartTheGame scene.

  - Created a Placeholder ShipFactory class.

## Version 0.1.5 JDW 2024-07-22
- added simple scoring: when rocket hits, player shooting the rocket gets a point.

## Version 0.1.4 JDW 2024-07-22
- centralized some gameplay constants
- countdown now always formatted xy:zw

## Version 0.1.3 JDW 2024-07-21

- fixing asteroid singleton bug
- some ui styling and css file

## Version 0.1.2 AA 2024-07-18
- further refactoring.
    - CameraController
      -- made camera less dependant on manual configuration
      -- setting Radius and LookAt should reposition camera accordingly (but i would like opinions on the implementation)
      -- changed camera to an Orthographic projection.
      -- camera now has initialValues that can be set in the inspector.
    - Deleted the dummyPlayer.cs script (it was just to test the camera.)
    - Deleted the PickUpMovement.cs script. (it was 90% the player movement script with less stuff)
        --this included removing some references to it from several other scripts.
    - ShipToolManager
      -- made private all the private sub-vector calculations.
      -- now uses Asteroid.Instance.Center to compute some of the sub vectors.


- trying to fix some of the nausea issues
  - removed stars from background
  - added a polar axis to still give a sense of where up is. I can prettify later
  -- some of the camera changes were due to this also.



### Version 0.1.1 JDW 2024-07-17
- started with ui doc. 
  - added timer count down
  - added and wired up other ui elements in a controller
- fixed sfx singleton instance implementation


### Version 0.1 Post-Jam

- Changed Global Controllers into  Singletons.
    Access with the static member Instance.
    -- affect systems: 
        PointSystem
        Asteroid
        SoundEffectMaster
        PickUpManager

- CameraController now has a SetTarget method.
