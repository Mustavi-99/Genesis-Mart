Create Database GenesisMart

create table Customer(
	CUSEmail varchar(50) primary key NOT NULL,
	CUSName varchar(20) NOT NULL,
	CUSPassword varchar (20) NOT NULL,
	CUSContactNo  varchar(11) NULL,
	CUSAddress  varchar(100) NULL
)
drop table Customer
Create table Product(
	PRID int primary key identity(1000,1),
	PRName varchar(20),
	PRImage varchar(50),
	PRType varchar(20),
	PRDescription varchar(100),
	PRPrize Decimal(8,2),
	PRRating Numeric (3,2),
	PRQuantity int
)

SELECT * FROM Customer

SELECT * FROM Product

Insert into Customer(CUSEmail,CUSName,CUSPassword) 
Values('mustavi.sadim99@gmail.com','mustavi','12345')


Insert into Customer
Values('sanjay39@gmail.com','sanjay','123456')