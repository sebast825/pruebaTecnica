create procedure getOrderByiD
@id int

AS
BEGIN
	IF NOT EXISTS (SELECT * FROM Orders WHERE OrderID = @id)
	 
	BEGIN
	RAISERROR('La orden no existe ', 16,1)
	return
	END
	ELSE 
	BEGIN
	PRINT 'La orden existe'
	end

		select CompanyName, Address from customers
	inner join Orders
	on orders.CustomerID = Customers.CustomerID
	where orders.OrderID = @id

	select Products.ProductName, Products.UnitPrice, od.Quantity , (select count(*) from [Order Details] where   orderId != @id and  ProductID = Products.ProductID  ) as CountProductoEnOrdenesDistitnas from [Order Details] as od
	inner join orders
	on orders.OrderID = od.OrderID
	inner join Products 
	on od.ProductID = Products.ProductID	
	group by Products.ProductID,Products.ProductName, Products.UnitPrice,orders.OrderID,  od.Quantity
	having orders.OrderID = @id

END



