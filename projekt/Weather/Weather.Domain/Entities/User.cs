using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Weather.Domain.Entities
{
    public class User : IdentityUser
    {
        public virtual ICollection<Place> Places { get; set; }
    }
}
