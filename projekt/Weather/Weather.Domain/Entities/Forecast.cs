using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Entities
{
    public class Forecast
    {
        [Key]
        public int Id { get; set; }

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

        [ForeignKey("Id")]
        public virtual Place Place { get; set; }
    }
}
