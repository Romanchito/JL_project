CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Login]        NVARCHAR(100)     NOT NULL,
    [Password]     NVARCHAR(50)      NOT NULL,
    [Name]         NVARCHAR(100)      NOT NULL,
    [Surname]      NVARCHAR(100)      NOT NULL,
    [AccountImage] VARBINARY (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

