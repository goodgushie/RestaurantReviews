CREATE TABLE [dbo].[Restaurants]
(
	[Id] INT NOT NULL IDENTITY (1, 1), 
    [Name] NVARCHAR(50) NOT NULL, 
    [AddressLine1] NVARCHAR(100) NOT NULL, 
    [AddressLine2] NVARCHAR(100) NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [Province] NVARCHAR(4) NULL, 
    [PostalCode] NVARCHAR(10) NULL, 
    [Country] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Restaurants] PRIMARY KEY ([Id])
)
