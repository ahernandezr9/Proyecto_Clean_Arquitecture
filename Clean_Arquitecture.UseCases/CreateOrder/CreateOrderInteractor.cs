﻿using Clean_Arquitecture.Entities.Exceptions;
using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Entities.POCOEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCases.CreateOrder
{
    public class CreateOrderInteractor : AsyncRequestHandler<CreateOrderInputPort>
    {
        readonly IOrderRepository OrderRepository;
        readonly IOrderDetailRepository OrderDetailRepository;
        readonly IUnitOfWork UnitOfWork;
        public CreateOrderInteractor(IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IUnitOfWork unitOfWork) =>
            (OrderRepository, OrderDetailRepository, UnitOfWork) = 
            (orderRepository, orderDetailRepository, unitOfWork);
        protected async override Task Handle(CreateOrderInputPort request, CancellationToken cancellationToken)
        {
            Order Order = new Order
            {
                CustomerId = request.RequestData.CustomerId,
                OrderDate = DateTime.Now,
                ShipAddress = request.RequestData.ShipAddress,
                ShipCity = request.RequestData.ShipCity,
                ShipCountry = request.RequestData.ShipCountry,
                ShipPostalCode = request.RequestData.ShipPostalCode,
                ShippingType = Entities.Enums.ShippingType.Road,
                DiscountType = Entities.Enums.DiscountType.Percentage,
                Discount = 10
            };
            OrderRepository.Create(Order);
            foreach (var Item in request.RequestData.OrderDetails)
            {
                OrderDetailRepository.Create(
                    new OrderDetail
                    {
                        Order = Order,
                        ProductId = Item.ProductId,
                        UnitPrice = Item.UnitPrice,
                        Quantity = Item.Quantity
                    });
            }
            try
            {
                await UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new GeneralException("Error al crear la orden.",
                    ex.Message);
            }
            request.OutputPort.Handle(Order.Id);
        }
    }
}
