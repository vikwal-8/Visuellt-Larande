How to import project?
Open Unity Hub and select “Add”. Choose the folder “Visuallt-Larande”.

Start the game.
To start the game, Open the Unity-project “Visuellt Lärande”.
The main scene is located in Assets/Scenes.

Add a new mini-game to be able to run the minigame within the main world, get back to the mainworld from the minigame and add coins to the main world?
Add the minigame-assets as a subfolder to the Unity-Project “Visuellt Lärande”.
If name conflict exists, where class-name conflicts with the class-names in the main-project the easiest way is to rename the class directly in for example Visual Studio.
Create a GameObject that represents the minigame.
Create a new C#-script and extend the script with the class “Minigame” and then attach the script to the GameObject.
Add the scene(s) for the minigame to the project with File->Build Settings->Add Open Scene.
Define the abstract string called scenename, a get-call should return the name of the scene that starts the minigame. Check the class StarShipCollider for an example. When starting the main world and moving the player to the GameObject you placed, you should now be able to start the minigame by pressing the spacebar.
To return to the mainworld within the minigame-scene, you simply just call the static function Minigame.LoadMainWorld(). Implement the call in a suitable way within your minigame.
To add coins earned in your minigame, the class SaveSystem offers several static functions. For example, SaveSystem.AddCredits(int amount) adds the specified amount to the save file.
The save file is located in Unity’s persistent path on your computer. If you are unsure where that location is, you can run Debug.Log(Application.persistentDataPath) to obtain it.

How to add new minigames to the information sign?
To add more minigames to the information sign, it needs to be redesigned.
First step is to make a new image to have as a place to display all minigames, to reach this image the button on the information sign needs to be redirected.
Create a general container for minigames that has a name of the game, description and a button to get teleported to where you start the game.
This container can then be copied to serve all minigames, you change the text and description.
To make the button work, a new function in the information script needs to be added, one that changes the position of the player to where the minigame is started. Since each button has its own function within the script. 

How to add new buyable items?
To buy an item it must first be added in the Item.cs file at the enum ItemType.
After the first step  the new item has to be added as a case with a return to all the functions in the class.
Optional to add more sprites into the item class you must firstly add a code line to Game assets and then refer to it as follows:
GameAssets.i.newspritename;
Then you must add the sprite to the game assets object in the scene.
To add the buy button you must first run another CreateItemButton from the UI-Shop class and add another line to tryBuyItem with the referred possitionIndex in the function here you should also refer it to the item you are trying to buy.
The GameObject must also be added to the BuyableObjects gameObject in the hierarki. Place the item in the world on the position where it should exist and add the item in the hierarki. Tag the object with “BuyItemX” where X represents a unique id. Check the other items for example.
 
What API is being used to check spelling?
Information about the API can be found here: https://skrutten.csc.kth.se/granskaapi/spell
Current configuration is a http GET and a JSON-reply.
To check the word “Flygplan”, a GET is sent as “https://skrutten.csc.kth.se/granskaapi/spell/json/Flygplan”.

Suggestions for the Achievement-orb.
The achievement orb is implemented and has interaction, but has no functional achievements yet. 
As the first step to implement working achievement a good choice would be to make a general container as with the minigames in the information sign. This container would describe the achievement and might display the progress and what is left to be done in order to achieve it.
Then in the scripts folder there is a folder for achievements where one could place all separate scripts for each different achievement.
An achievement class would be good and make a list with achievement objects in the savefile.
In the container a circle color red or green to indicate if the achievement has been done or not would be good. 
