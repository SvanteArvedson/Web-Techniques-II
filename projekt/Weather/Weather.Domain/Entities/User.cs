using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Weather.Domain.Entities
{
    /// <summary>
    /// Application User
    /// </summary>
    public class User : IdentityUser
    {
        public virtual ICollection<FavouritePlace> FavouritePlace { get; set; }

        public User()
        {
            this.FavouritePlace = new HashSet<FavouritePlace>();
        }
    }
}
