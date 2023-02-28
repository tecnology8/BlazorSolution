using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorSolution.Server.Models;
using BlazorSolution.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorSolution.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly DbblazorContext _context;
        public DepartamentoController(DbblazorContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<DepartamentoDto>>();
            var lista = new List<DepartamentoDto>();

            try
            {
                foreach (var item in  await _context.Departamentos.ToListAsync())
                {
                    lista.Add(new DepartamentoDto
                    {
                        Id = item.Id,
                        Nombre  = item.Nombre,
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = lista;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

    }
}