using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AkordideApi.Models
{
    public class Lugu
    {
        [Key]
        public int Id { get; set; }

        // Loole nime võime lisada
        public string? Nimetus { get; set; }

        // Iga takt sisaldab kolmkõla Id (või embed)
        public List<Takt> Taktid { get; set; } = new List<Takt>();

        // Valik: tagastab kõik noodid kas arvulise kujul või tähe kujul
        public IEnumerable<int[]> GetNoodidArvuliselt()
        {
            return Taktid.Select(t => t.Kolmkola.GetNoodid());
        }

        public IEnumerable<string[]> GetNoodidNimedena()
        {
            return Taktid.Select(t => t.Kolmkola.GetNimed());
        }
    }

    public class Takt
    {
        public int Id { get; set; }

        public int KolmkolaId { get; set; }   // FK
        public Kolmkola Kolmkola { get; set; } = null!;
    }

}
