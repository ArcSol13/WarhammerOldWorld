# Warhammer-OldWorld

## Creating a module subpart

We can individualize parts of the mod for easier subdivision of labor. This will make it easier to add new features without stepping on each other's toes.

Create a class of type CustomSubModule.

Do your initialization in OnLoad, do your deinitialization in OnUnload.

Do updates/checks in OnUpdate, called every frame, avoid using costly operations.

## Adding a module subpart to the system

In-progress.
