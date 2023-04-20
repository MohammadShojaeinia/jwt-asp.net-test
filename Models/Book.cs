using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}