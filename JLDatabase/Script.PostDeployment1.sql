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



insert into Films (Name, Country, Director, ReleaseDate, Stars, WorldwideGross)
values('Spider-Man','USA','P.Boil','2002-01-01','Toby,Linda,Klide,Tom',1200000000);


