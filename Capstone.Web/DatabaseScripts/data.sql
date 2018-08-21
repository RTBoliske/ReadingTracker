
INSERT INTO Family (Family_name) VALUES ('DeLaRosa');
INSERT INTO Family (Family_name) VALUES ('Boliske');
INSERT INTO Family (Family_name) VALUES ('Binegar');
INSERT INTO Family (Family_name) VALUES ('Lopez');

INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Austin', 'DeLaRosa', 1, 'adelarosa', 'FD7PS/mVScRRZ8jZXcg3xkOA8EE=', 'KlJGgDgFj8iaio+x1Bdp/Q==', 2);
INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Joseph', 'DeLaRosa', 1, 'jdelarosa', '9G0jGNMY2SjFAM4phOKNQElVOpE=', 'GjOVi5qyrpaFfLEwfnzJlQ==', 3);
INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Ted', 'Boliske', 2, 'tboliske', 'cQgdTq37FO2vDD+jCr4mhFWBePc=', 'VBZ1VOmoZ08HQ5PJOUHMxw==', 2);
INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Joe', 'Binegar', 3, 'jbinegar', 'ujf3h3kIuzWmJtr8Zti7YoPECN8=', 'bkkKEisi+ah7yJYD8s4tBg==', 2);
INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Nora', 'Young', 2, 'nyoung', 'cQgdTq37FO2vDD+jCr4mhFWBePc=', 'VBZ1VOmoZ08HQ5PJOUHMxw==', 2);
INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Dylan', 'Boliske', 3, 'dboliske', 'ujf3h3kIuzWmJtr8Zti7YoPECN8=', 'bkkKEisi+ah7yJYD8s4tBg==', 3);
INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Shakira', 'Binegar', 3, 'jbinegar', 'ujf3h3kIuzWmJtr8Zti7YoPECN8=', 'bkkKEisi+ah7yJYD8s4tBg==', 2);
INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Wes', 'Binegar', 3, 'd1binegar', 'ujf3h3kIuzWmJtr8Zti7YoPECN8=', 'bkkKEisi+ah7yJYD8s4tBg==', 3);
INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Sarah', 'Binegar', 3, 'd2binegar', 'ujf3h3kIuzWmJtr8Zti7YoPECN8=', 'bkkKEisi+ah7yJYD8s4tBg==', 3);
INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Chase', 'Binegar', 3, 'd3binegar', 'ujf3h3kIuzWmJtr8Zti7YoPECN8=', 'bkkKEisi+ah7yJYD8s4tBg==', 3);


INSERT INTO Book (FamilyID, Title, Author, ISBN) 
VALUES (1, 'Screwtape Letters', 'C.S. Lewis', '9781974042333');
INSERT INTO Book (FamilyID, Title, Author, ISBN) 
VALUES (1, 'An Infinity of Little Hours', 'Nancy Maguire', '9781283095358');
INSERT INTO Book (FamilyID, Title, Author, ISBN) 
VALUES (1, 'Catechism of the Council of Trent', 'Pius IV', '9780895558848');
INSERT INTO Book (FamilyID, Title, Author, ISBN) 
VALUES (3, 'Screwtape Letters', 'C.S. Lewis', '9781974042333');
INSERT INTO Book (FamilyID, Title, Author, ISBN) 
VALUES (3, 'An Infinity of Little Hours', 'Nancy Maguire', '9781283095358');
INSERT INTO Book (FamilyID, Title, Author, ISBN) 
VALUES (3, 'Catechism of the Council of Trent', 'Pius IV', '9780895558848');
INSERT INTO Book (FamilyID, Title, Author, ISBN) 
VALUES (2, 'Screwtape Letters', 'C.S. Lewis', '9781974042333');
INSERT INTO Book (FamilyID, Title, Author, ISBN) 
VALUES (2, 'An Infinity of Little Hours', 'Nancy Maguire', '9781283095358');
INSERT INTO Book (FamilyID, Title, Author, ISBN) 
VALUES (2, 'Catechism of the Council of Trent', 'Pius IV', '9780895558848');

INSERT INTO ReadingLog (BookId, Type, UserID, Minutes_read, Status, Date)
VALUES (1, 'Paper', 1, 65, 'Active','08/17/2018');
INSERT INTO ReadingLog (BookId, Type, UserID, Minutes_read, Status, Date)
VALUES (1, 'Paper', 1, 35, 'Active','08/20/2018');
INSERT INTO ReadingLog (BookId, Type, UserID, Minutes_read, Status, Date)
VALUES (1, 'Paper', 1, 95, 'Active','08/19/2018');
INSERT INTO ReadingLog (BookId, Type, UserID, Minutes_read, Status, Date)
VALUES (1, 'Paper', 4, 65, 'Active','08/17/2018');
INSERT INTO ReadingLog (BookId, Type, UserID, Minutes_read, Status, Date)
VALUES (2, 'Paper', 4, 35, 'Active','08/20/2018');
INSERT INTO ReadingLog (BookId, Type, UserID, Minutes_read, Status, Date)
VALUES (3, 'Paper', 4, 95, 'Active','08/19/2018');

INSERT INTO Prize (UserType, Goal, MaxNumPrize, isActive, StartDate, EndDate, FamilyID)
VALUES (2, 300, 0, 1, '01/01/2018', '12/30/2018', 1);
INSERT INTO Prize (UserType, Goal, MaxNumPrize, isActive, StartDate, EndDate, FamilyID)
VALUES (3, 300, 0, 1, '01/01/2018', '12/30/2018', 1);
INSERT INTO Prize (UserType, Goal, MaxNumPrize, isActive, StartDate, EndDate, FamilyID)
VALUES (3, 300, 0, 1, '01/01/2018', '12/30/2018', 3);
INSERT INTO Prize (UserType, Goal, MaxNumPrize, isActive, StartDate, EndDate, FamilyID)
VALUES (3, 300, 0, 1, '01/01/2018', '12/30/2018', 3);
INSERT INTO Prize (UserType, Goal, MaxNumPrize, isActive, StartDate, EndDate, FamilyID)
VALUES (2, 300, 0, 1, '01/01/2018', '12/30/2018', 2);
INSERT INTO Prize (UserType, Goal, MaxNumPrize, isActive, StartDate, EndDate, FamilyID)
VALUES (2, 300, 0, 1, '01/01/2018', '12/30/2018', 2);
INSERT INTO Prize (UserType, Goal, MaxNumPrize, isActive, StartDate, EndDate, FamilyID)
VALUES (2, 300, 0, 1, '01/01/2018', '12/30/2018', 3);
INSERT INTO Prize (UserType, Goal, MaxNumPrize, isActive, StartDate, EndDate, FamilyID)
VALUES (2, 300, 0, 1, '01/01/2018', '12/30/2018', 3);