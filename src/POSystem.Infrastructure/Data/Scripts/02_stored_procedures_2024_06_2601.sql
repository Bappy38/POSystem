CREATE PROCEDURE spGetAllOrders
AS
BEGIN
    SELECT * FROM Orders;
END;
GO

CREATE PROCEDURE spGetOrderById
    @OrderId INT
AS
BEGIN
    SELECT * FROM Orders WHERE Id = @OrderId;
END;
GO

CREATE PROCEDURE spGetOrderByIdWithLineItems
    @OrderId INT
AS
BEGIN
    SELECT 
        o.Id AS OrderId,
        o.ReferenceId,
        o.PlacedAtUtc,
        o.ExpectedDate,
        o.Remark,
        s.Id AS SupplierId,
        s.Name AS SupplierName,
        s.Email AS SupplierEmail,
        s.Phone AS SupplierPhone,
        s.Address AS SupplierAddress,
        li.Id AS LineItemId,
        li.Name AS ItemName,
        li.Quantity,
        li.Rate
    FROM Orders o
    INNER JOIN Suppliers s ON o.SupplierId = s.Id
    LEFT JOIN LineItems li ON o.Id = li.OrderId
    WHERE o.Id = @OrderId;
END;
GO

CREATE PROCEDURE spAddOrder
    @ReferenceId NVARCHAR(50),
    @PlacedAtUtc DATETIME,
    @SupplierId INT,
    @ExpectedDate DATETIME = NULL,
    @Remark NVARCHAR(MAX) = NULL
AS
BEGIN
    INSERT INTO Orders
        (ReferenceId, PlacedAtUtc, SupplierId, ExpectedDate, Remark)
    VALUES
        (@ReferenceId, @PlacedAtUtc, @SupplierId, @ExpectedDate, @Remark);
END;
GO

CREATE TYPE dbo.LineItemType AS TABLE
(
    Id INT,
    OrderId INT,
    Name NVARCHAR(255),
    Quantity INT,
    Rate DECIMAL(18, 2)
);
GO

CREATE PROCEDURE spUpdateOrder
    @OrderId INT,
    @ReferenceId NVARCHAR(50),
    @PlacedAtUtc DATETIME,
    @SupplierId INT,
    @ExpectedDate DATETIME = NULL,
    @Remark NVARCHAR(MAX) = NULL,
    @LineItems dbo.LineItemType READONLY
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        UPDATE Orders
        SET
            ReferenceId = @ReferenceId,
            PlacedAtUtc = @PlacedAtUtc,
            SupplierId = @SupplierId,
            ExpectedDate = @ExpectedDate,
            Remark = @Remark
        WHERE Id = @OrderId;

        DELETE FROM LineItems WHERE OrderId = @OrderId;

        INSERT INTO LineItems (Id, OrderId, Name, Quantity, Rate)
        SELECT Id, OrderId, Name, Quantity, Rate
        FROM @LineItems;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO

CREATE PROCEDURE spDeleteOrder
    @OrderId INT
AS
BEGIN
    DELETE FROM Orders WHERE Id = @OrderId;
END;
GO