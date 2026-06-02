using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.OrdersAndPayments
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
