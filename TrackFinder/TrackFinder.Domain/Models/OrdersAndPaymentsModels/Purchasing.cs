using System.ComponentModel.DataAnnotations;
using TrackFinder.Domain.Models.CourseModels;
using TrackFinder.Domain.Models.UserModels;

namespace TrackFinder.Domain.Models.OrdersAndPaymentsModels
{
    public class Purchasing
    {
        [Key]
        public Guid PurchasingId { get; set; } = Guid.NewGuid();
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
