using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.Entities.Specifications;
using Clean_Arquitecture.Repositories.EFCore.DataContext;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Arquitecture.Repositories.EFCore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly Clean_ArquitectureContext Context;
        public ProductRepository(Clean_ArquitectureContext context) =>
            Context = context;

        public IEnumerable<Product> GetProductsBySpecification(Specification<Product> specification)
        {
            var ExpressionDelegate = specification.Expression.Compile();
            return Context.Products.Where(ExpressionDelegate);
        }
    }
}
