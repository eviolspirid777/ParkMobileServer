﻿using ParkMobileServer.DbContext;
using ParkMobileServer.DTO.ItemDTO;
using ParkMobileServer.Entities.Items;

namespace ParkMobileServer.Mappers.ItemsMapper
{
    public class ItemMapper
    {
        public static ItemDTO MapToDto(ItemEntity item, int brandId, int categoryId)
        {
            if(brandId != null && categoryId != null )
            {
                return new ItemDTO
                {
                    id = item.Id,
                    Name = item.Name,
                    BrandId = brandId,
                    CategoryId = categoryId,
                    Description = item.Description,
                    DiscountPrice = item.DiscountPrice,
                    Image = item.Image,
                    Price = item.Price,
                    Stock = item.Stock,
                    Options = item.Options,
                    Article = item.Article
                };
            }
            if(brandId != null)
            {
                return new ItemDTO
                {
                    id = item.Id,
                    Name = item.Name,
                    BrandId = brandId,
                    CategoryId = -1,
                    Description = item.Description,
                    DiscountPrice = item.DiscountPrice,
                    Image = item.Image,
                    Price = item.Price,
                    Stock = item.Stock,
                    Options = item.Options,
					Article = item.Article
				};
            }
            if(categoryId != null)
            {
                return new ItemDTO
                {
                    id = item.Id,
                    Name = item.Name,
                    BrandId = -1,
                    CategoryId = categoryId,
                    Description = item.Description,
                    DiscountPrice = item.DiscountPrice,
                    Image = item.Image,
                    Price = item.Price,
                    Stock = item.Stock,
                    Options = item.Options,
					Article = item.Article
				};
            }

            return new ItemDTO
            {
                id = item.Id,
                Name = item.Name,
                BrandId = -1,
                CategoryId = -1,
                Description = item.Description,
                DiscountPrice = item.DiscountPrice,
                Image = item.Image,
                Price = item.Price,
                Stock = item.Stock,
                Options = item.Options,
				Article = item.Article
			};
        }
    }
}
