using ShopTARge23.Models.Kindergarten;

namespace ShopTARge23.Models.Kindergarten
{
    public class KindergartenDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string? KindergartenName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
