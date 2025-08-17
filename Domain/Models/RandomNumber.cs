using System.ComponentModel.DataAnnotations;

namespace ProvaPub.Domain.Models
{
    public class RandomNumber
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
    }
}
