using BlazorSolution.Shared;
using System.Net.Http.Json;

namespace BlazorSolution.Client.Services
{
    public class DepartamentoService  : IDepartamentoService
    {
        private readonly HttpClient _httpClient;
        public DepartamentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<DepartamentoDto>> Lista()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<DepartamentoDto>>>("api/Departamento/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
    }
}
