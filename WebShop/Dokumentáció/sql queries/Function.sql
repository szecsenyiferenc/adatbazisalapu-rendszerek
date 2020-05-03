IF OBJECT_ID (N'dbo.getCartNumbers', N'FN') IS NOT NULL  
    DROP FUNCTION dbo.getCartNumbers;  
GO  
CREATE FUNCTION dbo.getCartNumbers(@UserID varchar(MAX))  
RETURNS int   
AS   
BEGIN  
    DECLARE @ret int;  
    SELECT @ret = COUNT(*)   
    FROM Cart
    WHERE UserId = @UserID;
    IF (@ret IS NULL)   
        SET @ret = 0;  
    RETURN @ret;  
END; 