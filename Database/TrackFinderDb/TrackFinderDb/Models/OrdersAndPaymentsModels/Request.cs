using System;
using System.Collections.Generic;
using System.Text;
using TrackFinderDb.Models.UserModels;

namespace TrackFinderDb.Models.OrdersAndPayments
{
    public class Request
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        public Student Student { get; set; }
        public Order Order { get; set; }
    }
}
