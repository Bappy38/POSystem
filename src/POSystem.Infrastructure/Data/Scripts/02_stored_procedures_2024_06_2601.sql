﻿CREATE PROCEDURE spGetAllOrders
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
        o.Id,
        o.ReferenceId,
        o.PlacedAtUtc,
        o.ExpectedDate,
        o.Remark,
        o.SupplierId,
        li.Id AS LineItemId,
        li.OrderId,
        li.Name,
        li.Quantity,
        li.Rate
    FROM Orders o
    INNER JOIN LineItems li ON o.Id = li.OrderId
    WHERE o.Id = @OrderId;
END;
GO

CREATE PROCEDURE spGetPagedOrders
    @CursorId INT = NULL,
    @PageSize INT,
    @Search NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@PageSize)
        Id,
        ReferenceId,
        PlacedAtUtc,
        SupplierId,
        ExpectedDate,
        Remark
    FROM Orders
    WHERE (@CursorId IS NULL OR Id > @CursorId)
      AND (@Search IS NULL OR ReferenceId LIKE '%' + @Search + '%')
    ORDER BY Id;
END;
GO


CREATE TYPE dbo.AddLineItemType AS TABLE
(
    Name NVARCHAR(255),
    Quantity INT,
    Rate DECIMAL(18, 2)
);
GO

CREATE PROCEDURE spAddOrder
    @ReferenceId NVARCHAR(50),
    @PlacedAtUtc DATETIME,
    @SupplierId INT,
    @ExpectedDate DATETIME = NULL,
    @Remark NVARCHAR(MAX) = NULL,
    @LineItems dbo.AddLineItemType READONLY
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO Orders (ReferenceId, PlacedAtUtc, SupplierId, ExpectedDate, Remark)
        VALUES (@ReferenceId, @PlacedAtUtc, @SupplierId, @ExpectedDate, @Remark);

        DECLARE @OrderId INT;
        SET @OrderId = SCOPE_IDENTITY();

        INSERT INTO LineItems (OrderId, Name, Quantity, Rate)
        SELECT @OrderId, Name, Quantity, Rate
        FROM @LineItems;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH;
END;
GO

CREATE TYPE dbo.UpdateLineItemType AS TABLE
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
    @LineItems dbo.UpdateLineItemType READONLY
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

        INSERT INTO LineItems (OrderId, Name, Quantity, Rate)
        SELECT OrderId, Name, Quantity, Rate
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