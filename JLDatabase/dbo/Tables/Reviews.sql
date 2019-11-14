CREATE TABLE [dbo].[Reviews] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (100) NOT NULL,
    [Text]   NVARCHAR (MAX) NOT NULL,
    [Date]   DATETIME       NOT NULL,
    [UserId] INT            NOT NULL,
    [FilmId] INT            NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reviews_Films] FOREIGN KEY ([FilmId]) REFERENCES [dbo].[Films] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Reviews_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

