using Clean_Arquitecture.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCasesDTOs.PayOrder
{
    public class PayOrderOutputPort
    {
        public GetDataOrder Order { get; set; }
    }
    public class GetDataOrder
    {
        public DateTime OrderDate { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public double Discount { get; private set; }
        public List<GetOrderDetail> OrderDetails { get; private set; }
        public string Ticket { get; private set; }

        public StatusType StatusPay { get; private set; }
        public double AmountPay { get; private set; }

        public GetDataOrder(DateTime orderDate,
            DiscountType discountType, double discount, string ticket, StatusType statusPay, double amountPay)
        {
            OrderDate = orderDate;
            DiscountType = discountType;
            Discount = discount;
            Ticket = ticket;
            StatusPay = statusPay;
            AmountPay = amountPay;
        }

        public void SetOrderDetails(List<GetOrderDetail> orderDetails)
            => OrderDetails = orderDetails;
    }
    public class GetOrderDetail
    {
        public string Product { get; private set; }
        public decimal UnitPrice { get; private set; }
        public short Quantity { get; private set; }

        public GetOrderDetail(string product, decimal unitPrice, short quantity)
        {
            Product = product;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
