DROP TABLE Passports;
DROP TABLE Employees;
DROP TABLE Departments;

CREATE TABLE Departments (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(100),
  Phone VARCHAR(100),
);
CREATE TABLE Employees(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100),
    Surname VARCHAR(100),
	Phone VARCHAR(100),
	CompanyId INT,
	DepartmentId INT,
	FOREIGN KEY (DepartmentId) REFERENCES Departments(id) ON DELETE CASCADE,
	
);
CREATE TABLE Passports (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT,
    Type VARCHAR(100),
	Number VARCHAR(100),
	FOREIGN KEY (EmployeeId) REFERENCES Employees(id) ON DELETE CASCADE,
);

INSERT INTO Departments (Name,Phone)
    VALUES
	('HR','3151351531'),
	('Develop','41313513531'),
	('Analytic','32423432432')