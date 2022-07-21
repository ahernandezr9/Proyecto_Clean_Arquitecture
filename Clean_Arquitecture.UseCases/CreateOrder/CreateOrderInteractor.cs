using Clean_Arquitecture.Entities.Exceptions;
using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.UseCases.Common.Validators;
using Clean_Arquitecture.UseCasesDTOs.CreateOrder;
using FluentValidation;
using Clean_Arquitecture.UseCasesPorts.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Clean_Arquitecture.Entities.Enums;

namespace Clean_Arquitecture.UseCases.CreateOrder
{
    public class CreateOrderInteractor : ICreateOrderInputPort
    {
        readonly IOrderRepository OrderRepository;
        readonly IOrderDetailRepository OrderDetailRepository;
        readonly IPaymentRepository PaymentRepository;
        readonly IUnitOfWork UnitOfWork;
        readonly ICreateOrderOutputPort OutputPort;
        readonly IEnumerable<IValidator<CreateOrderParams>> Validators;
        public CreateOrderInteractor(IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork,
            ICreateOrderOutputPort outputPort,
            IEnumerable<IValidator<CreateOrderParams>> validators) =>
            (OrderRepository, OrderDetailRepository, PaymentRepository, UnitOfWork, OutputPort, Validators) = 
            (orderRepository, orderDetailRepository, paymentRepository, unitOfWork, outputPort, validators);

        public async Task Handle(CreateOrderParams order)
        {
            await Validator<CreateOrderParams>.Validate(order, Validators);

            Order Order = new Order
            {
                CustomerId = order.CustomerId,
                OrderDate = DateTime.Now,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipCountry = order.ShipCountry,
                ShipPostalCode = order.ShipPostalCode,
                ShippingType = ShippingType.Road,
                DiscountType = DiscountType.Percentage,
                Discount = 10
            };
            OrderRepository.Create(Order);
            foreach (var Item in order.OrderDetails)
            {
                OrderDetailRepository.Create(
                    new OrderDetail
                    {
                        Order = Order,
                        ProductId = Item.ProductId,
                        UnitPrice = Item.UnitPrice,
                        Quantity = Item.Quantity
                    });

                PaymentRepository.Create(
                    new Payment
                    {
                        Order = Order,
                        AmountPay = ((double)(Item.UnitPrice * Item.Quantity)) - ((double)(Item.UnitPrice * Item.Quantity) * (Order.Discount / 100)),
                        Ticket = "T00043",
                        //Ticket = ""+ Order,
                        //Ticket = "T" + Item.Quantity.ToString().PadLeft(5, '0'),
                        //Ticket = "T" + Order.Id.ToString().PadLeft(5, '0'),
                        StatusPay = PaymentStatus.Pending,
                        DateGenerate = DateTime.Now
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
            await OutputPort.Handle(Order.Id);
        }
    }
}
