using System.ComponentModel.DataAnnotations;

namespace ProvaPub.Domain.Models
{
	public class Customer
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}
