using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFinderDb.Models.OrdersAndPayments
{
    public class Payment
    {
        public int Id { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
