﻿using Dapper;
using POSystem.Domain.DTOs;
using POSystem.Domain.Entities;
using POSystem.Domain.Repositories;
using POSystem.Infrastructure.Data;
using System.Data;

namespace POSystem.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbContext _context;

    public OrderRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();

            var orderDictionary = new Dictionary<int, Order>();

            var result = await connection.QueryAsync<Order, LineItem, Order>(
                "spGetOrderByIdWithLineItems",
                (order, lineItem) =>
                {
                    if (!orderDictionary.TryGetValue(order.Id, out var orderEntry))
                    {
                        orderEntry = order;
                        orderEntry.Items = new List<LineItem>();
                        orderDictionary.Add(orderEntry.Id, orderEntry);
                    }

                    orderEntry.Items.Add(lineItem);

                    return orderEntry;
                },
                new
                {
                    OrderId = id
                },
                splitOn: "LineItemId",
                commandType: CommandType.StoredProcedure);

            return orderDictionary.Values.FirstOrDefault();
        }
    }

    public async Task<List<GetOrderDto>> GetPagedAsync(int cursor, int pageSize)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();

            var parameters = new DynamicParameters();
            parameters.Add("CursorId", cursor);
            parameters.Add("PageSize", pageSize);

            var orders = await connection.QueryAsync<GetOrderDto>("spGetPagedOrders", parameters, commandType: CommandType.StoredProcedure);
            return orders?.ToList() ?? new List<GetOrderDto>();
        }
    }

    public async Task<PaginatedList<GetOrderDto>> GetPagedAsync(int pageNo, int pageSize, string searchQuery)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();

            var parameters = new
            {
                PageNo = pageNo,
                PageSize = pageSize,
                Search = searchQuery
            };

            using (var multi = await connection.QueryMultipleAsync("spGetOffsetPagedOrders", parameters, commandType: CommandType.StoredProcedure))
            {
                var orders = multi.Read<GetOrderDto>().ToList();
                var totalItems = multi.ReadSingle<int>();

                var paginatedList = new PaginatedList<GetOrderDto>
                {
                    Items = orders,
                    PageIndex = pageNo,
                    PageSize = pageSize,
                    TotalItems = totalItems
                };

                return paginatedList;
            }
        }
    }

    public async Task<int> CreateAsync(Order order)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();

            var lineItemsDataTable = new DataTable();
            lineItemsDataTable.Columns.Add("Name", typeof(string));
            lineItemsDataTable.Columns.Add("Quantity", typeof(int));
            lineItemsDataTable.Columns.Add("Rate", typeof(decimal));

            foreach (var item in order.Items)
            {
                lineItemsDataTable.Rows.Add(item.Name, item.Quantity, item.Rate);
            }

            var parameters = new DynamicParameters();
            parameters.Add("ReferenceId", order.ReferenceId);
            parameters.Add("PurchaseOrderNo", order.PurchaseOrderNo);
            parameters.Add("PlacedAtUtc", DateTime.UtcNow);
            parameters.Add("SupplierId", order.SupplierId);
            parameters.Add("ExpectedDate", order.ExpectedDate);
            parameters.Add("Remark", order.Remark);
            parameters.Add("LineItems", lineItemsDataTable.AsTableValuedParameter("dbo.AddLineItemType"));

            var rowsAffected = await connection.ExecuteAsync("spAddOrder", parameters, commandType: CommandType.StoredProcedure);
            return rowsAffected;
        }
    }

    public async Task<int> UpdateAsync(Order order)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();

            var lineItemsDataTable = new DataTable();
            lineItemsDataTable.Columns.Add("OrderId", typeof(int));
            lineItemsDataTable.Columns.Add("Name", typeof(string));
            lineItemsDataTable.Columns.Add("Quantity", typeof(int));
            lineItemsDataTable.Columns.Add("Rate", typeof(decimal));

            foreach (var item in order.Items)
            {
                lineItemsDataTable.Rows.Add(order.Id, item.Name, item.Quantity, item.Rate);
            }

            var parameters = new DynamicParameters();
            parameters.Add("OrderId", order.Id);
            parameters.Add("ReferenceId", order.ReferenceId);
            parameters.Add("PurchaseOrderNo", order.PurchaseOrderNo);
            parameters.Add("PlacedAtUtc", order.PlacedAtUtc);
            parameters.Add("SupplierId", order.SupplierId);
            parameters.Add("ExpectedDate", order.ExpectedDate);
            parameters.Add("Remark", order.Remark);
            parameters.Add("LineItems", lineItemsDataTable.AsTableValuedParameter("dbo.UpdateLineItemType"));

            var rowsAffected = await connection.ExecuteAsync("spUpdateOrder", parameters, commandType: CommandType.StoredProcedure);
            return rowsAffected;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();

            var deleteQuery = "EXEC spDeleteOrder @OrderId";
            var result = await connection.ExecuteAsync(deleteQuery, new { OrderId = id });
            return result;
        }
    }
}
