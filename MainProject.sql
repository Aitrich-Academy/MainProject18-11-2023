CREATE DATABASE MainProject
USE MainProject


------------------------------------------------------------------------------------------------------------------
----------------------------------------------------Users Table---------------------------------------------------
CREATE TABLE UsersRegister (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(500) UNIQUE,
    PhoneNumber BIGINT,
    District VARCHAR(100) NOT NULL,
    Pincode BIGINT NOT NULL,
    PasswordHash NVARCHAR(25) NOT NULL,
	Status varchar(100)
);
select *from UsersRegister

------------------------------------------------------------------------------------------------------------------
----------------------------------------------------Category Table------------------------------------------------
CREATE TABLE ProductCategorys (
    CategoryID INT IDENTITY(100,1) PRIMARY KEY,
    Category_Name VARCHAR(50) NOT NULL,
	Status varchar(100),
);
select *from ProductCategorys


------------------------------------------------------------------------------------------------------------------
----------------------------------------------------Products Table------------------------------------------------
CREATE TABLE Products (
    ProductID INT IDENTITY(200,1) PRIMARY KEY,
    Product_Name VARCHAR(500) NOT NULL,
	Price DECIMAL NOT NULL,
    Image VARCHAR(500) NOT NULL,
    Category_id int,
	Status varchar(100),
	foreign key(Category_id) references ProductCategorys(CategoryID)
);
select *from Products

----------------------------------------------------Orders Table---------------------------------------------------
-------------------------------------------------------------------------------------------------------------------
CREATE TABLE Orders (
    OrderID INT IDENTITY(300,1) PRIMARY KEY,
	User__id INT,
	Category_id int,
	Product_id INT,
    Product_Name VARCHAR(50) NOT NULL,
	Price DECIMAL NOT NULL,
	Quantity INT NOT NULL,
	Total_Price INT NOT NULL,
	Image VARCHAR(500) NOT NULL,
	OrderDate DATETIME NOT NULL,
	Status varchar(100),
	foreign key(User__id) references UsersRegister(UserID),
	foreign key(Category_id) references ProductCategorys(CategoryID),
	foreign key(Product_id) references Products(ProductID)
);
select *from Orders