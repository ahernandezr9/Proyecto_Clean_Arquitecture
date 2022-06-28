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
            var payOrder = output.Order
                        .Select(s => new Order
                        {
                            OrderDate = s.OrderDate,
                            DiscountType = s.DiscountType,
                            Discount = s.Discount,
                            OrderDetails = s.OrderDetails
                            .Select(od => new OrderDetail
                            {
                                Product = od.Product,
                                UnitPrice = od.UnitPrice,
                                Quantity = od.Quantity
                            }).ToList(),
                            Ticket = s.Ticket,
                            StatusPay = s.StatusPay,
                            AmountPay = s.AmountPay
                        });
            Content = new PayOrderOutput()
            {
                Order = payOrder
            };

            return Task.CompletedTask;
        }
    }
}
