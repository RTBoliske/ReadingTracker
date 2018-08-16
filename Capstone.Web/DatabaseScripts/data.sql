
INSERT INTO Family (Family_name) VALUES ('DeLaRosa');
INSERT INTO Family (Family_name) VALUES ('Boliske');
INSERT INTO Family (Family_name) VALUES ('Binegar');
INSERT INTO Family (Family_name) VALUES ('Lopez');

INSERT INTO Roles (Role) VALUES ('Administrator');
INSERT INTO Roles (Role) VALUES ('Parent');
INSERT INTO Roles (Role) VALUES ('Child');

INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Austin', 'DeLaRosa', 1, 'adelarosa', 'FD7PS/mVScRRZ8jZXcg3xkOA8EE=', 'KlJGgDgFj8iaio+x1Bdp/Q==', 2);

INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Domingo', 'Lopez', 4, 'dlopez', '1234asdf', 'dlopez', 2);

INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Ted', 'Boliske', 2, 'tboliske', '1234asdf', 'tboliske', 2);

INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
VALUES ('Joe', 'Binegar', 3, 'jbinegar', '1234asdf', 'jbinegar', 2);
