using System.ComponentModel.DataAnnotations;

namespace Weather.Domain.Entities
{
    /// <summary>
    /// Users favorite places
    /// </summary>
    public class FavouritePlace
    {
        [Key]
        public int FavouritePlaceId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Region { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        public virtual User User { get; set; }
    }
}
