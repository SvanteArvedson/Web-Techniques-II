using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weather.Domain.Entities
{
    public class Place
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Region { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        public System.DateTime NextUpdate { get; set; }

        [Required]
        public int SearchId { get; set; }

        public virtual ICollection<Forecast> Forecasts { get; set; }

        public virtual ICollection<User> Users { get; set; }

        [ForeignKey("Id")]
        public virtual Search Search { get; set; }

        public Place()
        {
            this.Forecasts = new HashSet<Forecast>();
        }
    }
}
