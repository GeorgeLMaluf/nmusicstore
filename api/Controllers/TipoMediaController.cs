using System;
using System.Collections.Generic;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TipoMediaController: ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll(string intervalo, int pg)
        {
            var service = new TipoMediaService();
            try
            {
                var retorno = new RetornoPesquisa {
                    total = service.BuscarCount(intervalo),
                    itens = service.BuscarAll(intervalo, pg)
                };
                return new ObjectResult(retorno);
            }
            catch(Exception) {
                return NotFound();
            }
        }
    }
}