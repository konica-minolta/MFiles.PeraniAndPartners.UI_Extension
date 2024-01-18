using MFiles.PeraniAndPartners.Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Utilities;
using System.Transactions;

namespace MFiles.PeraniAndPartners.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {

        private IntranetPeraniContext _intranetPeraniContext;

        public SearchController(IntranetPeraniContext intranetPeraniContext)
        {
            _intranetPeraniContext = intranetPeraniContext;
        }

        // GET: Intranet
        [EnableCors("_myAllowSpecificOrigins")] // Required for this path.
        [HttpGet]
        public async Task<IActionResult> Get(string label)
        {

            Dictionary<string, object> resp = new Dictionary<string, object>();

            switch (label)
            {
                case "domini":
                    var domainsFiltered = _intranetPeraniContext.vw_domainnames.GroupBy(p => p.Estensione).Select(g => g.First()).ToList();
                    resp.Add("domini", domainsFiltered.Select(m => m.Estensione).ToList());
                    break;
            }

            if (resp.Count == 0)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(resp));

        }
    }
}
