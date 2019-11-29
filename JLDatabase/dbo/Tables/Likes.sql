CREATE TABLE [dbo].[Likes] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [UserId]   INT NOT NULL,
    [ReviewId] INT NOT NULL,
    [IsLike]   BIT NOT NULL,
    CONSTRAINT [PK_Likes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Likes_Reviews] FOREIGN KEY ([ReviewId]) REFERENCES [dbo].[Reviews] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Likes_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

