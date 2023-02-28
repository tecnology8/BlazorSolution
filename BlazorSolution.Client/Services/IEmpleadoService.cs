using BlazorSolution.Shared;

namespace BlazorSolution.Client.Services
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDto>> Lista();
        Task<EmpleadoDto> Buscar(int id);
        Task<int> Guardar(EmpleadoDto empleado);
        Task<int> Editar(EmpleadoDto empleado);
        Task<bool> Eliminar(int id);
    }
}