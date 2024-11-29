using ParkMobileServer.Entities.Items;

namespace ParkMobileServer.DTO.ItemDTO
{
    public class ItemDTO
    {
        public int? id { get; set; } = 0;
        public string Name { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public int Stock { get; set; }
        public string? Options { get; set; }

        public string CategoryName { get; set; }
        public string BrandName { get; set; }
    }
}
