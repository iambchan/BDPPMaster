USE bdppmasterdb;
GO

SET NOCOUNT ON;
GO

IF OBJECT_ID('dbo.[Games]', 'U') IS NOT NULL
BEGIN
	IF EXISTS (SELECT * FROM sysobjects WHERE name = 'FK_Games_Teams_T1')
		ALTER TABLE [Games] DROP CONSTRAINT FK_Games_Teams_T1
	IF EXISTS (SELECT * FROM sysobjects WHERE name = 'FK_Games_Teams_T2')
		ALTER TABLE [Games] DROP CONSTRAINT FK_Games_Teams_T2
	DROP TABLE [Games]
END
IF OBJECT_ID('dbo.[Teams]', 'U') IS NOT NULL
BEGIN
	IF EXISTS (SELECT * FROM sysobjects WHERE name = 'FK_Teams_Players_P1')
		ALTER TABLE [Teams] DROP CONSTRAINT FK_Teams_Players_P1
	IF EXISTS (SELECT * FROM sysobjects WHERE name = 'FK_Teams_Players_P2')
		ALTER TABLE [Teams] DROP CONSTRAINT FK_Teams_Players_P2
	DROP TABLE [Teams]
END
IF OBJECT_ID('dbo.[Players]', 'U') IS NOT NULL
BEGIN
	DROP TABLE [Players]
END
GO

CREATE TABLE [Players]
(
	PlayerId		int IDENTITY(1,1) PRIMARY KEY,
	FirstName		NVARCHAR(50),
	LastName		NVARCHAR(50),
	ScreenName		NVARCHAR(50),
	BDLoginName		NVARCHAR(50) NOT NULL,
	Email			NVARCHAR(255),
	ProfileImage	IMAGE,
	RFID			VARCHAR(255)
);

CREATE TABLE [Teams]
(
	TeamId		int IDENTITY(1,1) PRIMARY KEY,
	Player1_Id	int CONSTRAINT FK_Teams_Players_P1 FOREIGN KEY (Player1_Id) REFERENCES [Players](PlayerId),
	Player2_Id	int DEFAULT NULL CONSTRAINT FK_Teams_Players_P2 FOREIGN KEY (Player2_Id) REFERENCES [Players](PlayerId)
);

CREATE TABLE [Games]
(
	GameId			int IDENTITY(1,1) PRIMARY KEY,
	Team1_Id		int CONSTRAINT FK_Games_Teams_T1 FOREIGN KEY (Team1_Id) REFERENCES [Teams] (TeamId),
	Team2_Id		int CONSTRAINT FK_Games_Teams_T2 FOREIGN KEY (Team2_Id) REFERENCES [Teams] (TeamId),
	Team1_score		int NOT NULL DEFAULT 0,
	Team2_score		int NOT NULL DEFAULT 0,
	StartDateTime	DateTime,
	EndDateTime		DateTime
);
GO

INSERT INTO [Players] (FirstName, LastName, ScreenName, BDLoginName)
VALUES
('One', 'Guest', 'Guest1', 'bdppGuest1'),
('Two', 'Guest', 'Guest2', 'bdppGuest2'),
('Three', 'Guest', 'Guest3', 'bdppGuest3'),
('Four', 'Guest', 'Guest4', 'bdppGuest4');

INSERT INTO [Teams] (Player1_Id, Player2_Id)
VALUES
(1, 2),
(3, 4);

INSERT INTO [Games] (Team1_Id, Team2_Id, Team1_score, Team2_score, StartDateTime, EndDateTime)
VALUES
(1, 2, 15, 14, CURRENT_TIMESTAMP, DATEADD(MINUTE, 10, CURRENT_TIMESTAMP));
GO

