CREATE TABLE [dbo].[Task]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [InitiatorId] NVARCHAR(128) NOT NULL, 
    [ExecutorId] NVARCHAR(128) NOT NULL, 
    [ProjectId] INT NOT NULL, 
    [StateId] INT NOT NULL, 
    [CreateDate] DATETIME2 NOT NULL DEFAULT getutcdate()

    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
    [SolutionText] NVARCHAR(MAX) NULL, 
    FOREIGN KEY ([InitiatorId]) REFERENCES [dbo].[User] ([Id]),
    FOREIGN KEY ([ExecutorId]) REFERENCES [dbo].[User] ([Id]),
	FOREIGN KEY ([StateId]) REFERENCES [dbo].[TaskState] ([Id]),
)
