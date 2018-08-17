
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

INSERT INTO Book (FamilyID, UserID, Title, Author, ISBN, Type, Complete, isActive) 
VALUES (1, 1, 'Screwtape Letters', 'C.S. Lewis', '9781974042333', 'Paper', 0, 1);
INSERT INTO Book (FamilyID, UserID, Title, Author, ISBN, Type, Complete, isActive) 
VALUES (1, 1, 'An Infinity of Little Hours', 'Nancy Maguire', '9781283095358', 'Paper', 1, 0);
INSERT INTO Book (FamilyID, UserID, Title, Author, ISBN, Type, Complete, isActive) 
VALUES (1, 1, 'Catechism of the Council of Trent', 'Pius IV', '9780895558848', 'Paper', 0, 1);

INSERT INTO ReadingLog (BookID, UserID, FamilyID, Minutes_read, Complete, Date)
VALUES (1, 1, 1, 65, 0, '08/17/2018');
