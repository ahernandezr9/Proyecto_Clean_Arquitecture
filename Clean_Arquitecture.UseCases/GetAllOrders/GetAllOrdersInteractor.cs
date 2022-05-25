using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.UseCasesDTOs.GetAllOrders;
using Clean_Arquitecture.UseCasesPorts.GetAllOrders;
using Clean_Arquitecture.Entities.Exceptions;
using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.Entities.Specifications;
using Clean_Arquitecture.UseCases.Common.Validators;

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean_Arquitecture.Repositories.EFCore.Repositories;

namespace Clean_Arquitecture.UseCases.GetAllOrders
{
    public class GetAllOrdersInteractor : IGetAllOrdersInputPort
    {
        readonly IOrderDetailRepository OrderDetailRepository;
        readonly IProductRepository ProductRepository;
        readonly IGetAllOrdersOutputPort OutputPort;
        readonly IEnumerable<IValidator<GetAllOrdersParams>> Validators;

        public GetAllOrdersInteractor(IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            IGetAllOrdersOutputPort outputPort,
            IEnumerable<IValidator<GetAllOrdersParams>> validators)
        {
            OrderDetailRepository = orderDetailRepository;
            ProductRepository = productRepository;
            OutputPort = outputPort;
            Validators = validators;
        }

        public async Task Handle(GetAllOrdersParams getAllOrders)
        {
            await Validator<GetAllOrdersParams>.Validate(getAllOrders, Validators);

            var output = new GetAllOrdersOutputPort();
            output.Orders = new List<GetAllOrder>();

            try
            {
                var expressionOrderDetail = new Specification<OrderDetail>(s => s.Order.CustomerId.ToLower() == getAllOrders.CustomerId.ToLower());
                var ordersDetail = OrderDetailRepository.GetOrdersDetailBySpecification(expressionOrderDetail).ToList();

                var productsId = ordersDetail.Select(s => s.ProductId).Distinct().ToList();
                var expressionProduct = new Specification<Product>(s => productsId.Contains(s.Id));
                var products = ProductRepository.GetProductsBySpecification(expressionProduct).ToList();

                var ordersId = ordersDetail.Select(s => s.Order.Id).Distinct().ToList();

                foreach (var id in ordersId)
                {
                    var order = ordersDetail
                        .Where(s => s.Order.Id == id)
                        .Select(s => new GetAllOrder(
                            s.Order.OrderDate,
                            s.Order.ShipAddress,
                            s.Order.ShipCity,
                            s.Order.ShipCountry,
                            s.Order.ShipPostalCode,
                            s.Order.DiscountType,
                            s.Order.Discount,
                            s.Order.ShippingType
                            ))
                        .FirstOrDefault();

                    var detail = ordersDetail
                        .Where(f => f.Order.Id == id)
                        .Select(s => new GetAllOrderDetail(
                            products.FirstOrDefault(d => d.Id == s.ProductId).Name,
                            s.UnitPrice,
                            s.Quantity
                            ))
                        .ToList();

                    order.SetOrderDetails(detail);

                    output.Orders.Add(order);
                }
            }
            catch (Exception ex)
            {
                throw new GeneralException("Error getting order", ex.Message);
            }

            await OutputPort.Handle(output);
        }
    }
}
