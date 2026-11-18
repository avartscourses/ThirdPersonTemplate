# ThirdPersonTemplate — Unity Third Person Template

A simple 3D project developed with **Unity 6.3 LTS** for the *Videogame Design and Development* course.

The project demonstrates basic game programming concepts, third-person movement, animations, and interaction mechanisms.

## Play Online

[Play Third Person Template](https://avartscourses.github.io/ThirdPersonTemplate/)

**Controls**

- WASD — Move
- Mouse — Rotate the camera
- Left Shift — Run
- Space — Jump
- E — Interact with the door

## Download and Open the Unity Project

Option 1 Download ZIP

Click Code → Download ZIP.

Extract the downloaded file.

Option 2 Clone the repository

Copy the repository URL: [https://github.com/avartscourses/ThirdPersonTemplate.git](https://github.com/avartscourses/ThirdPersonTemplate.git)

Clone it using Git GUI, Git Bash, or GitHub Desktop.

Open the `UnityProject` folder through Unity Hub using **Unity 6.3 LTS**.

## Main Features

- Third Person movement, camera control, and character animations
- Walking, running, jumping, and respawning
- Raycast-based interaction with a door
- Trigger zones and automatic collection of objects
- Collectible items and inventory
- GameState for managing resources and inventory
- Player health and environmental hazards
- Door animation and sound effects
- HUD for gameplay feedback

## Using the Template

You can use this project as a starting point for your own game.

Explore the GameObjects and Components, examine the provided C# scripts, and adapt the movement, camera, and interaction mechanisms to suit your game concept.

The camera can be modified to support different perspectives, including fixed or top-down views. However, camera selection should take into account the size and layout of the environment, object visibility, and player movement.

The accompanying workshop notes explain the main programming concepts, the organization of the scripts, and how the components communicate. They also discuss raycasting, camera alternatives, and differences between First Person and Third Person implementations.

## Project Structure

- `docs/` Playable WebGL version, published through GitHub Pages
- `UnityProject/` — Editable Unity project and C# scripts

The WebGL version is intended for playing and testing. To modify the game, download and open the Unity project in the Unity Editor.
