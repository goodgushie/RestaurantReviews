CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY(1, 1), 
    [ScreenName] NVARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
)
