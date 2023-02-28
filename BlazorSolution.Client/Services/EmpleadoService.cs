using BlazorSolution.Shared;
using System.Net.Http.Json;

namespace BlazorSolution.Client.Services
{
    public class EmpleadoService         : IEmpleadoService
    {
        private readonly HttpClient _httpClient;
        public EmpleadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmpleadoDto>> Lista()
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<EmpleadoDto>>>("api/Empleado/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
        public async Task<EmpleadoDto> Buscar(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ResponseAPI<EmpleadoDto>>($"api/Empleado/Buscar/{id}");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
        public async Task<int> Guardar(EmpleadoDto empleado)
        {
            var result = await _httpClient.PostAsJsonAsync($"api/Empleado/Guardar", empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }
        public async Task<int> Editar(EmpleadoDto empleado)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Empleado/Editar/{empleado.Id}", empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }
        public async Task<bool> Eliminar(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Empleado/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}