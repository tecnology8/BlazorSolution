using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorSolution.Server.Models;
using BlazorSolution.Shared;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorSolution.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DbblazorContext _context;
        public EmpleadoController(DbblazorContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EmpleadoDto>>();
            var lista = new List<EmpleadoDto>();

            try
            {
                foreach (var item in await _context.Empleados.Include(d => d.Departamento).ToListAsync())
                {
                    lista.Add(new EmpleadoDto
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        DepartamentoId = item.DepartamentoId,
                        Sueldo = item.Sueldo,
                        FechaContrato = item.FechaContrato,
                        Departamento = new DepartamentoDto
                        {
                            Id = item.Id,
                            Nombre = item.Nombre
                        }
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

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<EmpleadoDto>();
            var empleadoDto = new EmpleadoDto();

            try
            {
                var empleado = await _context.Empleados.FirstOrDefaultAsync(x => x.Id == id);

                if (empleado != null)
                {
                    empleadoDto.Id = empleado.Id;
                    empleadoDto.Nombre = empleado.Nombre;
                    empleadoDto.DepartamentoId = empleado.DepartamentoId;
                    empleadoDto.Sueldo = empleado.Sueldo;
                    empleadoDto.FechaContrato = empleado.FechaContrato;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = empleadoDto;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encuentra el empleado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(EmpleadoDto empleado)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbempleado = new Empleado()
                {
                    Nombre = empleado.Nombre,
                    DepartamentoId = empleado.DepartamentoId,
                    Sueldo = empleado.Sueldo,
                    FechaContrato = empleado.FechaContrato
                };

                _context.Empleados.Add(dbempleado);
                await _context.SaveChangesAsync();


                if (dbempleado.Id != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbempleado.Id;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudo guardar el empleado";
                }


            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }


        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(EmpleadoDto empleado, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbempleado = await _context.Empleados.FirstOrDefaultAsync(x => x.Id == id);

                if (dbempleado != null)
                {
                    dbempleado.Nombre = empleado.Nombre;
                    dbempleado.DepartamentoId = empleado.DepartamentoId;
                    dbempleado.Sueldo = empleado.Sueldo;
                    dbempleado.FechaContrato = empleado.FechaContrato;

                    _context.Empleados.Update(dbempleado);
                    await _context.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbempleado.Id;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no se encuentra";
                }


            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbempleado = await _context.Empleados.FirstOrDefaultAsync(x => x.Id == id);

                if (dbempleado != null)
                {
                    _context.Empleados.Remove(dbempleado);
                    await _context.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no se encuentra";
                }
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