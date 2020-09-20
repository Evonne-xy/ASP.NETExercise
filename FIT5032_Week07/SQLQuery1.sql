--creating table 'Students'
Create table [dbo].[Students](
 [Id] int Identity(1,1) not null,
 [FirstName] nvarchar(max) not null,
 [LastName] nvarchar(max) Not null,
 [UserId] nvarchar(max) Not null

 primary key (Id)
);
Go

--creating table 'Units'
Create table [dbo].[Units](
 [Id] int Identity(1,1) not null,
 [Name] nvarchar(max) not null,
 [Description] nvarchar(max) Not null,
 [StudentId] int not null
 primary key (Id)
 Foreign key (studentId) references students(Id)
);
Go