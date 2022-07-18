using Clean_Arquitecture.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Presenters.PayOrderDTO
{
    public class PayOrderOutput
    {
        public Order_Pay Order { get; set; }
    }
    public class Order_Pay
    {
        public DateTime OrderDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public double Discount { get; set; }
        public List<OrderDetail_Pay> OrderDetails { get; set; }

        public string Ticket { get; set; }
        public StatusType StatusPay { get; set; }
        public double AmountPay { get; set; }
    }

    public class OrderDetail_Pay
    {
        public string Product { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
    }

}
