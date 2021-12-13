CREATE DATABASE aao;
USE aao
GO;
-- Tables 
CREATE TABLE UserTypes (
    UserTypeId INT IDENTITY(1, 1),
    UserTypeName VARCHAR(10) UNIQUE,
    PRIMARY KEY (UserTypeId)
);
CREATE TABLE Users (
    UserId INT IDENTITY(1, 1),
    UserTypeId INT,
    UserEmail VARCHAR(255) UNIQUE,
    UserPassword VARCHAR(255),
    UserFullName VARCHAR(255),
    UserPhoneNumber VARCHAR(20),
    PRIMARY KEY (UserId),
    FOREIGN KEY (UserTypeId) REFERENCES UserTypes(UserTypeId)
);
CREATE TABLE Countries (
    CountryId INT IDENTITY(1, 1),
    -- ISO 3166
    CountryName VARCHAR(3) UNIQUE,
    PRIMARY KEY (CountryId)
);
CREATE TABLE Cities (
    CityId INT IDENTITY(1, 1),
    CityName VARCHAR(255),
    CountryId INT,
    PRIMARY KEY (CityId),
    FOREIGN KEY (CountryId) REFERENCES Countries(CountryId)
);
CREATE TABLE Locations (
    LocationId INT IDENTITY(1, 1),
    LocationAddress VARCHAR(255),
    LocationPostalCode VARCHAR(10),
    CityId INT,
    PRIMARY KEY (LocationId),
    FOREIGN KEY (CityId) REFERENCES Cities(CityId)
);
CREATE TABLE LicenceTypes (
    LicenceTypeId INT IDENTITY(1, 1),
    LicenceTypeName VARCHAR(20) UNIQUE,
    PRIMARY KEY (LicenceTypeId)
);
CREATE TABLE Licences (
    LicenceId INT IDENTITY(1, 1),
    LicenceTypeId INT,
    LicenceImage VARCHAR(255),
    LicenceExpirationDate DATE,
    PRIMARY KEY (LicenceId),
    FOREIGN KEY (LicenceTypeId) REFERENCES LicenceTypes(LicenceTypeId)
);
CREATE TABLE DriverInformation (
    DriverInformationId INT IDENTITY(1, 1),
    UserId INT UNIQUE,
    LocationId INT,
    DriverLicenceId INT UNIQUE,
    LorryLicenceId INT UNIQUE,
    EUCertificate INT UNIQUE,
    PRIMARY KEY (DriverInformationId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (LocationId) REFERENCES Locations(LocationId),
    FOREIGN KEY (DriverLicenceId) REFERENCES Licences(LicenceId),
    FOREIGN KEY (LorryLicenceId) REFERENCES Licences(LicenceId),
    FOREIGN KEY (EUCertificate) REFERENCES Licences(LicenceId)
);
CREATE TABLE Departments (
    DepartmentId INT IDENTITY(1, 1),
    DepartmentName VARCHAR(255) UNIQUE,
    DepartmentContactNumber VARCHAR(20),
    DepartmentEmail VARCHAR(255),
    PRIMARY KEY (DepartmentId)
);
CREATE TABLE RouteStatus (
    RouteStatusId INT IDENTITY(1, 1),
    RouteStatusName VARCHAR(50) UNIQUE,
    PRIMARY KEY (RouteStatusId)
);
CREATE TABLE Routes (
    RouteId INT IDENTITY(1, 1),
    RouteDescription TEXT,
    RouteStartDate DATETIME,
    RouteEndDate DATETIME,
    RouteStartLocationId INT,
    RouteEndLocationId INT,
    RouteHighPriority BIT,
    RouteStatusId INT,
    UserId INT NULL,
    DepartmentId INT,
    RouteEstTime INT,
    PRIMARY KEY (RouteId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RouteStatusId) REFERENCES RouteStatus(RouteStatusId),
    FOREIGN KEY (RouteStartLocationId) REFERENCES Locations(LocationId),
    FOREIGN KEY (RouteEndLocationId) REFERENCES Locations(LocationId),
    FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId)
);
CREATE TABLE DriversAvailable (
    DriversAvailableId INT IDENTITY(1, 1),
    UserId INT NOT NULL,
    DriversAvailableDate DATE NOT NULL,
    PRIMARY KEY (DriversAvailableId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
CREATE TABLE SignUpDrivers (
    UserId INT NOT NULL,
    RouteId INT NOT NULL,
    PRIMARY KEY (UserId, RouteId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RouteId) REFERENCES Routes(RouteId)
);
-- Dummy data 
-- UserTypes
INSERT INTO UserTypes
VALUES ('admin');
INSERT INTO UserTypes
VALUES ('driver');
-- Users
INSERT INTO Users
VALUES (
        1,
        'mh@cavea.io',
        'mh@cavea.io',
        'Mads Cavea',
        '34892311'
    );
INSERT INTO Users
VALUES (
        2,
        'mads@mhouge.dk',
        'mads@mhouge.dk',
        'Mads Privat',
        '20974430'
    );
-- Countries 
INSERT INTO Countries
VALUES ('DNK');
INSERT INTO Countries
VALUES ('GER');
INSERT INTO Countries
VALUES ('NOR');
-- Cities 
INSERT INTO Cities
VALUES('Odense', 1);
INSERT INTO Cities
VALUES('Nykøbing Falster', 1);
INSERT INTO Cities
VALUES('Berlin', 2);
INSERT INTO Cities
VALUES('Dortmund', 2);
INSERT INTO Cities
VALUES('Oslo', 3);
INSERT INTO Cities
VALUES('Trundheim', 3);
-- Locations 
INSERT INTO Locations
VALUES ('Kranvejen 77', '5000', 1);
INSERT INTO Locations
VALUES ('Guldborgsundsvej 14', '4800', 2);
INSERT INTO Locations
VALUES ('Nyborgvej 12', '8210', 3);
INSERT INTO Locations
VALUES ('Kettingestræde 54', '7000', 2);
-- LicenceTypes
INSERT INTO LicenceTypes
VALUES ('driver');
INSERT INTO LicenceTypes
VALUES ('lorry');
INSERT INTO LicenceTypes
VALUES ('eu');
-- Licences
INSERT INTO Licences
VALUES (1, '', CURRENT_TIMESTAMP);
INSERT INTO Licences
VALUES (2, '', CURRENT_TIMESTAMP);
INSERT INTO Licences
VALUES (3, '', CURRENT_TIMESTAMP);
-- DriverInformation
INSERT INTO DriverInformation
VALUES(2, 1, 1, 2, 3);
-- Departments
INSERT INTO Departments
VALUES ('Odense', '+45 32849291', 'odense@aao.dk');
INSERT INTO Departments
VALUES ('Aarhus', '+45 32841291', 'aarhus@aao.dk');
INSERT INTO Departments
VALUES ('Greve', '+45 32884291', 'greve@aao.dk');
INSERT INTO Departments
VALUES ('Norway', '+46 32882391', 'norway@aao.dk');
INSERT INTO Departments
VALUES ('Sweden', '+47 32852391', 'sweden@aao.dk');
INSERT INTO Departments
VALUES (
        'Netherlands',
        '+48 32822391',
        'netherlands@aao.dk'
    );
-- RouteStatus
INSERT INTO RouteStatus
VALUES ('missing-driver');
INSERT INTO RouteStatus
VALUES ('pending');
INSERT INTO RouteStatus
VALUES ('completed');
-- Routes 
INSERT INTO Routes
VALUES (
        'This is the description',
        CURRENT_TIMESTAMP,
        CURRENT_TIMESTAMP,
        1,
        2,
        0,
        1,
        null,
        1,
        8
    );
--DriversAvailable--
INSERT INTO DriversAvailable
VALUES (1, CURRENT_TIMESTAMP);
--SignUpDrivers--
INSERT INTO SignUpDrivers
VALUES (1, 1);
--DriversInfo--
INSERT INTO DriverInformation (UserId, LocationId)
VALUES (1, 1);
-- DriversAvailable info --
SELECT Users.UserId,
    Users.UserFullName,
    Users.UserPhoneNumber,
    Users.UserEmail,
    DriversAvailable.DriversAvailableDate,
    CONCAT (
        Locations.LocationAddress,
        ', ',
        Locations.LocationPostalCode,
        ' ',
        Cities.CityName,
        ', ',
        Countries.CountryName
    ) AS [Location]
FROM Users
    INNER JOIN DriversAvailable ON Users.UserId = DriversAvailable.UserId
    LEFT JOIN DriverInformation ON Users.UserId = DriverInformation.UserId
    INNER JOIN Locations ON DriverInformation.LocationId = Locations.LocationId
    INNER JOIN Cities ON Locations.CityId = Cities.CityId
    INNER JOIN Countries ON Cities.CountryId = Countries.CountryId;