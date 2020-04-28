# Primitive Awakeness
## User's Possibilities
User has an ability to click anywhere on the surface using left mouse button. At the click's point spawns a random primitive figure with random color which changes after some amount of time, set in GameData file, into another random color. If user clicks on spawned figure it changes its color depending on clicks count made on this figure. Each figure has several data files in which stored settings for clicks diapason and a color to which figure has to change when this clicks diapason was reached. 
User clicks have no impact on figure's change color timer so figure will change it's color even if that had been already changed because of user clicks.

## Used components
- Default Unity's 3D models used as a primitive figures: cube, sphere, capsule, cylinder
- Asset Bundles used for prefabs storing and loading at app start
- ScriptableObjects used for storing information about each figure, it's color datas and for storing timer preferences (GameData file)
- UniRx used for figure's change-to-random-color timer
- Json object used for storing prefabs names

## Project structure
You can find working build for Windows in the Build directory. All source files are under Source/Primitive Awakeness directory.
All figures GeometryObjectDatas and GameData file are stored in Assets/Resources folder and all figures ClickColorDatas are stored in Assets/Resources/ClickColorData folder.

## Screenshots

![](https://github.com/IDmikael/Primitive-Awakeness/blob/master/Screenshots/hq-UIyiRvO8.jpg) 

![](https://github.com/IDmikael/Primitive-Awakeness/blob/master/Screenshots/xES7qwUsu9w.jpg)
