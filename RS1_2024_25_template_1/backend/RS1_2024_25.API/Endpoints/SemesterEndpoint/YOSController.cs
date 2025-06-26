using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.SharedTables;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul1_Auth;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RS1_2024_25.API.Endpoints.SemesterEndpoint
{
    [Route("semesters")]
    [ApiController]
    public class YOSController(ApplicationDbContext db) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<List<YOSResponse>>> GetById(int id, CancellationToken cancellationToken)
        {
            await db.AcademicYears.LoadAsync(cancellationToken);
            await db.MyAppUsers.LoadAsync(cancellationToken);

            var result = db.YearsOfStudy.Where(s => s.Id == id)
                .Select(yos => new YOSResponse
                {
                    Id = yos.Id,
                    AkademskaGodinaId = yos.AkademskaGodinaId,
                    AkademskaGodina = yos.AkademskaGodina,
                    GodinaStudija = yos.GodinaStudija,
                    Obnova = yos.Obnova,
                    DatumUpisa = yos.DatumUpisa,
                    SnimioId = yos.SnimioId,
                    Snimio = yos.Snimio,
                    CijenaSkolarine = yos.CijenaSkolarine,
                    StudentId = yos.StudentId,
                    Student = yos.Student
                }).ToListAsync(cancellationToken);

            return Ok(result);
        }

        public class YOSResponse
        {
            public int Id { get; set; }
            public int AkademskaGodinaId { get; set; }
            public AcademicYear AkademskaGodina { get; set; }
            public int GodinaStudija { get; set; }
            public bool Obnova { get; set; }
            public DateTime DatumUpisa { get; set; }
            public int SnimioId { get; set; }
            public MyAppUser Snimio { get; set; }
            public float CijenaSkolarine { get; set; }
            public int StudentId { get; set; }
            public MyAppUser Student { get; set; }
        }
    }
}
