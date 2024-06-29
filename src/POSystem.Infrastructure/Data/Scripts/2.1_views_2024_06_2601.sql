CREATE VIEW vwOrdersWithSupplierName AS
SELECT 
    o.Id,
    o.ReferenceId,
    o.PurchaseOrderNo,
    o.PlacedAtUtc,
    o.SupplierId,
    o.ExpectedDate,
    o.Remark,
    s.Name AS SupplierName
FROM Orders o
INNER JOIN Suppliers s ON o.SupplierId = s.Id;