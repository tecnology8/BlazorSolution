using System;
using System.Collections.Generic;

namespace BlazorSolution.Server.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int DepartamentoId { get; set; }

    public int Sueldo { get; set; }

    public DateTime FechaContrato { get; set; }

    public virtual Departamento Departamento { get; set; } = null!;
}
