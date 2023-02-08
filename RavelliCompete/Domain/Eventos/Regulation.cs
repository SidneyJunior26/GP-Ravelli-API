using System;
using System.Collections.Generic;

namespace RavelliCompete.Domain.Eventos;

public partial class Regulation {
    public int IdEvento { get; set; }
    public string Regulamento1 { get; set; } = null!;
    public string Compromisso { get; set; } = null!;
}