using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Permission
    {
        [Key]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> users { get; set; }

        public Permission() { }

        public Permission(string name)
        {
            this.Name = name;
        }
    }
}