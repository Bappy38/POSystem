using Dapper;
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

    public async Task<int> CreateAsync(OrderDto order)
    {
        using (var connection = _context.CreateConnection())
        {
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
            parameters.Add("PlacedAtUtc", DateTime.UtcNow);
            parameters.Add("SupplierId", order.SupplierId);
            parameters.Add("ExpectedDate", order.ExpectedDate);
            parameters.Add("Remark", order.Remark);
            parameters.Add("LineItems", lineItemsDataTable.AsTableValuedParameter("dbo.AddLineItemType"));

            var rowsAffected = await connection.ExecuteAsync("spAddOrder", parameters, commandType: CommandType.StoredProcedure);
            return rowsAffected;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var deleteQuery = "EXEC spDeleteOrder @OrderId";
            var result = await connection.ExecuteAsync(deleteQuery, new { OrderId = id });
            return result;
        }
    }

    public async Task<OrderDto> GetByIdAsync(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var orderDictionary = new Dictionary<int, OrderDto>();

            var result = await connection.QueryAsync<OrderDto, LineItemDto, OrderDto>(
                "spGetOrderByIdWithLineItems",
                (order, lineItem) =>
                {
                    OrderDto orderEntry;

                    if (!orderDictionary.TryGetValue(order.Id, out orderEntry))
                    {
                        orderEntry = order;
                        orderEntry.Items = new List<LineItemDto>();
                        orderDictionary.Add(orderEntry.Id, orderEntry);
                    }

                    if (lineItem != null)
                    {
                        orderEntry.Items.Add(lineItem);
                    }

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

    public async Task<List<Order>> GetPagedAsync(int cursor, int pageSize)
    {
        using (var connection = _context.CreateConnection())
        {
            var parameters = new DynamicParameters();
            parameters.Add("CursorId", cursor);
            parameters.Add("PageSize", pageSize);
            //parameters.Add("Search", search);

            var orders = await connection.QueryAsync<Order>("spGetPagedOrders", parameters, commandType: CommandType.StoredProcedure);
            return orders?.ToList() ?? new List<Order>();
        }
    }

    public async Task<int> UpdateAsync(OrderDto order)
    {
        using (var connection = _context.CreateConnection())
        {
            var lineItemsDataTable = new DataTable();
            lineItemsDataTable.Columns.Add("Id", typeof(int));
            lineItemsDataTable.Columns.Add("OrderId", typeof(int));
            lineItemsDataTable.Columns.Add("Name", typeof(string));
            lineItemsDataTable.Columns.Add("Quantity", typeof(int));
            lineItemsDataTable.Columns.Add("Rate", typeof(decimal));

            foreach (var item in order.Items)
            {
                lineItemsDataTable.Rows.Add(item.LineItemId, order.Id, item.Name, item.Quantity, item.Rate);
            }

            var parameters = new DynamicParameters();
            parameters.Add("OrderId", order.Id);
            parameters.Add("ReferenceId", order.ReferenceId);
            parameters.Add("PlacedAtUtc", order.PlacedAtUtc);
            parameters.Add("SupplierId", order.SupplierId);
            parameters.Add("ExpectedDate", order.ExpectedDate);
            parameters.Add("Remark", order.Remark);
            parameters.Add("LineItems", lineItemsDataTable.AsTableValuedParameter("dbo.UpdateLineItemType"));

            var rowsAffected = await connection.ExecuteAsync("spUpdateOrder", parameters, commandType: CommandType.StoredProcedure);
            return rowsAffected;
        }
    }
}
