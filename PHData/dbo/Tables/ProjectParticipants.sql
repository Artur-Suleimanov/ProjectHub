CREATE TABLE [dbo].[ProjectParticipants]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProjectId] INT NOT NULL, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [RoleId] INT NOT NULL

    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
	FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),

    UNIQUE ([ProjectId], [UserId]) -- Ограничение: один пользователь в одном проекте только один раз
)
