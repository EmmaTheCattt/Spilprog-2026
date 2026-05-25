TACTIC TOE
------------------------------------------------
Hola, We have made two folders. On with a whole unity project for you to look at our code in and a complete build to test the game.

Briefing on the relavent content in the project:
Everything in our final project is contained within the scene: "Valdemar"
except for our player prefab. This can be found in the prefab folder.

The scene contains:
NetworkManager - a game object that contains our Network Manager and Unity Transport script
UIDocumment - a UI Document with a Connection UI script. You should be familiar with this.
DatabaseTest - a game object with our script for interfacing with our database
EventSystem - Manages mouse input.
InfoManager - Holds the Network Data script, this singleton holds player information after they login.
Player_InfoHOST and PlayerInfoCLIENT - contains canvases with text for displaying the information stored in the Network Data script.
GAME - Contains the playing board, game pieces, tiles and the WinManager
	Tiles - Each contains a trigger to see if a game piece is placed on it at any given time.
	Game pieces - Holds a script that allows them to be picked up and dragged around with the mouse.
	WinManager - A script that checks if any player has won, updates the rating based on their ELO amd finally resets the board state when the game is over

Player Prefab
PlayerData - used to hold what the Network Data script does, now it's used to interface between the login buttons and the Database.
LoginCanvasClient and LoginCanvasHost - Fields for inputting login information and buttons for calling the NewPlayer and Login methods.

HOW TO PLAY THE GAME:
1. 	Run two seperate instances of the game. Either through the unity editor (Includes special bug) or from the Final Build Folder.
2. 	Have one Instance join as HOST, press the "HOST" button in the upper left corner.
3. 	Have the other join as CLIENT, press the "CLIENT" button in the upper left corner.
4.  a.	Enter a name not already in the system and a password, press "Register" and have a fresh player made in the system with a rating of 1000
    b.	Enter a name fx "Alex" and the password "MinKode" and hit Login.
	The following players are added when the Database is first created:
	"Alex", "Camilla", "Rose", "Emma", "Valdemar", "Magnus C." and "Magnus Carlsen"
	All of the passwords are "MinKode" (Very secure, Much safe)
5. 	Repeat for the other player.
6. 	Move the desired pieces, our game is fast paced and without turn order by design! It operates on a first come first served politic.
7.	When wither player has connected 3 pieces in a row, the game will end rather instantly, and the players ratings will be updated and the board reset.
8. 	To play again simple move the pieces again. 
9. 	To login as different players, you will need to close both instances and open them again. This is also a feature...

				

