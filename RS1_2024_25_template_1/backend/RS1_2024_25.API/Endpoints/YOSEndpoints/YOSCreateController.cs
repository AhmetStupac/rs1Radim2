using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.SharedTables;

namespace RS1_2024_25.API.Endpoints.YOSEndpoints
{
    [Route("yos")]
    [ApiController]
    public class YOSCreateController(ApplicationDbContext db) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<int>> HandleAsync(YOSCreateRequest req, CancellationToken cancellationToken)
        {
            YearOfStudy yos = new YearOfStudy
            {
                DatumUpisa = req.DatumUpisa,
                GodinaStudija = req.GodinaStudija,
                AkademskaGodinaId = req.AkademskaGodinaId,
                StudentId = req.StudentId,
                SnimioId = req.SnimioId,
            };

            bool isRenewing = await db.YearsOfStudy.Where(yos => yos.GodinaStudija == req.GodinaStudija
            && yos.StudentId == req.StudentId).FirstOrDefaultAsync(cancellationToken) != null;

            yos.CijenaSkolarine = isRenewing ? 400f : 1800f;
            yos.Obnova = isRenewing;

            db.YearsOfStudy.AddAsync(yos, cancellationToken);
            db.SaveChangesAsync(cancellationToken);

            return Ok(yos.Id);
        }


       public class YOSCreateRequest
        {
            public DateTime DatumUpisa { get; set; }
            public int GodinaStudija { get; set; }
            public int AkademskaGodinaId { get; set; }
            public int SnimioId { get; set; }
            public int StudentId { get; set; }
        }
    }
}
