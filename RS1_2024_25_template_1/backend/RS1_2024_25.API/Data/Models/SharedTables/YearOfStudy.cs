using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul1_Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models.SharedTables
{
    public class YearOfStudy
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(AcademicYear))]
        public int AkademskaGodinaId { get; set; }
        public AcademicYear AkademskaGodina { get; set; }
        public int GodinaStudija { get; set; }
        public bool Obnova { get; set; }
        public DateTime DatumUpisa { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public int SnimioId { get; set; }
        public MyAppUser Snimio { get; set; }
        public float CijenaSkolarine { get; set; }
        [ForeignKey(nameof(MyAppUser))]
        public int StudentId { get; set; }
        public MyAppUser Student { get; set; }


    }
}
