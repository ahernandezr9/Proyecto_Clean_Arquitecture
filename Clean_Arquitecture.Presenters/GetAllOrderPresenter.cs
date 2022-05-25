using Clean_Arquitecture.Presenters.GetAllOrdersDTO;
using Clean_Arquitecture.UseCasesDTOs.GetAllOrders;
using Clean_Arquitecture.UseCasesPorts.GetAllOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Presenters
{
    public class GetAllOrdersPresenter : IGetAllOrdersOutputPort, IPresenter<GetAllOrdersOutput>
    {
        public GetAllOrdersOutput Content { get; private set; }

        public Task Handle(GetAllOrdersOutputPort output)
        {
            var orders = output.Orders
                        .Select(s => new Order
                        { 
                            OrderDate = s.OrderDate,
                            ShipAddress = s.ShipAddress,
                            ShipCity = s.ShipCity,
                            ShipCountry = s.ShipCountry,
                            ShipPostalCode = s.ShipPostalCode,
                            DiscountType = s.DiscountType,
                            Discount = s.Discount,
                            shippingType = s.shippingType,
                            OrderDetails = s.OrderDetails
                            .Select(od => new OrderDetail{
                                Product = od.Product,
                                UnitPrice =od.UnitPrice,
                                Quantity=od.Quantity
                            }).ToList()
                        })
                        .ToList();
            Content = new GetAllOrdersOutput()
            {
                Orders = orders
            };

            return Task.CompletedTask;
        }
    }
}
