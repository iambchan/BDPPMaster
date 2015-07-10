USE bdppmasterdb;

Select * from Games;
Select * from Players;
Select * from Teams;

--find player
SELECT * FROM [Players] WHERE FirstName LIKE '%One%' OR LastName LIKE '%Guest%' OR ScreenName LIKE '%Guest%' OR BDLoginName LIKE '%Guest%' OR Email Like '%%' OR RFID LIKE '%%';
SELECT * FROM [Players] WHERE ScreenName = 'Guest1' OR BDLoginName = 'bdppGuest1';

--create new player & return player
INSERT INTO [Players] (FirstName, LastName, ScreenName, BDLoginName, Email, RFID)
Output Inserted.*
VALUES ('Five', 'Guest', 'Guest5', 'bdppGuest5', 'bdppGuest5@bd.com', '');

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
OUTPUT Inserted.TeamId
VALUES (1, 3);

--getTeamPlayerInfo
SELECT t.TeamId, p.* 
FROM [Teams] t
INNER JOIN [Players] p
ON p.PlayerId IN (t.Player1_Id, t.Player2_Id)
WHERE t.TeamId = 1;

----Update Team
--UPDATE [Teams]
--SET Player1_Id = 1,
--	Player2_Id = 2
--WHERE TeamId = 1;

--Create new Game
INSERT INTO [Games] (Team1_Id, Team2_Id, StartDateTime)
OUTPUT Inserted.GameId
VALUES (1, 2, CURRENT_TIMESTAMP);


--getGameInfo
SELECT * FROM [Players] p
INNER JOIN [Teams] t ON p.PlayerId IN (t.Player1_id, t.Player2_id)
INNER JOIN [Games] g ON t.TeamId IN (g.Team1_id, g.Team2_id);

SELECT t.TeamId, p.* 
FROM [Teams] t
INNER JOIN [Players] p
ON p.PlayerId IN (t.Player1_Id, t.Player2_Id)
WHERE t.TeamId = 1;

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

--SELECT * FROM [Players] p
--INNER JOIN [Teams] t ON p.PlayerId = t.Player1_id
--OR p.PlayerId = t.Player2_id
--INNER JOIN [Games] g ON g.Team1_id = t.TeamId
--OR g.Team2_id = t.TeamId


--SELECT * FROM [Players] p
--INNER JOIN [Teams] t ON p.PlayerId = t.Player1_id
--OR p.PlayerId = t.Player2_id
--INNER JOIN [Games] g ON g.Team1_id = t.TeamId
--OR g.Team2_id = t.TeamId
--WHERE 
--(p.PlayerId = t.Player1_id
--AND g.Team1_score > g.Team2_score)
--OR 
--(p.PlayerId = t.Player2_id
--AND g.Team2_score > g.Team1_score)
