--delete bestaande data
DELETE FROM  dbo.Begeleiding;
IF(IDENT_CURRENT('dbo.Begeleiding') = 1) BEGIN DBCC checkident('dbo.Begeleiding', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.Begeleiding', reseed,0); END;
DELETE FROM  dbo.KlasStudiebezoek;
IF(IDENT_CURRENT('dbo.KlasStudiebezoek') = 1) BEGIN DBCC checkident('dbo.KlasStudiebezoek', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.KlasStudiebezoek', reseed,0); END;
DELETE FROM  dbo.Foto;
IF(IDENT_CURRENT('dbo.Foto') = 1) BEGIN DBCC checkident('dbo.Foto', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.Foto', reseed,0); END;
DELETE FROM  dbo.FotoAlbum;
IF(IDENT_CURRENT('dbo.FotoAlbum') = 1) BEGIN DBCC checkident('dbo.FotoAlbum', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.FotoAlbum', reseed,0); END;
DELETE FROM  dbo.Bijlage;
IF(IDENT_CURRENT('dbo.Bijlage') = 1) BEGIN DBCC checkident('dbo.Bijlage', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.Bijlage', reseed,0); END;
DELETE FROM  dbo.Studiebezoek;
IF(IDENT_CURRENT('dbo.Studiebezoek') = 1) BEGIN DBCC checkident('dbo.Studiebezoek', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.Studiebezoek', reseed,0); END;
DELETE FROM  dbo.GebruikerNavorming;
IF(IDENT_CURRENT('dbo.GebruikerNavorming') = 1) BEGIN DBCC checkident('dbo.GebruikerNavorming', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.GebruikerNavorming', reseed,0); END;
DELETE FROM  dbo.Navorming;
IF(IDENT_CURRENT('dbo.Navorming') = 1) BEGIN DBCC checkident('dbo.Navorming', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.Navorming', reseed,0); END;
DELETE FROM  dbo.Afwezigheid;
IF(IDENT_CURRENT('dbo.Afwezigheid') = 1) BEGIN DBCC checkident('dbo.Afwezigheid', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.Afwezigheid', reseed,0); END;
DELETE FROM  dbo.Gebruiker;
DELETE FROM  dbo.Vak;
IF(IDENT_CURRENT('dbo.Vak') = 1) BEGIN DBCC checkident('dbo.Vak', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.Vak', reseed,0); END;
DELETE FROM  dbo.Klas;
IF(IDENT_CURRENT('dbo.Klas') = 1) BEGIN DBCC checkident('dbo.Klas', reseed,1); END
ELSE BEGIN DBCC checkident('dbo.Klas', reseed,0); END;

--generate insert
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('1.1 Moderne', 1);
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('1.2 Metaalbewerking', 0);
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('2.1 Tuinbouw', 0);
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('3.3 Economie', 0);
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('4.1 Informatica', 0);
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('4.2 Latijn', 1);
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('5.1 Boekhouden', 0);
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('5.2 Elektronica', 0);
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('6.2 Humane', 0);
INSERT INTO dbo.Klas (naam, verwijderd) VALUES('6.3 Zorgkunde', 0);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Nederlands', 0);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Frans', 0);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Houtbewerking', 1);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Lichamelijke opvoeding', 0);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Wiskunde', 0);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Chemie', 0);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Informatica', 0);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Zorgkunde', 0);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Aardrijkskunde', 0);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Godsdienst', 1);
INSERT INTO dbo.Vak (naam, verwijderd) VALUES('Geschiedenis', 0);
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) 
VALUES('2e91ef14-54e8-4b78-b2e2-394c163065b6', 'Van Looyen', 'Arne', 'Arne VL', 0, 'ArneVanLooyen@School.com', 'ARNEVANLOOYEN@SCHOOL.COM', 'ArneVanLooyen@School.com', 'ARNEVANLOOYEN@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAEPDbqalkvfg6B4RioDQrzgTg/aAUeJdQ/xyOqWfe9tLyFh7IizojlWLo//aqpj1uKA==', 'OHZAFXMJWIGHSAGZSK2HGNSHAMDNMFR4', '15964517-45a4-4b23-84e6-c2ab28da40f0', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES('31f08605-16fc-4a44-bdd4-db02b747423f', 'Tijlen', 'Inne', 'Inne', 0, 'InneTijlen@School.com', 'INNETIJLEN@SCHOOL.COM', 'InneTijlen@School.com', 'INNETIJLEN@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAEGPhgFYC76VMxp3tbaPSZEqXSHTzwvt3ApH8uc9wSD3WZE8jeVtdULg3ZxpdTvXdwg==', 'BOGGRPE7VBSWJVOI2C742T3YT5DQO43I', 'dbb677e6-9fbc-4cf0-a24e-026c4db04b3a', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES('37d7b81b-4118-445d-927c-68095609db85', 'Bakkers', 'Fik', 'Fikske', 0, 'FikBakkers@School.com', 'FIKBAKKERS@SCHOOL.COM', 'FikBakkers@School.com', 'FIKBAKKERS@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAEMXpuoyE0OvEIr/TYaFb9X9Rb+EQOpgb1Uh1WNEo++vAi0OU60VogTHhcbIH3cIXTw==', 'ASEFMDL6ZZNGQ67QQMRVT45XHMPALJZ5', 'd37c6625-f52c-4c0d-8e34-7349d5101485', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES('401b4e87-7535-4c5b-8d89-885688373b89', 'AchterHuze', 'Kathelijn', 'Kathelijn A.', 0, 'KathelijnAchterHuze@School.com', 'KATHELIJNACHTERHUZE@SCHOOL.COM', 'KathelijnAchterHuze@School.com', 'KATHELIJNACHTERHUZE@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAELHl03wDTf295S/doEt/Oqa00e7W80FRocKglqEquuGKAAph/PEXW5wtDK1sWFG63w==', 'KZKF5C23I5XGHTTHXYHJ4ISQ6CCZSNXB', '1db5be95-9225-43c8-aa2a-a60cf6c254e2', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES('4b30acf3-6750-4021-86ee-fe16c8a6fc0a', 'De Poorteren', 'Elise', 'Ellie <333', 0, 'EliseDePoorteren@School.com', 'ELISEDEPOORTEREN@SCHOOL.COM', 'EliseDePoorteren@School.com', 'ELISEDEPOORTEREN@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAEJnE4IvP5B9r2SwOYCNU0SoELjKWwtv3+YvXIk5JZhKxEudIxJFv6Z3L/4khg2n+5g==', 'U5DVT6Q77OZZSXTAHYSZMP6CKQ3HI5UT', '66c54cc5-852a-4da6-8fe9-6c9b20e8c794', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES('741f8fb7-ca4d-4a0d-b468-51ed6ec847bb', 'Minne', 'Daniel', 'Daniel Minne', 0, 'DanielMinne@School.com', 'DANIELMINNE@SCHOOL.COM', 'DanielMinne@School.com', 'DANIELMINNE@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAEE7kTIPvufVQV4t+lVe28lwL6uz+GKTpUxmh4nUGHSJ7mHeIdibopANT5p3OG5B0QA==', 'O5ZFACM35X5UHO5YPFKR53VIJ4S5OKLF', '6cff1b03-3701-4534-aeb7-37096ec62444', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES('7a262c9b-c1fc-486a-b862-b64bd564cafc', 'Vermeulen', 'Fons', 'Fonske', 0, 'FonsVermeulen@School.com', 'FONSVERMEULEN@SCHOOL.COM', 'FonsVermeulen@School.com', 'FONSVERMEULEN@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAEKRsf3T9GepKSsMmghyl7cp3Q2P9a9A2zs3KaBu5MEwLTMDfG7L9oZI6ICMhc8R11g==', '74GCLGMLDBAP6AF734JWVL33NS7TJR7K', '40fc42ae-c7dd-47a7-8a01-dca049170d6d', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES('9ed8c9dd-ef3b-4b1c-83bd-18343a92edc5', 'Vroens', 'Jacques', 'Jacque_Frans', 0, 'JacquesVroens@School.com', 'JACQUESVROENS@SCHOOL.COM', 'JacquesVroens@School.com', 'JACQUESVROENS@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAEJt6+QYfc1KT4IECg+v+MZ+Jx0RwTCE3urhTXKXsFQlFBxWzepsANHRIvhdrbQwOsA==', 'Z5AYADLIXB2AIAQ57LBWNOQPC2UPDRTZ', '95360392-0a37-4af7-84ef-b8fc3a3edec3', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES('c30ccfa2-1ef2-4908-b094-429f2a555196', 'Qimbala', 'Zaza', 'Zaza (intrim)', 0, 'ZazaQimbala@School.com', 'ZAZAQIMBALA@SCHOOL.COM', 'ZazaQimbala@School.com', 'ZAZAQIMBALA@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAEEZb+kn9/fYTE/EgKoZdnO0MxS7AXbdXrG459O/EfnnJPxs0h5iwYSYgvV6eBKxl1w==', 'NLWGHDV7KGMK6TERFVDRMDYSAYW5JR7S', '8091b13c-884e-4fd2-aea4-0bdd63674923', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Gebruiker (Id, Naam, Voornaam, Gebruikersnaam, Verwijderd, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
VALUES('ef8c1c81-6f72-45e7-b124-f35a18051738', 'Smets', 'Marthe', 'Marthe X', 0, 'MartheSmets@School.com', 'MARTHESMETS@SCHOOL.COM', 'MartheSmets@School.com', 'MARTHESMETS@SCHOOL.COM', 1, 'AQAAAAEAACcQAAAAEM75HwNLZt0laK/4udPoOyCJIuD48SdYXYDYvzg6y4U+1zGLggh+i0KCHat4vi1IXQ==', '6AVGMNIPCU4LDGOTGNAHCSFIXCAIBUWA', '7336c58e-ae43-4683-8f7c-ca67e3a9e59c', 'NULL', 0, 0, NULL, 1, '0');
INSERT INTO dbo.Afwezigheid (gebruikerId, startDatum, eindDatum) VALUES('4b30acf3-6750-4021-86ee-fe16c8a6fc0a', '2022-05-06 00:00:00:000', '2022-06-01 00:00:00:000');
INSERT INTO dbo.Afwezigheid (gebruikerId, startDatum, eindDatum) VALUES('4b30acf3-6750-4021-86ee-fe16c8a6fc0a', '2022-03-01 00:00:00:000', '2022-03-02 00:00:00:000');
INSERT INTO dbo.Afwezigheid (gebruikerId, startDatum, eindDatum) VALUES('4b30acf3-6750-4021-86ee-fe16c8a6fc0a', '2021-02-01 00:00:00:000', '2021-03-10 00:00:00:000');
INSERT INTO dbo.Afwezigheid (gebruikerId, startDatum, eindDatum) VALUES('401b4e87-7535-4c5b-8d89-885688373b89', '2022-05-09 00:00:00:000', '2022-05-10 00:00:00:000');
INSERT INTO dbo.Afwezigheid (gebruikerId, startDatum, eindDatum) VALUES('ef8c1c81-6f72-45e7-b124-f35a18051738', '2022-05-09 00:00:00:000', '2022-05-10 00:00:00:000');
INSERT INTO dbo.Afwezigheid (gebruikerId, startDatum, eindDatum) VALUES('401b4e87-7535-4c5b-8d89-885688373b89', '2022-05-15 00:00:00:000', '2022-05-20 00:00:00:000');

INSERT INTO dbo.Navorming (AanvragerId, datum, startUur, eindUur, reden, kostprijs, isGoedgekeurdDir, isAfgewerkt)
VALUES('37d7b81b-4118-445d-927c-68095609db85', '2022-06-27 11:45:00:000', '2022-09-01 12:00:00:000', '2022-09-01 18:00:00:000', 'algemene bijscholing pedagogie', CAST('200.00' AS FLOAT), 1, 1);
INSERT INTO dbo.Navorming (AanvragerId, datum, startUur, eindUur, reden, kostprijs)
VALUES('9ed8c9dd-ef3b-4b1c-83bd-18343a92edc5', '2022-10-11 08:00:00:000', '2022-10-15 08:00:00:000', '2022-10-15 09:00:00:000', 'Jeugdig en gezond lesgeven - schoolactie', CAST('50.00' AS FLOAT));
INSERT INTO dbo.Navorming (AanvragerId, datum, startUur, eindUur, reden, kostprijs)
VALUES('ef8c1c81-6f72-45e7-b124-f35a18051738', '2022-12-27 23:29:00:000', '2023-01-15 08:30:00:000', '2023-01-15 12:30:00:000', 'Bijskolingsmoment frans gramatica', CAST('20.00' AS FLOAT));
INSERT INTO dbo.Navorming (AanvragerId, datum, startUur, eindUur, reden, kostprijs, isGoedgekeurdDir, isAfgewezen, opmerkingAfgewezen)
VALUES('31f08605-16fc-4a44-bdd4-db02b747423f', '2022-03-01 15:15:00:000', '2022-03-23 15:30:00:000', '2022-03-23 15:45:00:000', 'bewustzijnscampagne over huisslakken', CAST('0.00' AS FLOAT), 0, 1, 'Duurt niet lang genoeg');

INSERT INTO dbo.GebruikerNavorming (DeelnemerId, navormingId) VALUES('9ed8c9dd-ef3b-4b1c-83bd-18343a92edc5', 1);
INSERT INTO dbo.GebruikerNavorming (DeelnemerId, navormingId) VALUES('741f8fb7-ca4d-4a0d-b468-51ed6ec847bb', 1);
INSERT INTO dbo.GebruikerNavorming (DeelnemerId, navormingId) VALUES('31f08605-16fc-4a44-bdd4-db02b747423f', 1);
INSERT INTO dbo.GebruikerNavorming (DeelnemerId, navormingId) VALUES('ef8c1c81-6f72-45e7-b124-f35a18051738', 1);
INSERT INTO dbo.GebruikerNavorming (DeelnemerId, navormingId) VALUES('31f08605-16fc-4a44-bdd4-db02b747423f', 2);
INSERT INTO dbo.GebruikerNavorming (DeelnemerId, navormingId) VALUES('ef8c1c81-6f72-45e7-b124-f35a18051738', 2);
INSERT INTO dbo.GebruikerNavorming (DeelnemerId, navormingId) VALUES('401b4e87-7535-4c5b-8d89-885688373b89', 2);
INSERT INTO dbo.GebruikerNavorming (DeelnemerId, navormingId) VALUES('c30ccfa2-1ef2-4908-b094-429f2a555196', 3);
INSERT INTO dbo.GebruikerNavorming (DeelnemerId, navormingId) VALUES('401b4e87-7535-4c5b-8d89-885688373b89', 4);

INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen, isGoedgekeurdCo, isGoedgekeurdDir, isAfgewezen)
VALUES('9ed8c9dd-ef3b-4b1c-83bd-18343a92edc5', '2', '2022-06-05 00:00:00:000', '2022-06-15 10:30:00:000', '2022-06-15 18:00:00:000', 'Lezingen over gedichten door franse schrijvers op de boekenbeurs', 23, CAST('0.00' AS FLOAT), 1, 0, 0, 1, 0, 0, CAST('250.50' AS FLOAT), '', 'Gelieve een brief voor de ouders te voorzien', 1, 1, 0);
INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen, isGoedgekeurdCo, isGoedgekeurdDir, isAfgewezen, opmerkingAfgewezen)
VALUES('9ed8c9dd-ef3b-4b1c-83bd-18343a92edc5', '3', '2021-11-06 00:00:00:000', '2021-12-02 08:30:00:000', '2021-12-02 12:30:00:000', 'Bedrijfsbezoek houtbewerking  bij splinters en co, moderne machines', 16, CAST('100.00' AS FLOAT), 0, 0, 0, 1, 0, 0, CAST('0.00' AS FLOAT), '', '', 1, 0, 1, 'Alternatief aanbod wordt in overweging genomen');
INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen, isGoedgekeurdCo, isGoedgekeurdDir, isAfgewezen)
VALUES('4b30acf3-6750-4021-86ee-fe16c8a6fc0a', '11', '2022-10-21 00:00:00:000', '2022-11-04 9:00:00:000', '2022-11-10 16:30:00:000', 'Jaarlijkse excursie westhoek 5de jaars', 129, CAST('26500.00' AS FLOAT), 1, 0, 0, 1, 0, 0, CAST('600.00' AS FLOAT), 'Tom Leysen', 'Hotelboeken zijn nog niet geregeld', 1, 1, 0);
INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen, isGoedgekeurdCo, isGoedgekeurdDir, isAfgewezen)
VALUES('ef8c1c81-6f72-45e7-b124-f35a18051738', '6', '2022-09-05 00:00:00:000', '2022-09-20 06:15:00:000', '2022-09-20 08:30:00:000', 'Studie over de vorming en samenstelling van dauwdruppels', 7, CAST('0.00' AS FLOAT), 0, 0, 0, 1, 0, 0, CAST('0.00' AS FLOAT), '', '', 1, 1, 0);
INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen, isGoedgekeurdCo)
VALUES('4b30acf3-6750-4021-86ee-fe16c8a6fc0a', '8', '2021-07-08 00:00:00:000', '2021-10-01 00:00:00:000', '2021-10-06 00:00:00:000', 'Dagbezoek aan ziekenhuis Sint Rochus', 22, CAST('0.00' AS FLOAT), 0, 0, 0, 1, 0, 1, CAST('0.00' AS FLOAT), 'Joël Dierckx', '', 1);
INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen)
VALUES('31f08605-16fc-4a44-bdd4-db02b747423f', '10', '2022-03-09 00:00:00:000', '2022-03-21 08:30:00:000', '2022-03-21 17:00:00:000', 'Excursie religeuze diversiteit Jain tempel - Joodse Wijk', 56, CAST('2000.00' AS FLOAT), 1, 1, 1, 1, 0, 0, CAST('156.00' AS FLOAT), 'Jonas van Berloo', '');
INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen, isGoedgekeurdCo)
VALUES('2e91ef14-54e8-4b78-b2e2-394c163065b6', '4', '2021-05-16 00:00:00:000', '2021-06-04 08:30:00:000', '2021-06-04 16:00:00:000', 'Sportdag 2021 ', 129, CAST('2500.00' AS FLOAT), 1, 0, 0, 1, 0, 1, CAST('200.00' AS FLOAT), 'Dries Ennekens, Steff Hapers', '', 1);
INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen, isGoedgekeurdCo, isAfgewezen, opmerkingAfgewezen)
VALUES('741f8fb7-ca4d-4a0d-b468-51ed6ec847bb', '5', '2022-01-22 00:00:00:000', '2022-01-24 9:00:00:000', '2022-01-24 15:30:00:000', 'studenten laten werken aan de bouw van telramen voor mijn collectie', 35, CAST('0.00' AS FLOAT), 0, 0, 0, 0, 1, 0, CAST('50.00' AS FLOAT), '', 'Vergoeding voor transport mag op mijn rekening gestort worden', 0, 1, 'Gelieve geen studenten voor private winst te gebruiken');
INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen)
VALUES('401b4e87-7535-4c5b-8d89-885688373b89', '9', '2022-06-01 00:00:00:000', '2022-07-10 12:30:00:000', '2022-07-24 20:45:00:000', 'Bezoek aan zuid-spanje vanwege speciale strandformaties', 2, CAST('20000.00' AS FLOAT), 1, 0, 0, 0, 0, 0, CAST('3500.00' AS FLOAT), '', 'Sangria voor de docent en bronwater voor de studenten voorzien');
INSERT INTO dbo.Studiebezoek (AanvragerId, vakId, datum, startUur, eindUur, reden, aantalStudenten, kostprijsStudiebezoek, vervoerBus, vervoerTram, vervoerTrein, vervoerTeVoet, vervoerAuto, vervoerFiets, kostprijsVervoer, afwezigeStudenten, opmerkingen, isGoedgekeurdCo, isGoedgekeurdDir, isAfgewezen, isAfgewerkt)
VALUES('401b4e87-7535-4c5b-8d89-885688373b89', '1', '2021-04-01 00:00:00:000', '2021-05-08 7:00:00:000', '2021-05-08 12:30:00:000', 'Bezoek aan drukkerij Plantijn', 36, CAST('100.00' AS FLOAT), 0, 0, 1, 1, 0, 0, CAST('327.00' AS FLOAT), '', '', 1, 1, 0, 1);

INSERT INTO dbo.Bijlage (studiebezoekId, bestandsNaam) VALUES('4', 'BohrJournal_25nov_dew.txt');
INSERT INTO dbo.Bijlage (studiebezoekId, bestandsNaam) VALUES('1', 'Leesbrief_voorlezing_HugoFiron.txt');
INSERT INTO dbo.Bijlage (studiebezoekId, bestandsNaam) VALUES('3', 'WestHoek_Gids.txt');
INSERT INTO dbo.FotoAlbum (studiebezoekId, naam, datum) VALUES(5, 'Uitstap Jain tempel', '2022-03-09 8:30:00:000');
INSERT INTO dbo.FotoAlbum (studiebezoekId, naam, datum) VALUES(2, 'Excursie Westhoek', '2022-10-21 8:30:00:000');
INSERT INTO dbo.FotoAlbum (studiebezoekId, naam, datum) VALUES(8, 'Sportdag foto''s Jacques', '2021-05-16 8:30:00:000');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(1, 'Jain tempel Antwerpen buiten', 'https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Templejaindanvers.jpg/390px-Templejaindanvers.jpg');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(1, 'klasfoto tempel', 'https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Mahavir.jpg/176px-Mahavir.jpg');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(1, 'tempel binnen', 'https://upload.wikimedia.org/wikipedia/commons/thumb/3/3c/Thirthankara_Suparshvanath_Museum_Rietberg_RVI_306.jpg/255px-Thirthankara_Suparshvanath_Museum_Rietberg_RVI_306.jpg');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(2, 'West-hoek Ieper Museum', 'https://upload.wikimedia.org/wikipedia/commons/thumb/5/5b/De_Lakenhal_met_het_Belfort%2C_Ieper.jpg/399px-De_Lakenhal_met_het_Belfort%2C_Ieper.jpg');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(2, 'Loopgraven', 'https://upload.wikimedia.org/wikipedia/commons/thumb/4/4b/Dodengang.jpg/1024px-Dodengang.jpg');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(2, 'Menenpoort', 'https://upload.wikimedia.org/wikipedia/commons/thumb/3/3c/Menin_Gate.jpg/399px-Menin_Gate.jpg');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(2, 'Nieuwpoort', 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/Nieuwpoort_IJzer_R02.jpg/270px-Nieuwpoort_IJzer_R02.jpg');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(3, 'Sportdag: lopen', 'https://upload.wikimedia.org/wikipedia/commons/thumb/a/ae/1987WorldCupTrials.jpg/330px-1987WorldCupTrials.jpg');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(3, 'Sportdag: fietsen', 'https://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Two_siblings_on_their_way_home_after_school.jpg/330px-Two_siblings_on_their_way_home_after_school.jpg');
INSERT INTO dbo.Foto (fotoAlbumId, naamFoto, thumbnail) VALUES(3, 'Sportdag: slapen', 'https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Domenico_Fetti_-_Sleeping_Girl_-_WGA7863.jpg/330px-Domenico_Fetti_-_Sleeping_Girl_-_WGA7863.jpg');
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(4, 1);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(3, 2);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(5, 3);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(6, 3);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(7, 3);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(8, 3);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(9, 3);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(10, 3);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(2, 4);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(10, 5);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(1, 6);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(9, 6);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(5, 7);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(6, 7);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(7, 7);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(8, 7);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(9, 7);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(10, 7);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(3, 8);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(2, 8);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(7, 9);
INSERT INTO dbo.KlasStudiebezoek (klasId, studiebezoekId) VALUES(5, 10);
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(1, '9ed8c9dd-ef3b-4b1c-83bd-18343a92edc5');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(2, '9ed8c9dd-ef3b-4b1c-83bd-18343a92edc5');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(3, '741f8fb7-ca4d-4a0d-b468-51ed6ec847bb');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(3, '31f08605-16fc-4a44-bdd4-db02b747423f');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(3, 'ef8c1c81-6f72-45e7-b124-f35a18051738');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(3, '401b4e87-7535-4c5b-8d89-885688373b89');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(4, 'ef8c1c81-6f72-45e7-b124-f35a18051738');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(5, '741f8fb7-ca4d-4a0d-b468-51ed6ec847bb');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(6, '741f8fb7-ca4d-4a0d-b468-51ed6ec847bb');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(6, '31f08605-16fc-4a44-bdd4-db02b747423f');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(7, '9ed8c9dd-ef3b-4b1c-83bd-18343a92edc5');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(7, '741f8fb7-ca4d-4a0d-b468-51ed6ec847bb');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(7, '31f08605-16fc-4a44-bdd4-db02b747423f');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(7, 'ef8c1c81-6f72-45e7-b124-f35a18051738');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(8, '741f8fb7-ca4d-4a0d-b468-51ed6ec847bb');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(9, '401b4e87-7535-4c5b-8d89-885688373b89');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(10, '401b4e87-7535-4c5b-8d89-885688373b89');
INSERT INTO dbo.Begeleiding (studiebezoekId, gebruikerId) VALUES(10, '9ed8c9dd-ef3b-4b1c-83bd-18343a92edc5');