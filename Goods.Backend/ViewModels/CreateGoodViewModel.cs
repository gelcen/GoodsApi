using System.ComponentModel.DataAnnotations;

namespace Goods.Backend.ViewModels
{
	public class CreateGoodViewModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public decimal Price { get; set; }
	}
}
