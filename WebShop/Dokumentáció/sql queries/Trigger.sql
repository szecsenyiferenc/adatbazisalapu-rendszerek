CREATE TRIGGER updateToRegularCustomer on Cart
AFTER INSERT
AS
BEGIN
	DECLARE @customerId varchar(MAX);
	DECLARE @customerCartNumber int;
		
	SELECT @customerId = carts.UserId from inserted carts;
	SELECT @customerCartNumber = dbo.getCartNumbers(@customerId);

	IF @customerCartNumber >= 5
       UPDATE Customer SET IsRegularCustomer = 1 WHERE Email = @customerId;
END
GO