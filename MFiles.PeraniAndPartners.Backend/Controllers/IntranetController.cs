﻿using MFiles.PeraniAndPartners.Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Get(int currentPage, int pageSize, string dominio = "null", int order = 1,string estensione = "null", bool ricercaEsatta = false, DateTime? scadenzaDal = null, DateTime? scadenzaAl = null, string stato = "QUALSIASI")
        {

            using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
            }, TransactionScopeAsyncFlowOption.Enabled))
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

                if (stato != "QUALSIASI")
                {
                    domains = domains.Where(s => s.Stato == stato);
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

                if (domains == null) { return NotFound(); }

                Result result = new Result(domains.Count(), PaginatedList<Domain>.CreateAsync(domains, currentPage, pageSize));
                return Ok(result);
            }         
        }

        public class Result
        {
            public int TotalRecords { get; set; }
            public PaginatedList<Domain> List { get; set; }

            public Result  (int totalRecords, PaginatedList<Domain> list)
            {
                this.List = list;
                this.TotalRecords = totalRecords;
            }   
        }
      
    }
}
