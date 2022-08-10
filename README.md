# ClimbRock3D
## What is ClimbRock3D?
An mobile game that developed with Unity3D.
You must reach top of the mountain to get to the next level while avoiding obstacles.

APK link:https://drive.google.com/file/d/11Ai2_X6S4nWqd2yfaa03-9x1XchJaYa4/view?usp=sharing

If you want to know about more for creating content with it you can read instructions.txt

## In game screenshots

![image](https://user-images.githubusercontent.com/44952253/183712163-2b4abcd3-dd38-4bef-ac29-9d5772ab4db6.png)

![image](https://user-images.githubusercontent.com/44952253/183712297-3868f771-b38f-44c5-9c0d-bace109a4b2e.png)


## Game flow diagram

![Başlıksız Diyagram drawio (2)](https://user-images.githubusercontent.com/44952253/183711446-9306e934-ed54-46c8-a4c0-69d44198ead6.png)

## User input diagram

![Başlıksız Diyagram drawio (1)](https://user-images.githubusercontent.com/44952253/183711542-c0e3df87-03cf-4ca0-a295-32dce85e3ec9.png)

## Class structure

![Başlıksız Diyagram drawio (4)](https://user-images.githubusercontent.com/44952253/183711322-e052d055-dbe3-4679-99b8-bf0e285ac507.png)

## instructions
```
you can edit player launch speed, linearSawSpeed, handlePositionGap by values.json file.
you need to create new value in json for every level. example
   
   {
      "obstacleLinearStartSpeed": "5",
      "handlePositionGap": "4",
      "climbSpeed": "1"
    }


linearSawSpeed= linear moving red saw speed
handle position gap= unused for now. it was created for porductive level generator.
climb speed= player launch speed to an handle to handle


**LEVEL CREATION**
you can duplicate level02 scene by CTRL+D to create a level. level02 has all handles and obstacles in it.
you can change positioning of nahdles&obstacles in Environment<HANDLES. All wall objects are here.
you can add new objects from Prefabs file. This file has all handles and Obstacle prefabs.
you can extend the level by changing Environment<TopGround y position in scene.  You should add more handles so player can reach finish handle.
ground has start position objects. if you want to adjust them its on Environment<Ground.
if you want to edit saw speeds by per object you must select object and in ObstacleMovement, set true SetSpeedOn value and set speed.

You should add your level to build scenes from build settings.

**OBSTACLE CREATION**
Add your obstacle a collider, and change tag to the "Obstacle". Thats it. You can add add movement to it with ObstacleMovement script.

**HANDLE CREATION**
Add your handle a collider and rigidbody with freezed position and location, and make it kinematic. Tag it "Handle".

*UI manager*
UI manager has fail events and win events, you can add new functions to it if you want to do more.

*Sound Manager*
if you want to add new sound, go to sound manager and add a new enum element into the Sounds enum objects. Then you need to reference audio sound from scene 1. You can call it from anywhere with instance by SoundManager.instance.Play(Sounds sound).


```
### used assets from assetstore

https://assetstore.unity.com/packages/2d/gui/icons/flat-icoon-ui-2d-puzzle-game-ui-69370
https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488
https://assetstore.unity.com/packages/3d/props/props-for-track-environment-lowpoly-free-211494
https://assetstore.unity.com/packages/audio/music/free-music-tracks-for-games-156413
https://assetstore.unity.com/packages/3d/characters/humanoids/banana-man-196830
https://assetstore.unity.com/packages/templates/packs/obstacle-course-pack-178169
https://assetstore.unity.com/packages/tools/camera/simple-cam-follow-164460
https://assetstore.unity.com/packages/audio/sound-fx/interface-and-item-sounds-89646
