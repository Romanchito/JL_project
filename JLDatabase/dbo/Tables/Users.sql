CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Login]        NCHAR (100)     NOT NULL,
    [Password]     NCHAR (20)      NOT NULL,
    [Name]         NCHAR (40)      NOT NULL,
    [Surname]      NCHAR (40)      NOT NULL,
    [AccountImage] VARBINARY (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

