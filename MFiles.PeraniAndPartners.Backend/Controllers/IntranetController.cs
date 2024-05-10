using MFiles.PeraniAndPartners.Backend.Helpers;
using MFiles.PeraniAndPartners.Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml.FormulaParsing.Utilities;
using System;
using System.Linq.Expressions;
using System.Transactions;

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
        public async Task<IActionResult> Get(int currentPage, int pageSize, string searchParam = "null", int order = 1, string estensione = "null", bool ricercaEsatta = false, DateTime? scadenzaDal = null, DateTime? scadenzaAl = null, string stato = "QUALSIASI", string tipoRicerca = "Dominio")
        {
            IQueryable<Domain> domains = GetDomains(searchParam, order, estensione, ricercaEsatta, scadenzaDal, scadenzaAl, stato, tipoRicerca);

            if (domains == null) { return NotFound(); }
            PaginatedList<Domain> resultPaginated = PaginatedList<Domain>.CreateAsync(domains, currentPage, pageSize);
            Result result = new Result(domains.Count(), resultPaginated);
            return Ok(result);
            //   }         
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IQueryable<Domain> GetDomains(string searchParam, int order, string estensione, bool ricercaEsatta, DateTime? scadenzaDal, DateTime? scadenzaAl, string stato, string tipoRicerca)
        {
            var domains = from s in _intranetPeraniContext.vw_domainnames
                          select s;

            if (tipoRicerca.ToLower() == "tutti i campi")
            {
                domains = SearchParamConfigurationAllFields(searchParam, ricercaEsatta, tipoRicerca, domains);
            }
            else
            {
                domains = SearchParamConfiguration(searchParam, ricercaEsatta, tipoRicerca, domains);
            }

            if (estensione != "null")
            {
                Expression<Func<Domain, bool>> predicate = BuilderPredicateOrCondtion(estensione, "Estensione");
                domains = domains.Where(predicate);
                //domains = domains.Where(s => s.Estensione == estensione);
            }

            if (scadenzaDal != null && scadenzaAl != null)
            {
                domains = domains.Where(s => s.DataScadenza >= scadenzaDal && s.DataScadenza <= scadenzaAl);
            }

            if (!stato.Contains("QUALSIASI"))
            {
                Expression<Func<Domain, bool>> predicate = BuilderPredicateOrCondtion(stato, "Stato");
                //domains = domains.Where(s => s.Stato == multipleCondtionValue);
                domains = domains.Where(predicate);
            }
            if (order == 1)
            {
                domains = domains.OrderBy(s => s.NomeDominio);
            }
            else if (order == 2)
            {
                domains = domains.OrderByDescending(s => s.NomeDominio);
            }
            else if (order == 3)
            {
                domains = domains.OrderBy(s => s.DataRegistrazione);
            }
            else if (order == 4)
            {
                domains = domains.OrderByDescending(s => s.DataRegistrazione);
            }
            else if (order == 5)
            {
                domains = domains.OrderBy(s => s.DataScadenza);
            }
            else if (order == 6)
            {
                domains = domains.OrderByDescending(s => s.DataScadenza);
            }

            return domains;
        }

        private static Expression<Func<Domain, bool>> BuilderPredicateOrCondtion(string multipleCondtionValue, string property)
        {
            string[] condtionValue = multipleCondtionValue.Split(',');
            var predicate = PredicateBuilderHelper.False<Domain>();
            foreach (string condition in condtionValue)
            {
                if (string.IsNullOrEmpty(condition))
                    continue;
                switch (property)
                {
                    case "Stato":
                        predicate = predicate.OR(s => s.Stato == condition);
                        break;
                    case "Estensione":
                        predicate = predicate.OR(s => s.Estensione == condition);
                        break;
                }
            }

            return predicate;
        }

        private IQueryable<Domain> SearchParamConfiguration(string searchParam, bool ricercaEsatta, string tipoRicerca, IQueryable<Domain> domains)
        {
            if (tipoRicerca == "Dominio")
            {
                if (searchParam != "null")
                {
                    if (ricercaEsatta)
                    {
                        domains = domains.Where(s => s.NomeDominio == searchParam);
                    }
                    else
                    {
                        domains = domains.Where(s => s.NomeDominio.Contains(searchParam));
                    }
                }
            }
            else if (tipoRicerca == "Provider")
            {
                if (searchParam != "null")
                {
                    if (ricercaEsatta)
                    {
                        domains = domains.Where(s => s.Provider == searchParam);
                    }
                    else
                    {
                        domains = domains.Where(s => s.Provider.Contains(searchParam));
                    }
                }
            }
            else if (tipoRicerca == "Riferimento")
            {
                if (searchParam != "null")
                {
                    if (ricercaEsatta)
                    {
                        domains = domains.Where(s => s.NumeroPratica == searchParam);
                    }
                    else
                    {
                        domains = domains.Where(s => s.NumeroPratica.Contains(searchParam));
                    }
                }
            }
            else if (tipoRicerca == "Registrar")
            {
                if (searchParam != "null")
                {
                    if (ricercaEsatta)
                    {
                        domains = domains.Where(s => s.Registrar == searchParam);
                    }
                    else
                    {
                        domains = domains.Where(s => s.Registrar.Contains(searchParam));
                    }
                }
            }
            else if (tipoRicerca == "Owner")
            {
                if (searchParam != "null")
                {
                    if (ricercaEsatta)
                    {
                        domains = domains.Where(s => s.Owner == searchParam);
                    }
                    else
                    {
                        domains = domains.Where(s => s.Owner.Contains(searchParam));
                    }
                }
            }
            else if (tipoRicerca == "Cliente")
            {
                if (searchParam != "null")
                {
                    if (ricercaEsatta)
                    {
                        domains = domains.Where(s => s.Cliente == searchParam);
                    }
                    else
                    {
                        domains = domains.Where(s => s.Cliente.Contains(searchParam));
                    }
                }
            }

            return domains;
        }

        private IQueryable<Domain> SearchParamConfigurationAllFields(string searchParam, bool ricercaEsatta, string tipoRicerca, IQueryable<Domain> domains)
        {
            var predicate = PredicateBuilderHelper.False<Domain>();
            if (ricercaEsatta)
            {
                predicate = predicate.OR(s => s.NomeDominio == searchParam);
            }
            else
            {
                predicate = predicate.OR(s => s.NomeDominio.Contains(searchParam));
            }


            if (ricercaEsatta)
            {
                predicate = predicate.OR(s => s.Provider == searchParam);
            }
            else
            {
                predicate = predicate.OR(s => s.Provider.Contains(searchParam));
            }

            if (ricercaEsatta)
            {
                predicate = predicate.OR(s => s.NumeroPratica == searchParam);
            }
            else
            {
                predicate = predicate.OR(s => s.NumeroPratica.Contains(searchParam));
            }


            if (ricercaEsatta)
            {
                predicate = predicate.OR(s => s.Registrar == searchParam);
            }
            else
            {
                predicate = predicate.OR(s => s.Registrar.Contains(searchParam));
            }

            if (ricercaEsatta)
            {
                predicate = predicate.OR(s => s.Owner == searchParam);
            }
            else
            {
                predicate = predicate.OR(s => s.Owner.Contains(searchParam));
            }


            if (ricercaEsatta)
            {
                predicate = predicate.OR(s => s.Cliente == searchParam);
            }
            else
            {
                predicate = predicate.OR(s => s.Cliente.Contains(searchParam));
            }

            return domains.Where(predicate);
        }

        public class Result
        {
            public int TotalRecords { get; set; }
            public PaginatedList<Domain> List { get; set; }

            public Result(int totalRecords, PaginatedList<Domain> list)
            {
                this.List = list;
                this.TotalRecords = totalRecords;
            }
        }

    }
}
