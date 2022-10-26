# push-puzzle

Push Puzzle is a 2D grid based puzzle game which requires you to push things out of your way to make it to the goal.

The assets used by this game (including images and the levels themselves) are stored in `assets` and are all fully customisable by anyone. Yes, that includes you!

The existing `assets` folder contains complete examples of assets which are ready to be edited and this document contains the information required to customise them fully.

## Levels
Levels are saved in `assets/levels` and should be named `<level_number>.lvl`. The game will look for and load `1.lvl` first, followed by `2.lvl`, followed by `3.lvl`, and so on.

There is no intended limit to the number of levels the game has, but maybe try to keep it below 2147483647.

### .lvl
The first line of a .lvl file defines the dimensions of the level. This should be of the form `<wdith>x<height>` e.g. `10x12` means the level is 10 squares in width and 12 squares in height.

The second line of a .lvl file is the name of the background image which should be used by the level. The file extension (e.g. `.png`) should not be included. The background image itself should be stored in `assets/sprites/backgrounds` as a .png.

The remaining lines of a .lvl file are comma-seperated values of characters (letters and numbers) representing the squares the level is made up of.
The number of lines and the number of values per line in this section must match the height and width defined in the first line of the file respectively. The characters which can be used and the parts of the level they represent are:

- `P` - Player.
- `W` - Wall. Cannot be moved and stops other things from moving.
- `p` - Pushable. Can be moved around the level.
- `G` - Goal. Must be reached by the player to finish the level.
- `u` `r` `d` `l`- Push (facing up, right, down, or left respectively). Moves anything on top of it one square in the direction it is facing.
- `^` `>` `V` `<` - Throw (facing up, right, down, or left respectively). Moves anything on top of it as many squares as possible in the direction it is facing until something gets in the way.

## Sprites
Sprites which can be animated should be saved in `assets/sprites/<thing>/<state>/<direction>`, where:

- `<thing>` is what uses these sprites (e.g. `player`).
- `<state>` is the state the thing is in when using these sprites (e.g. `idle`).
- `<direction>` is the direction the thing is facing when using these sprites (e.g. `up`). Not all things have a direction e.g. goal.

An animation for each collection of sprites will be constructed by the game e.g. the sprites in `assets/sprites/player/idle/up` will be used to make the player's up-facing idle animation. Sprites should be named `<frame_number>.png`, where `<frame_number>` starts from `1` e.g. the first frame of the animation is named `1.png`, the second frame is named `2.png`, and so on.

Backgrounds and UI elements are not animated and should be saved in `assets/sprites/backgrounds` and `assets/sprites/ui` respectively.

### animation.properties
An `animation.properties` file must be included in each sprite folder. This file should contain the properties which tell the game how to use the sprites. Properties should be set one per line with no spaces. If a property is not set it will use a default value defined by the game. The properties which should be included in this file are:

- `numberOfFrames` - The number of frames this animation has. Must be an integer. Default is `0`.
- `speed` - The number of seconds between frames. Must be a decimal number. Default is `0`.
- `repeat` - Determines if the animation should loop or play once. Must be either `true` or `false`. Default is `false` (will not repeat/loop).
- `resetToFirstFrame` - Determines if the animation should play from the first frame or from whichever frame the previous animation was on. Must be either `true` or `false`. Default is `false` (will not reset to first frame).

Sprites for background and UI elements do not require an `animation.properties` file as they are not animated.
