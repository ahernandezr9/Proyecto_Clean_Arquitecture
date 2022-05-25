using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.Entities.Specifications;
using Clean_Arquitecture.Repositories.EFCore.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Arquitecture.Repositories.EFCore.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        readonly Clean_ArquitectureContext Context;
        public OrderDetailRepository(Clean_ArquitectureContext context) => Context = context;

        public void Create(OrderDetail orderDetail)
        {
            Context.Add(orderDetail);
        }

        public IEnumerable<OrderDetail> GetOrdersDetailBySpecification(Specification<OrderDetail> specification)
        {
            var ExpressionDelegate = specification.Expression.Compile();
            return Context.OrderDetails.Include(i => i.Order).Where(ExpressionDelegate);
        }
    }
}
