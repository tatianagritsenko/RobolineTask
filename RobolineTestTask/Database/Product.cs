using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RobolineTestTask.Database
{
    public class Product
    {
        [Range(1, int.MaxValue, ErrorMessage = "The value must be a positive number")]
        [DefaultValue(0)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [DefaultValue(null)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The value must be a positive number")]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The value must be a positive number")]
        [DefaultValue(0)]
        public int CategoryId { get; set; }
    }
}
