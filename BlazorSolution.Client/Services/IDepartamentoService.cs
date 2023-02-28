using BlazorSolution.Shared;

namespace BlazorSolution.Client.Services
{
    public interface IDepartamentoService
    {
        Task<List<DepartamentoDto>> Lista();
    }
}
