using Clean_Arquitecture.Entities.Enums;
using System;

namespace Clean_Arquitecture.Entities.POCOEntities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public double AmountPay { get; set; }
        public string Ticket { get; set; }
        public StatusType StatusPay { get; set; }
        public DateTime DateGenerate { get; set; }
        public Order Order { get; set; }
    }
}
