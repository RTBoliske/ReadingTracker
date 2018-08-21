USE [master];
GO

BEGIN TRY
	DROP DATABASE [dbfamilyreader];
END TRY
BEGIN CATCH
END CATCH
GO

CREATE DATABASE [dbfamilyreader];
GO

USE [dbfamilyreader]

CREATE TABLE Users(
	ID int IDENTITY(1,1),
	First_name varchar(50) NOT NULL,
	Last_name varchar(50) NOT NULL,
	FamilyID int NOT NULL,
	Username varchar(50) NOT NULL UNIQUE,
	Password varchar(50) NOT NULL,
	Salt varchar(50) NOT NULL,
	RoleID int NOT NULL,
	CONSTRAINT pk_user_ID PRIMARY KEY (ID),
	CONSTRAINT uc_users UNIQUE (Username),
);

CREATE TABLE Family(
	ID int Identity(1,1),
	Family_name varchar(50) NOT NULL,
	CONSTRAINT pk_family_ID PRIMARY KEY (ID),
);

CREATE TABLE Roles(
	ID int Identity(1,1),
	Role varchar(50) NOT NULL,
	CONSTRAINT pk_roles_ID PRIMARY KEY (ID),
);

CREATE TABLE Book(
	ID int Identity(1,1),
	FamilyID int NOT NULL,
	Title varchar(100) NOT NULL,
	Author varchar (100) NOT NULL,
	ISBN varchar (20) NOT NULL,
	CONSTRAINT pk_book_ID PRIMARY KEY (ID),
);

CREATE TABLE ReadingLog(
	ID int Identity(1,1),
	BookID int NOT NULL,
	UserID int NOT NULL,
	Minutes_read int NOT NULL,
	Status varchar (15) NOT NULL,
	Type varchar (50) NOT NULL,
	Date date NOT NULL,
);

CREATE TABLE Prize(
	ID int Identity(1,1),
	UserType int NOT NULL,
	Goal int NOT NULL,
	MaxNumPrize int,
	isActive bit NOT NULL,
	StartDate Date,
	EndDate Date,
	FamilyID int NOT NULL,
);

ALTER TABLE Users ADD FOREIGN KEY (FamilyID) REFERENCES Family(ID);
ALTER TABLE Users ADD FOREIGN KEY (RoleID) REFERENCES Roles(ID);
ALTER TABLE Book ADD FOREIGN KEY (FamilyID) REFERENCES Family(ID);
ALTER TABLE ReadingLog ADD FOREIGN KEY (BookID) REFERENCES Book(ID);
ALTER TABLE ReadingLog ADD FOREIGN KEY (UserID) REFERENCES Users(ID);
ALTER TABLE Prize ADD FOREIGN KEY (FamilyID) REFERENCES Family(ID);

INSERT INTO Roles (Role) VALUES ('Administrator');
INSERT INTO Roles (Role) VALUES ('Parent');
INSERT INTO Roles (Role) VALUES ('Child');
