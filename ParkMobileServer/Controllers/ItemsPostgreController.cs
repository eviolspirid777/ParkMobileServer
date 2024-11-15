using Microsoft.AspNetCore.Mvc;
using ParkMobileServer.DbContext;
using ParkMobileServer.Entities;
using System.Buffers;
using System.IO;

namespace ParkMobileServer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ItemsPostgreController : Controller
	{
		PostgreSQLDbContext _postgreSQLDbContext;
		public ItemsPostgreController
		(
			PostgreSQLDbContext postgreSQLDbContext
		)
		{
			_postgreSQLDbContext = postgreSQLDbContext;
		}

		[HttpGet]
		public IEnumerable<ItemEntity> GetItemsList(int skip, int take)
		{
			return _postgreSQLDbContext.ItemEntities
									.Skip(skip)
									.Take(take)
									.ToArray();
		}

		[HttpGet("{id}")]
		public ActionResult<ItemEntity> GetItem(int id)
		{
			var item = _postgreSQLDbContext.ItemEntities.FirstOrDefault(item => item.Id == id);

			if (item == null)
			{
				return NotFound();
			}

			return item;
		}
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImageData(int id)
        {
            var item = await _postgreSQLDbContext.ItemEntities.FindAsync(id);
            if (item == null || item.Image == null)
            {
                return NotFound();
            }

            // Убедитесь, что тип изображения корректен
            var imageContentType = "image/jpeg"; // или другой тип, который у вас
            return File(item.Image, imageContentType); // Используйте метод File для отправки изображения
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadData([FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Image is required");
            }

            // Получение данных из FormData
            var form = await Request.ReadFormAsync();
            var price = form["price"];
            var tag = form["tag"];

            // Преобразование IFormFile в byte[]
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                // Создание уникального ID
                var item = new ItemEntity
                {
                    Image = imageBytes,
                    Price = price.ToString(),
                    Tag = tag.ToString()
                };

                _postgreSQLDbContext.ItemEntities.Add(item);
                await _postgreSQLDbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
            }
        }
    }
}
