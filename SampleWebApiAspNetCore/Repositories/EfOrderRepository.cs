using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Helpers;
using SampleWebApiAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SampleWebApiAspNetCore.Repositories
{
    public class EfOrderRepository : IOrderRepository
    {
        private readonly OrderDBContext _orderDbContext;

        public EfOrderRepository(OrderDBContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public OrderItem GetSingle(int order_num)
        {
            return _orderDbContext.OrderItems.FirstOrDefault(x => x.Order_Num == order_num);
        }

        public void Add(OrderItem item)
        {
            _orderDbContext.OrderItems.Add(item);
        }

        public void Delete(int Order_Num)
        {
            OrderItem orderitem = GetSingle(Order_Num);
            _orderDbContext.OrderItems.Remove(orderitem);
        }

        public OrderItem Update(int order_num, OrderItem item)
        {
            _orderDbContext.OrderItems.Update(item);
            return item;
        }

        public IQueryable<OrderItem> GetAll(QueryParameters queryParameters)
        {
            IQueryable<OrderItem> _allItems = _orderDbContext.OrderItems.OrderBy(queryParameters.OrderBy,
              queryParameters.IsDescending());

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Order_Num.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.OrderDescription.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public int Count()
        {
            return _orderDbContext.OrderItems.Count();
        }

        public bool Save()
        {
            return (_orderDbContext.SaveChanges() >= 0);
        }

        public ICollection<OrderItem> GetRandomOrders()
        {
            List<OrderItem> toReturn = new List<OrderItem>
            {
                GetRandomItem("EMEAORDER1"),
                GetRandomItem("EMEAORDER2"),
                GetRandomItem("EMEAORDER3")
            };

            return toReturn;
        }

        private OrderItem GetRandomItem(string _orderSegment)
        {
            return _orderDbContext.OrderItems
                .Where(x => x.Segment == _orderSegment)
                .OrderBy(o => Guid.NewGuid())
                .FirstOrDefault();
        }
    }
}
