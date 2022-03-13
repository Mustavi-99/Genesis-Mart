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

Insert into Product(PRName,PRImage,PRType,PRDescription,PRPrize,PRRating,PRQuantity)
Values('Elden Ring','EldenRing.jpeg','Games','Elden Ring is an action role-playing game developed by FromSoftware and published by Bandai Namco Entertainment. The game was made in collaboration with fantasy novelist George R. R. Martin, who provided material for the games setting.',4000,5,8),
('Forza Horizon 5','FZ5.jpg','Games','Forza Horizon 5 is a 2021 racing video game developed by Playground Games and published by Xbox Game Studios. It is the twelfth main instalment in the Forza series and the game is set in a fictionalised representation of Mexico.',3500,5,8),
('God Of War Ragnarok','GowRagnar.jpg','Games','God of War Ragnarök is an action-adventure hack and slash video game developed by Santa Monica Studio and will be published by Sony Interactive Entertainment. It is scheduled to be released in 2022 for the PlayStation 4 and PlayStation 5.',5000,5,8),
('Ori and The Blind Forest','Ori.jpg','Games','Ori and the Blind Forest is a platform-adventure Metroidvania video game developed by Moon Studios and published by Microsoft Studios. The game was released for Xbox One and Microsoft Windows in March 2015, and for Nintendo Switch in September 2019.',4000,5,8)

Insert into Product(PRName,PRImage,PRType,PRDescription,PRPrize,PRRating,PRQuantity)
Values('Steam Deck','steamDeck.jpg','Console','The Steam Deck is a handheld gaming computer developed by Valve and Advanced Micro Devices, released on February 25, 2022. The Steam Deck can be played as a handheld or connected to a monitor in the same manner as the Nintendo Switch.',40000,5,8),
('Horizon Zero Dawn','HZD.jpg','Games','Horizon Zero Dawn is a 2017 action role-playing game developed by Guerrilla Games and published by Sony Interactive Entertainment. The plot follows Aloy, a young huntress in a world overrun by machines, who sets out to uncover her past.',2000,5,8),
('Nintendo Switch','switch.jpg','Console','The Nintendo Switch – OLED Model and Nintendo Switch systems transform from home console to handheld, letting you play your favorite games at home or on the go. They include a dock for connecting to your TV, as well as removable Joy-Con™ controllers that you can share with a friend for two-player fun.',50000,5,8),
('Windows','win10.jpg','Software','Windows 10 is a major release of Microsofts Windows NT operating system. It is the direct successor to Windows 8.1, which was released nearly two years earlier. It was released to manufacturing on July 15, 2015, and later to retail on July 29, 2015.',13000,5,8),
('PhotoShop','ps.jpg','Software','Adobe Photoshop is a raster graphics editor developed and published by Adobe Inc. for Windows and macOS. It was originally created in 1988 by Thomas and John Knoll. Since then, the software has become the industry standard not only in raster graphics editing, but in digital art as a whole.',7000,4.8,8)





Create table ContactUs(
	ContactID int primary key identity(10000,1),
	FullName varchar(50) NOT NULL,
	Email varchar(50) NOT NULL,
	ContactMessage varchar(5000) NOT NULL
)
drop table ContactUs

Create table OrderList(
	OrderID int primary key identity(1,1),
	itemName varchar(500) NOT NULL,
	totalPrice int  Not Null,
	cusEmail varchar(500) Not Null
)
drop table OrderList

Create table Comment(
CommentID int primary key identity(100,1),
ProductID int Not Null,
CustName varchar (50) Not Null,
CommentMessage varchar(5000) Not Null
)
Select * from Product
Select * from Product where PRType = 'Games'
Select * from Product where PRType = 'Games' order by PRID ASC LIMIT 1

SELECT * FROM 
(
    SELECT TOP 1 *, ROW_NUMBER OVER (ORDER BY (SELECT 1)) AS rnum 
    FROM Product
) a
WHERE rnum > 0

SELECT * FROM Product where PRType = 'Games' order by PRID OFFSET 0 rows FETCH FIRST 2 ROWS ONLY
Select * from Product order by PRID OFFSET 1 rows FETCH FIRST 1 ROWS ONLY
drop table Comment