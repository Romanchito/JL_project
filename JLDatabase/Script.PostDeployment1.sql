/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/




CREATE TABLE Creation(
	Id int IDENTITY(1,1) NOT NULL,
	Name nvarchar(100) NOT NULL,
	Director nvarchar(100) NOT NULL,
	Stars nvarchar(200) NOT NULL,
	Country nvarchar(50) NOT NULL,
	ReleaseDate date NOT NULL,
	WorldwideGross decimal(18, 0) NOT NULL
);



insert into Creation (Name, Country, Director, ReleaseDate, Stars, WorldwideGross)
values('Spider-Man 2','USA','P.Boil','2002-01-01','Toby,Linda,Klide,Tom',1300000000);
insert into Creation (Name, Country, Director, ReleaseDate, Stars, WorldwideGross)
values('Spider-Man 3','Germany','t.Filips jr','2002-01-01','Toby,Linda,Klide,Tom',1400000000);
insert into Creation (Name, Country, Director, ReleaseDate, Stars, WorldwideGross)
values('Spider-Man 4','Germany','P.Boil','2002-01-01','Toby,Linda,Klide,Tom',1500000000);
insert into Creation (Name, Country, Director, ReleaseDate, Stars, WorldwideGross)
values('Spider-Man 5','England','P.Boil','2002-01-01','Toby,Linda,Klide,Tom',1600000000);


USE JLDatabase;
GO
MERGE Films USING Creation ON Films.Name = Creation.Name
	WHEN MATCHED
		THEN UPDATE SET 
			Films.Name = Creation.Name,
			Films.Director = Creation.Director,
			Films.ReleaseDate = Creation.ReleaseDate,
			Films.WorldwideGross = Creation.WorldwideGross,
			Films.Stars = Creation.Stars,
			Films.Country = Creation.Country

	WHEN NOT MATCHED 
		THEN INSERT (Name,Director,Stars,Country,ReleaseDate,WorldwideGross)
			 VALUES (Creation.Name,Creation.Director,Creation.Stars,Creation.Country,Creation.ReleaseDate,Creation.WorldwideGross);
DROP TABLE Creation;
