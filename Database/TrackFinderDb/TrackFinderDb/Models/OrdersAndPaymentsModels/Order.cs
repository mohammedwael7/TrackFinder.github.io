using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.OrdersAndPayments
{
    public class Order
    {
        public int Id { get; set; }

        public Request Request { get; set; }
        public Payment Payment { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
