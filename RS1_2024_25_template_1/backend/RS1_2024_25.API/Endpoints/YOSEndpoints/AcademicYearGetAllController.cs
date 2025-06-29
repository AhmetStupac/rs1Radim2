using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;

namespace RS1_2024_25.API.Endpoints.YOSEndpoints
{
    [Route("academic-year")]
    [ApiController]
    public class AcademicYearGetAllController(ApplicationDbContext db) : ControllerBase
    {

        [HttpGet("get")]
        public async Task<ActionResult<List<AcademicYearGetAllResponse>>> handleAsync(CancellationToken cancellationToken)
        {
            var result = await db.AcademicYears
                .Select(ay => new AcademicYearGetAllResponse
                {
                    Id = ay.ID,
                    Name = ay.Description,
                }).ToListAsync(cancellationToken);


            return Ok(result);
        }


        public class AcademicYearGetAllResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
