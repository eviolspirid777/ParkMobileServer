using System.ComponentModel.DataAnnotations;

namespace ParkMobileServer.Entities.Items
{
    public enum ItemCategory
    {
        Iphone = 0,
        Ipad,
        Watch,
        Mac,
        Airpods,
        Accessories,
        Gadgets,
        Audio,
        Phones,
        Gaming,
        Health,
        Tv,
    }

    public enum ItemBrand
    {
        Apple = 0,
        Samsung,
        Xiaomi,
        Sony,
        Steam,
        Dyson,
    }

    public class ItemEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public ItemCategory Category { get; set; }
        public ItemBrand ItemBrand { get; set; }
        public int Stock { get; set; }
    }
}
