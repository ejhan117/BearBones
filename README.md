# BearBones
For this project, I implemented touch functionality, animation, collision/physics, and a UI.
Touch input can be seen and used as soon as the user gets loaded into the game as the user can use joysticks controlled by touch to move the player (bear).
Animations can be seen with the bear's running animation while moving. The bear goes back into idle when the bear stops moving.
Collision and physics can be seen in the application in multiple ways. First, when the player collides with a skeleton, the user takes damage and the skeletons get pushed back. Secondly, when an attack launched by the player collides with a skeleton, both the projectile and enemy get destroyed.
The UI can be seen in multiple ways as well. First, there are multiple bits of information on the screen throughout the game loop, including health and time. There is also an upgrade system that has a  UI whenever the player completes a 2 minute wave.

The game ends after the player's health reaches zero or the player survives five waaves of enemies. The enemies progressively spawn faster as the waves go on and the player gets stronger as the waves go on.
