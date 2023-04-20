using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Index(IsUnique = true)]
        [MinLength(6), MaxLength(64)]
        public string Username { get; set; }
        public string Password { get; set; }
        public int? BookId { get; set; }
        public Book Book { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }

        public bool ShouldSerializePassword()
        {
            return false;
        }
    }
}