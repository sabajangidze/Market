-----------returns data-------------
 create function GetData(
     @CategoryName nvarchar(30) = null,
     @ProductName nvarchar(30) = null
)
returns table
as return
select P.ProductID, P.Name ProductName, C.Name CategoryName, P.Price
from Category C join Products P on C.CategoryID = P.CategoryID
where (C.Name = @CategoryName or @CategoryName is null)
and   (P.Name = @ProductName or @ProductName is null)

--------------Inports Data----------------
create proc InportData(
    @CategoryName nvarchar(30),
    @ProductName nvarchar(30),
    @Price money
)
as
    begin
        if not exists (select * from GetData (@CategoryName, @ProductName)) and
           exists (select * from GetData (default, @ProductName))
            begin
                declare @Error varchar(200) = 'Invalid category on Product => ' + @ProductName 
                raiserror(@Error, 16, 1)
                return 1
            end

        if not exists (select * from GetData (@CategoryName, default))
            insert into Category(Name) values (@CategoryName)

        if not exists (select * from GetData (default, @ProductName))
            insert into Products(name, price, categoryid) values (@ProductName, @Price, (select CategoryID from Category where Name = @CategoryName))
        else
            update Products set Price = @Price where Name = @ProductName
    end

