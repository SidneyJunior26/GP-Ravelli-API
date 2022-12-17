using System;
using System.Collections.Generic;

namespace RavelliCompete.Domain.Eventos;

public partial class Subcategoria
{
    public int Id { get; set; }
    public int IdEvento { get; set; }
    public int Categoria { get; set; }
    public string DescSubcategoria { get; set; } = null!;
    public bool? FiltroSexo { get; set; }
    public bool? FiltroDupla { get; set; }
    public sbyte? IdadeDe { get; set; }
    public sbyte? IdadeAte { get; set; }
    public string? Aviso { get; set; }
    public bool Ativo { get; set; }
}
