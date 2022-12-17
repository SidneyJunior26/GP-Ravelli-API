using System;
using System.Collections.Generic;

namespace RavelliCompete.Domain.Eventos;

public partial class Inscricao
{
    public int Id { get; set; }
    public int IdEvento { get; set; }
    public string CpfAtleta { get; set; } = null!;
    public int IdSubcategoria { get; set; }
    public string? Equipe { get; set; }
    public string? Dupla { get; set; }
    public string? Quarteto { get; set; }
    public string? Numeral { get; set; }
    public DateOnly? DataInscricao { get; set; }
    public DateOnly? DataEfetivacao { get; set; }
    public bool? Pago { get; set; }
    public decimal? ValorPago { get; set; }
    public int? Pacote { get; set; }
    public bool AceiteRegulamento { get; set; }
    public bool? Cancelado { get; set; }
    public DateOnly? GnExpireAt { get; set; }
    public int? GnChargeId { get; set; }
    public int? GnTotal { get; set; }
    public string? GnLink { get; set; }
    public string? GnBarcode { get; set; }
    public string? GnStatus { get; set; }
}
