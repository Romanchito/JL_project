CREATE TABLE [dbo].[Films] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (100) NOT NULL,
    [Director]       NVARCHAR (100) NOT NULL,
    [Stars]          NVARCHAR (200) NOT NULL,
    [Country]        NVARCHAR (50)  NOT NULL,
    [ReleaseDate]    DATE           NOT NULL,
    [WorldwideGross] DECIMAL (18)   NOT NULL,
    [FilmImage]      NVARCHAR (200) NULL,
    CONSTRAINT [PK_Films] PRIMARY KEY CLUSTERED ([Id] ASC)
);

