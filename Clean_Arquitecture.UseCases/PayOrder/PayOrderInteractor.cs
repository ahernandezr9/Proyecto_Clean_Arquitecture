using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.Entities.Specifications;
using Clean_Arquitecture.UseCases.Common.Validators;
using Clean_Arquitecture.UseCasesDTOs.PayOrder;
using Clean_Arquitecture.UseCasesPorts.PayOrder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCases.PayOrder
{
    public class PayOrderInteractor : IPayOrderInputPort
    {
        readonly IOrderDetailRepository OrderDetailRepository;
        readonly IProductRepository ProductRepository;
        readonly IPaymentRepository PaymentRepository;
        readonly IPayOrderOutputPort OutputPort;
        readonly IEnumerable<IValidator<PayOrderParams>> Validators;

        public PayOrderInteractor(IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            IPaymentRepository paymentRepository,
            IPayOrderOutputPort outputPort,
            IEnumerable<IValidator<PayOrderParams>> validators)
        {
            OrderDetailRepository = orderDetailRepository;
            ProductRepository = productRepository;
            PaymentRepository = paymentRepository;
            OutputPort = outputPort;
            Validators = validators;
        }

        public async Task Handle(PayOrderParams payOrders)
        {
            await Validator<PayOrderParams>.Validate(payOrders, Validators);

            var output = new PayOrderOutputPort();
            //output.Order = new GetDataOrder;

            try
            {
                var expressionOrderDetail = new Specification<OrderDetail>(s => s.Order.Id == payOrders.OrderId);
                var ordersDetail = OrderDetailRepository.GetOrdersDetailBySpecification(expressionOrderDetail).ToList();

                var productsId = ordersDetail.Select(s => s.ProductId).Distinct().ToList();
                var expressionProduct = new Specification<Product>(s => productsId.Contains(s.Id));
                var products = ProductRepository.GetProductsBySpecification(expressionProduct).ToList();

                var paymentId = ordersDetail.Select(p => p.OrderId).Distinct().ToList();
                var expressionPayment = new Specification<Payment>(p => paymentId.Contains(p.Id));
                var payments = PaymentRepository.GetOrdersBySpecification(expressionPayment).ToList();

                var ordersId = ordersDetail.Select(s => s.Order.Id).FirstOrDefault();

                
                foreach (var id in ordersId)
                {
                    var order = ordersDetail.Join(payments, o => o.OrderId, pay => pay.OrderId, (o,pay) => new { o, pay })
                        .Where(s => s.o.OrderId == id)
                        .Select(s => new GetDataOrder(
                            s.o.Order.OrderDate,
                            s.o.Order.DiscountType,
                            s.o.Order.Discount,
                            s.pay.Ticket,
                            s.pay.AmountPay
                            ))
                        .FirstOrDefault();

                    var detail = ordersDetail
                        .Where(f => f.Order.Id == id)
                        .Select(s => new GetOrderDetail(
                            products.FirstOrDefault(d => d.Id == s.ProductId).Name,
                            s.UnitPrice,
                            s.Quantity
                            ))
                        .ToList();

                    order.SetOrderDetails(detail);

                    output.Order=(order);
                }
            }
            catch (Exception)
            {

                throw;
            }

            await OutputPort.Handle(output);
        }
    }
}
