using ShopTARge23.Core.Dto;

namespace ShopTARge23.Models.Kindergarten
{
    public class KindergartenIndexViewModel
    {
        public Guid? Id { get; set; }
        public string? KindergartenName { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? Teacher { get; set; }
    }
}
