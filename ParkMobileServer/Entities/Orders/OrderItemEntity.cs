using ParkMobileServer.Entities.Items;

namespace ParkMobileServer.Entities.Orders
{
	public class OrderItemEntity
	{
        public int Id { get; set; }
        public int Quantity { get; set; } // Количество данного товара в заказе


        public int ProductId { get; set; }
        public ItemEntity? Product { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
