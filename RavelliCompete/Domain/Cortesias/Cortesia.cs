using System;
using System.Collections.Generic;

namespace RavelliCompete.Domain.Cortesias;

public partial class Cortesia
{
    public string Cupom { get; set; } = null!;
    public int IdEvento { get; set; }
    public string Email { get; set; } = null!;
    public int? IdInscricao { get; set; }
}
