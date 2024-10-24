using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // this gives a termporary null value.
        public double Price { get; set; }
        public int? SerialNumberId { get; set; }
        [ForeignKey( "SerialNumberId")]
        public SerialNumber? SerialNumber { get; set; }
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public List<ItemClient>? ItemClients { get; set; }
    }
}
