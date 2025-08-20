using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Infra.Db;

namespace MinimalApi.Domain
{
    [ApiController]
    [Route("[controller]")]
    public class BoxController : ControllerBase
    {
        private readonly DbContexto _ctx;
        public BoxController(DbContexto ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("all")]
        async public Task<IActionResult> GetAll()
        {
            var adms = await _ctx.Administrators.ToListAsync();
            return Ok(adms);
        }
    }
}
