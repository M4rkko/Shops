//using Microsoft.AspNetCore.Http;

namespace ShopTARge23.Core.Dto
{
    public class KindergartenDto
    {
        public Guid? Id { get; set; }
        public string? KindergartenName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
