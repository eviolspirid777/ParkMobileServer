using Microsoft.AspNetCore.Mvc;
using ParkMobileServer.DbContext;
using ParkMobileServer.Entities;
using System.Collections.Immutable;

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
	}
}
