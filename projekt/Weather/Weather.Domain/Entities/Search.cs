using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Weather.Domain.Entities
{
    public class Search
    {
        [Key]
        public int SearchId { get; set; }

        [Required]
        [StringLength(50)]
        public string Word { get; set; }

        [Required]
        public DateTime NextUpdate { get; set; }

        public virtual ICollection<Place> Places { get; set; }

        public Search()
        {
            this.Places = new HashSet<Place>();
        }
    }
}
