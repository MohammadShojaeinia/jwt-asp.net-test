using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Permission
    {
        [Key]
        public string Name { get; set; }

        public Permission(string name)
        {
            this.Name = name;
        }
    }
}