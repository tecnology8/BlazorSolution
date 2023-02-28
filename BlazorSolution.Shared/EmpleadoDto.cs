using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorSolution.Shared
{
    public class EmpleadoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Campo {O} es requerido")]
        public string Nombre { get; set; } = null!;

        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "El Campo {O} es requerido")]
        public int DepartamentoId { get; set; }

        public int Sueldo { get; set; }

        public DateTime FechaContrato { get; set; }

        public DepartamentoDto? Departamento { get; set; }
    }
}
