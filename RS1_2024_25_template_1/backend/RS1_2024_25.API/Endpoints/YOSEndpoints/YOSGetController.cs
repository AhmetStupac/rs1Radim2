using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;

namespace RS1_2024_25.API.Endpoints.YOSEndpoints
{
    [Route("yos")]
    [ApiController]
    public class YOSGetController(ApplicationDbContext db) : ControllerBase
    {

        [HttpGet("get/{id}")]
        public async Task<ActionResult<List<YOSGetResponse>>> getById(int id, CancellationToken cancellationToken)
        {
            await db.YearsOfStudy.LoadAsync(cancellationToken);
            await db.MyAppUsers.LoadAsync(cancellationToken);

            return Ok( await db.YearsOfStudy.Where(s => s.Id == id)
                .Select(yos => new YOSGetResponse
                {
                    Id = yos.Id,
                    StudentId = yos.StudentId,
                    Snimio = yos.Snimio.Email,
                    AkademskaGodinaId = yos.AkademskaGodinaId,
                    AkademskaGodina = yos.AkademskaGodina.Description,
                    Obnova = yos.Obnova,
                    GodinaStudija = yos.GodinaStudija,
                    DatumUpisa = DateOnly.FromDateTime(yos.DatumUpisa),
                }).ToListAsync(cancellationToken));


        }

        public class YOSGetResponse
        {
            public int Id { get; set; }
            public int StudentId { get; set; }
            public string Snimio { get; set; }
            public int AkademskaGodinaId { get; set; }
            public string AkademskaGodina { get; set; }
            public DateOnly DatumUpisa { get; set; }
            public bool Obnova { get; set; }
            public int GodinaStudija { get; set; }
        }
    }
}
