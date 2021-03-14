create database Market
go

use Market
go

create table Category(
    CategoryID int PRIMARY KEY identity,
    Name nvarchar(30)
)
go

create table Products(
    ProductID int primary key identity,
    Name nvarchar(30),
    Price money,
    CategoryID int,
    foreign key (CategoryID) references Category(CategoryID)
)
go

------------------insert data--------------------------
insert into Category(Name) values('Toys')
insert into Category(Name) values('Food')
insert into Products(name, price, categoryid) values ('car', 10.5, 1)
insert into Products(name, price, categoryid) values ('Train', 20, 1)
insert into Products(name, price, categoryid) values ('Ball', 15, 1)
insert into Products(name, price, categoryid) values ('Lobiani', 2, 2)
insert into Products(name, price, categoryid) values ('Xachapuri', 3, 2)