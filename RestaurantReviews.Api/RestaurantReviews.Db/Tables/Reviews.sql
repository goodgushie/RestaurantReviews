CREATE TABLE [dbo].[Reviews]
(
	[Id] INT NOT NULL IDENTITY(1, 1), 
    [UserId] INT NOT NULL, 
    [RestaurantId] INT NOT NULL, 
    [Rating] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Active] BIT NOT NULL, 
    [LastUpdated] DATETIME2 NOT NULL, 
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Reviews_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_Reviews_Restaurants] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants]([Id])
)
