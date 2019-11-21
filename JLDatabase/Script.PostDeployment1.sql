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



USE JLDatabase;
GO
MERGE Films USING 
(	
	values
	('Spider-Man 2','USA','P.Boil','2002-01-01','Toby,Linda,Klide,Tom',1300000000),
	('Spider-Man 3','Germany','t.Filips jr','2002-01-01','Toby,Linda,Klide,Tom',1400000000),
	('Spider-Man 4','Germany','P.Boil','2002-01-01','Toby,Linda,Klide,Tom',1500000000),
	('KKK','USA','P.Boil','2012-01-01','Soul Mn Jeffry, Katlin Stark',9900000)
) 
AS Source (Name,Country,Director,ReleaseDate,Stars,WorldwideGross)	ON Films.Name = Source.Name

WHEN NOT MATCHED 
	THEN INSERT (Name,Director,Stars,Country,ReleaseDate,WorldwideGross)
		 VALUES (Name,Director,Stars,Country,ReleaseDate,WorldwideGross);
