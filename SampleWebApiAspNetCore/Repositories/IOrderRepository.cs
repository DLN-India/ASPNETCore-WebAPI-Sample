using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebApiAspNetCore.Repositories
{
    public interface IOrderRepository
    {
        OrderItem GetSingle(int Order_Num);
        void Add(OrderItem item);
        void Delete(int Order_Num);
        OrderItem Update(int Order_Num, OrderItem item);
        IQueryable<OrderItem> GetAll(QueryParameters queryParameters);

        ICollection<OrderItem> GetRandomOrders();
        int Count();

        bool Save();
    }
}
