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
        public async Task<IActionResult> Get(int currentPage, int pageSize, string dominio="null",string estensione ="null", bool ricercaEsatta = false, DateTime? scadenzaDal = null, DateTime? scadenzaAl = null)
        {

            var domains = from s in _intranetPeraniContext.vw_domainnames
                          select s;
            if (dominio != "null")
            {
                if (ricercaEsatta)
                {
                    domains = domains.Where(s => s.NomeDominio == dominio);
                }
                else
                {
                    domains = domains.Where(s => s.NomeDominio.Contains(dominio));
                }  
            }
            if (estensione != "null")
            {
                domains = domains.Where(s => s.Estensione == estensione);
            }

            if (scadenzaDal!=null && scadenzaAl!=null)
            {
                domains = domains.Where(s => s.DataScadenza >= scadenzaDal && s.DataScadenza<=scadenzaAl);
            }
            domains = domains.OrderBy(s => s.NomeDominio);

            if (domains == null) { return NotFound(); }

            return Ok(await PaginatedList<Domain>.CreateAsync(domains.AsNoTracking(), currentPage, pageSize));

        }

      
    }
}
