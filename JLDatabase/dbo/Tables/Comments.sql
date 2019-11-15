CREATE TABLE [dbo].[Comments] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Text]     NVARCHAR (220) NOT NULL,
    [Date]     DATETIME       NOT NULL,
    [UserId]   INT            NOT NULL,
    [ReviewId] INT            NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Comments_Reviews] FOREIGN KEY ([ReviewId]) REFERENCES [dbo].[Reviews] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Comments_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON UPDATE CASCADE
);

