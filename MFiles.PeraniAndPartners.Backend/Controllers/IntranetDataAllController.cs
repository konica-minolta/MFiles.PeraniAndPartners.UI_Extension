using MFiles.PeraniAndPartners.Backend.Models;
using MFiles.PeraniAndPartners.Backend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Options;
using MailKit;
using MailService = MFiles.PeraniAndPartners.Backend.Services.MailService;

namespace MFiles.PeraniAndPartners.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntranetDataAllController : ControllerBase
    {
        private IntranetPeraniContext _intranetPeraniContext;
        private IConfiguration configuration;
        private readonly MailSettings _mailSettings;
        private string vault_guid;
        private string username;
        private string password;


        public IntranetDataAllController(IntranetPeraniContext intranetPeraniContext, IConfiguration configuration, IOptions<MailSettings> mailSettingsOptions)
        {
            _intranetPeraniContext = intranetPeraniContext;
            vault_guid = configuration.GetValue<string>("Vault_Guid");
            username = configuration.GetValue<string>("MFilesUser");
            password = configuration.GetValue<string>("Password");
            _mailSettings = mailSettingsOptions.Value;
        }

        // GET: Intranet
        // GET: Intranet
        [EnableCors("_myAllowSpecificOrigins")] // Required for this path.
        [HttpGet]
        public async Task<FileResult> Get(string dominio = "null", string estensione = "null", bool ricercaEsatta = false, DateTime? scadenzaDal = null, DateTime? scadenzaAl = null)
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

            //List<Domain> domainsToExport = domains.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList<Domain>();
            List<Domain> domainsToExport = domains.ToList<Domain>();
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
