using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkMobileServer.Entities.Orders
{
	public enum OrderStatus
	{
		Awaiting_payment = 0,
		In_processing,
		Sent,
		Delivered,
	}
	public class OrderEntity
	{
		[Key]
		public int Id { get; set; }
		public int UserId { get; set; }
		public OrderStatus Status { get; set; }
	}
}
