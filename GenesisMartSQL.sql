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
	PRName varchar(50),
	PRImage varchar(50),
	PRType varchar(20),
	PRDescription varchar(8000),
	PRPrize Decimal(8,2),
	PRRating Numeric (3,2),
	PRQuantity int
)
drop table Product
SELECT * FROM Customer

SELECT * FROM Product

Insert into Customer(CUSEmail,CUSName,CUSPassword) 
Values('mustavi.sadim99@gmail.com','mustavi','12345')


Insert into Customer
Values('sanjay152@gmail.com','sanjay','123456')

Insert into Product(PRName,PRImage,PRType,PRDescription,PRPrize,PRRating,PRQuantity)
Values('God of war','gow.jpg','Games','God of War (also known as God of War IV) is the sequel to God of War III as well as a continuation of the canon God of War chronology. God of War is the eighth installment in the franchise overall.Unlike previous installments, this game focuses on Norse mythology and follows an older and more seasoned Kratos and his new son Atreus in the years since God of War III. The game released on April 20, 2018, and is currently exclusive to the PlayStation 4.',5000.00,5,5),
	('Red Dead Redemption 2','rdr2.jpg','Games','After a robbery goes badly wrong in the western town of Blackwater, Arthur Morgan and the Van der Linde gang are forced to flee. With federal agents and the best bounty hunters in the nation massing on their heels, the gang must rob, steal and fight their way across the rugged heartland of America in order to survive.',4000.00,4.40,6),
	('Play Station 5','ps5.jpg','Console','The latest Sony PlayStation introduced in November 2020. Powered by an eight-core AMD Zen 2 CPU and custom AMD Radeon GPU, the PS5 is offered in two models: with and without a 4K Blu-ray drive. Supporting a 120Hz video refresh, the PS5 is considerably more powerful than the PS4 and PS4 Pro.',75000.00,4.39,4),
	('Xbox Series X','xbox.png','Console','The Xbox Series X has higher end hardware and supports higher display resolutions (up to 8K resolution), along with higher frame rates and real-time ray tracing; it also has a high-speed solid-state drive to reduce loading times.',60000.00,4.53,5),
	('Microsoft Windows 11','win11.jpg','Software','Windows 11 is the latest major release of the Windows NT operating system developed by Microsoft that was announced on June 24, 2021, and is the successor to Windows 10, which was released in 2015.',13000,0,7),
	('ESET Antivirus Security','eset.jpg','Software','Explore the great online, securely protected by ESET’s award-winning detection technology. It’s trusted by over 110 million users worldwide to detect and neutralize all types of digital threats, including viruses, rootkits, worms and spyware. It also protects against techniques that seek to evade detection, and blocks targeted attacks and exploits.',1000,3.6,8)
