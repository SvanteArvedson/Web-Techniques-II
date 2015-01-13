using System.ComponentModel.DataAnnotations;

namespace Weather.Domain.Entities
{
    /// <summary>
    /// Weather forecast from Yr.no
    /// </summary>
    public class Forecast
    {
        [Key]
        public int ForecastId { get; set; }

        [Required]
        [Range(0, 3)]
        public int Period { get; set; }

        [Required]
        public System.DateTime StartTime { get; set; }

        [Required]
        public System.DateTime EndTime { get; set; }

        [Required]
        [Range(1, 129)]
        public byte SymbolNbr { get; set; }

        [Required]
        [StringLength(30)]
        public string SymbolTxt { get; set; }

        [Required]
        public double Temperatur { get; set; }

        [Required]
        public int PlaceId { get; set; }

        public virtual Place Place { get; set; }
    }
}
