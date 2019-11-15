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
insert into Films (Name, Country, Director, ReleaseDate, Stars, WorldwideGross)
values('Spider-Man 2','USA','P.Boil','2002-01-01','Toby,Linda,Klide,Tom',1300000000);
insert into Films (Name, Country, Director, ReleaseDate, Stars, WorldwideGross)
values('Spider-Man 3','Germany','t.Filips jr','2002-01-01','Toby,Linda,Klide,Tom',1400000000);
insert into Films (Name, Country, Director, ReleaseDate, Stars, WorldwideGross)
values('Spider-Man 4','Germany','P.Boil','2002-01-01','Toby,Linda,Klide,Tom',1500000000);
insert into Films (Name, Country, Director, ReleaseDate, Stars, WorldwideGross)
values('Spider-Man 5','England','P.Boil','2002-01-01','Toby,Linda,Klide,Tom',1600000000);

