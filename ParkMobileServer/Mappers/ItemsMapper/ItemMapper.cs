using ParkMobileServer.DbContext;
using ParkMobileServer.DTO.ItemDTO;
using ParkMobileServer.Entities.Items;

namespace ParkMobileServer.Mappers.ItemsMapper
{
    public class ItemMapper
    {
        public static ItemDTO MapToDto(ItemEntity item, ItemBrand? brand, ItemCategory? category)
        {
            if(brand != null && category != null )
            {
                return new ItemDTO
                {
                    Name = item.Name,
                    BrandName = brand.Name,
                    CategoryName = category.Name,
                    Description = item.Description,
                    DiscountPrice = item.DiscountPrice,
                    Image = item.Image,
                    Price = item.Price,
                    Stock = item.Stock
                };
            }
            if(brand != null)
            {
                return new ItemDTO
                {
                    Name = item.Name,
                    BrandName = brand.Name,
                    CategoryName = "",
                    Description = item.Description,
                    DiscountPrice = item.DiscountPrice,
                    Image = item.Image,
                    Price = item.Price,
                    Stock = item.Stock
                };
            }
            if(category != null)
            {
                return new ItemDTO
                {
                    Name = item.Name,
                    BrandName = "",
                    CategoryName = category.Name,
                    Description = item.Description,
                    DiscountPrice = item.DiscountPrice,
                    Image = item.Image,
                    Price = item.Price,
                    Stock = item.Stock
                };
            }

            return new ItemDTO
            {
                Name = item.Name,
                BrandName = "",
                CategoryName = "",
                Description = item.Description,
                DiscountPrice = item.DiscountPrice,
                Image = item.Image,
                Price = item.Price,
                Stock = item.Stock
            };
        }
    }
}
