using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BzVault.Models
{
    public class LoginListMeta
    {
        public IEnumerable<LoginItem> Records { get; set; }
        public IEnumerable<Link> Links { get; set; }
        public string ErrorMessage { get; set; }
    }
}
