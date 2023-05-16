using MFiles.PeraniAndPartners.Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MFiles.PeraniAndPartners.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntranetCounterController : ControllerBase
    {

        private IntranetPeraniContext _intranetPeraniContext;

        public IntranetCounterController(IntranetPeraniContext intranetPeraniContext)
        {
            _intranetPeraniContext = intranetPeraniContext;
        }

        // GET: Intranet
        [EnableCors("_myAllowSpecificOrigins")] // Required for this path.
        [HttpGet]
        public async Task<IActionResult> Get(int pageSize, string dominio="null", string estensione = "null",bool ricercaEsatta=false, DateTime? scadenza = null, DateTime? scadenzaDal = null, DateTime? scadenzaAl = null, string stato = "QUALSIASI")
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
            if (scadenzaDal != null && scadenzaAl != null)
            {
               scadenzaDal.Value.ToLocalTime();
               domains = domains.Where(s => s.DataScadenza >= scadenzaDal && s.DataScadenza <= scadenzaAl);
            }

            if (stato!="QUALSIASI")
            {
                domains = domains.Where(s => s.Stato == stato);
            }
            int total;
           
            total = domains.Count();
            
            return Ok(total);

        }

      
    }
}
