using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzVault.Models
{
    public class LoginData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime? Expiry { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? Folder { get; set; }
    }
}
