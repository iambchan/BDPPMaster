USE bdppmasterdb;

--Select * from Games;
--Select * from Players;
--Select * from Teams;

--find player
SELECT * FROM [Players] WHERE ScreenName LIKE '%Guest%' OR BDLoginName LIKE '%Guest%';

--create new player & return player
INSERT INTO [Players] (FirstName, LastName, ScreenName, BDLoginName, Email, RFID)
Output Inserted.*
VALUES ('Six', 'Guest', 'Guest6', 'bdppGuest6', 'bdppGuest6@bd.com', '');

--update existing player
UPDATE [Players]
Set FirstName = '',
	LastName = '',
	ScreenName = '',
	BDLoginName = '',
	Email = '',
	ProfileImage = '',
	RFID = ''
WHERE FirstName = '',
	AND LastName = '',
	AND ScreenName = '',
	AND BDLoginName = '';

--Update exisitng player profile picture
UPDATE [Players]
Set ProfileImage = ''
WHERE PlayerId = 1;

--create new team
INSERT INTO [Teams] (Player1_Id, Player2_Id)
VALUES (1, 2);

----Update Team
--UPDATE [Teams]
--SET Player1_Id = 1,
--	Player2_Id = 2
--WHERE TeamId = 1;

--Create new Game
INSERT INTO [Games] (Team1_Id, Team2_Id, StartDateTime)
VALUES
(1, 2, CURRENT_TIMESTAMP);

--Update Game (update scores)
UPDATE [Games] 
SET Team1_score = 1, Team2_score = 1
--, EndDateTime = CURRENT_TIMESTAMP
)
WHERE GameId = 1;

--Update/End Game (update scores & end game)
UPDATE [Games] 
SET Team1_score = 1, Team2_score = 1, EndDateTime = CURRENT_TIMESTAMP)
WHERE GameId = 1;

--get game duration
SELECT GameId, DATEDIFF(MINUTE, StartDateTime, EndDateTime)
FROM [Games]
WHERE GameId = 1;

