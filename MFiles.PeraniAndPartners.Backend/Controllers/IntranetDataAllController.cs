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

        [EnableCors("_myAllowSpecificOrigins")] // Required for this path.
        [HttpGet]
        public async Task<FileResult> Get(int currentPage, int pageSize, string searchParam = "null", string estensione = "null", bool ricercaEsatta = false, DateTime? scadenzaDal = null, DateTime? scadenzaAl = null, string stato = "QUALSIASI", string tipoRicerca = "Dominio")
        {
            IntranetController IntranetController = new(_intranetPeraniContext);
            IQueryable<Domain> result = IntranetController.GetDomains(searchParam, 1, estensione, ricercaEsatta, scadenzaDal, scadenzaAl, stato, tipoRicerca);
            List<Domain> domainsToExport = result.ToList();

            if (currentPage != -1 && pageSize != -1)
            {
                PaginatedList<Domain> resultPaginated = PaginatedList<Domain>.CreateAsync(result, currentPage, pageSize);
                domainsToExport = resultPaginated.ToList();
            }

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
