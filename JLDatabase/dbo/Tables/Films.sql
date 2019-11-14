CREATE TABLE [dbo].[Films] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (100) NOT NULL,
    [Director]       NVARCHAR (100) NOT NULL,
    [Stars]          NVARCHAR (MAX) NOT NULL,
    [Country]        NVARCHAR (50)  NOT NULL,
    [ReleaseDate]    DATE           NOT NULL,
    [WorldwideGross] MONEY          NOT NULL,
    CONSTRAINT [PK_Films] PRIMARY KEY CLUSTERED ([Id] ASC)
);

