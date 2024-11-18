namespace ShopTARge23.Core.Domain
{
    public class Kindergarten
    {
        public Guid? Id { get; set; }
        public string? KindergartenName { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? Teacher {  get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? KindergartenId { get; set; } // nullable foreign key
       
    }
}
