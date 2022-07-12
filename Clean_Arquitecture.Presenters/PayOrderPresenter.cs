using Clean_Arquitecture.Presenters.PayOrderDTO;
using Clean_Arquitecture.UseCasesDTOs.PayOrder;
using Clean_Arquitecture.UseCasesPorts.PayOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Presenters
{
    public class PayOrderPresenter : IPayOrderOutputPort, IPresenter<PayOrderOutput>
    {
        public PayOrderOutput Content { get; private set; }

        public Task Handle(PayOrderOutputPort output)
        {
            var payOrder = new Order { 
                            OrderDate = output.Order.OrderDate,
                            DiscountType = output.Order.DiscountType,
                            Discount = output.Order.Discount,
                            OrderDetails = output.Order.OrderDetails
                            .Select(od => new OrderDetail
                            {
                                Product = od.Product,
                                UnitPrice = od.UnitPrice,
                                Quantity = od.Quantity
                            }).ToList(),
                            Ticket = output.Order.Ticket,
                            StatusPay = output.Order.StatusPay,
                            AmountPay = output.Order.AmountPay
            };
            Content = new PayOrderOutput()
            {
                Order = payOrder
            };

            return Task.CompletedTask;
        }
    }
}
