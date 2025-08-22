CREATE TABLE [dbo].[Character]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100),
    [CurseLevel] INT,
    [VibeType] NVARCHAR(50),
    [BackStory] NVARCHAR(MAX),
    [Strength] INT,
    [Agility] INT,
    [Intelligence] INT,
    [Luck] INT
)
