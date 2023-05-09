using MFiles.PeraniAndPartners.Backend.Models;
using MFiles.PeraniAndPartners.Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MFiles.PeraniAndPartners.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntranetDataPaginatedController : ControllerBase
    {
        private IntranetPeraniContext _intranetPeraniContext;

        public IntranetDataPaginatedController(IntranetPeraniContext intranetPeraniContext)
        {
            _intranetPeraniContext = intranetPeraniContext;
        }

        // GET: Intranet
        [EnableCors("_myAllowSpecificOrigins")] // Required for this path.
        [HttpGet]
        public async Task<FileResult> Get(int currentPage, int pageSize, string dominio = "null", string estensione = "null", bool ricercaEsatta = false, DateTime? scadenzaDal = null, DateTime? scadenzaAl = null)
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
                domains = domains.Where(s => s.DataScadenza >= scadenzaDal && s.DataScadenza <= scadenzaAl);
            }
            domains = domains.OrderBy(s => s.NomeDominio);

            List<Domain> domainsToExport = domains.Skip((currentPage-1) * pageSize).Take(pageSize).ToList<Domain>();
            //List<Domain> domainsToExport = domains.ToList<Domain>();
            var bytes = ExcelService.ListToExcel<Domain>(domainsToExport);

            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Response.ContentType = contentType;
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            var fileContentResult = new FileContentResult(bytes, contentType)
            {
                FileDownloadName = "Intranet_Export.xlsx"
            };

            return fileContentResult;
        }
    }
}
