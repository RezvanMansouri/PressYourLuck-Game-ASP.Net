# PressYourLuck-Game
a simple betting game 

ASP.NET MVC Core
The goal of this project is to create a simple betting game that uses sessions, serialization and cookies to keep track of most of the information in this game. 

Highlights:
●	The current player name and their total number of current coins will be stored in a cookie so that it can persist across sessions
●	The current game state (for the current player) will be stored in a Session
●	Essentially this game consists of a List of (randomly generated) Tile objects. I used the NewtonSoft.Json package to serialize/deserialize
  the current game to/from the session as Json strings.
●	I implemented some static helper methods along the way to make it easier to interact with the required Session & Cookie data
●	then the solution stores an audit trail in a SQL server database
