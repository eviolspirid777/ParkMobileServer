using System.ComponentModel.DataAnnotations;

namespace ParkMobileServer.Entities
{
	public class ItemEntity
	{
		[Key]
		public int Id { get; set; }
		public string Tag { get; set; }
		public string Price { get; set; }
		public byte[]? Image { get; set; }
	}
}
