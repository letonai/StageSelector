# StageSelector
A simple Unity stage selector for games by drag and drop scenes.

.StageManager.cs:
  Creates and manages buttona to load scenes on a cavas layout.  

.ButtonConfig
  Keeps some scene configurations to load
  
The script will create a grid of buttons for each scene que is dragged and dropped on inspector.

This scripts validates if the player have enough stars to load that scene (see comments to more details).

How to use, based on the commited project:

1. Open on Unity:5.3.5f1
2. Click on Canvas on the hierarchy
3. On Stage Manager(Inspector Panel) drag and drop the prefab button from directory UI to "Prefab Stage Button" field (on inspector)
4. Drag and drop "Stages" panel, which is inside the canvas, to "Grid" field (on inspector)
5. On Stage List, select a number of stage you need.
6. Drad and Drop scenes from your project to each new created field
7. On Stage Label, type the title that you want to be displayed on the screen, keep blank to display the scene number
8. On Unlock With Stars, number of stars needed to be the player load that stage.(You must to implement a method to keep track of stars, see line 132 on StageManager.cs)
8. Play, for each added scene you should have a button.
9. Have fun.



By Letonai
