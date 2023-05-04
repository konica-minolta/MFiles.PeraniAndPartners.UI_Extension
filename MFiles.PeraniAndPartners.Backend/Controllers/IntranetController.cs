using MFiles.PeraniAndPartners.Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MFiles.PeraniAndPartners.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntranetController : ControllerBase
    {

        private IntranetPeraniContext _intranetPeraniContext;

        public IntranetController(IntranetPeraniContext intranetPeraniContext)
        {
            _intranetPeraniContext = intranetPeraniContext;
        }

        // GET: Intranet
        [EnableCors("_myAllowSpecificOrigins")] // Required for this path.
        [HttpGet]
        public async Task<IActionResult> Get(int currentPage, int pageSize, [FromQuery] string dominio="null")
        {

            var domains = from s in _intranetPeraniContext.vw_domainnames
                          select s;
            if (dominio != "null")
            {
                domains = domains.Where(s => s.NomeDominio ==dominio);
            }
            domains = domains.OrderBy(s => s.NomeDominio);

            if (domains == null) { return NotFound(); }

            return Ok(await PaginatedList<Domain>.CreateAsync(domains.AsNoTracking(), currentPage, pageSize));

        }

      
    }
}
