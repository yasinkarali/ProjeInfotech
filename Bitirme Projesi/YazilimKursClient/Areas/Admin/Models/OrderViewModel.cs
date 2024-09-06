namespace YazilimKursClient.Areas.Admin.Models
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public string UserId { get; set; }
		public List<OrderItemViewModel> OrderItems { get; set; }
	}
	public class OrderItemViewModel
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int CourseId { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
