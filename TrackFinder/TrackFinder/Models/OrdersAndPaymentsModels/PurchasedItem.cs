using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrackFinder.Models.TeachingModels;
using TrackFinder.Models.UserModels;

namespace TrackFinder.Models.OrdersAndPayments
{
    public class PurchasedItem
    {
        [Key]
        public Guid PurchasingId { get; set; }
        public string type { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }

        public Guid PurchasedItemId { get; set; }
        public Guid StudentId { get; set; }
        public Guid InstructorId { get; set; }
        public virtual Course? Course { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public virtual Student Student { get; set; } = null!;
        public virtual Instructor Instructor { get; set; } = null!;
    }
}
