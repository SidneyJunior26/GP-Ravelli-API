using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using RavelliCompete.Controllers.Eventos;

namespace RavelliCompete.Domain.Eventos;

public partial class Evento : Notifiable<Notification>
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? Local { get; set; }
    public DateTime Data { get; set; }
    public DateTime DataIniInscricao { get; set; }
    public DateTime DataFimInscricao { get; set; }
    public DateTime DataDesconto { get; set; }
    public DateTime? DataValorNormal { get; set; }
    public decimal Valor1 { get; set; }
    public decimal Valor2 { get; set; }
    public decimal? ValorNormal { get; set; }
    public decimal Pacote2V1 { get; set; }
    public decimal Pacote2V2 { get; set; }
    public decimal Pacote2V3 { get; set; }
    public decimal Pacote3V1 { get; set; }
    public decimal Pacote3V2 { get; set; }
    public decimal Pacote3V3 { get; set; }
    public decimal Pacote4V1 { get; set; }
    public decimal Pacote4V2 { get; set; }
    public decimal Pacote4V3 { get; set; }
    public string Pacote1Desc { get; set; } = null!;
    public string Pacote2Desc { get; set; } = null!;
    public string Pacote3Desc { get; set; } = null!;
    public string Pacote4Desc { get; set; } = null!;
    public int? Pacote1Ativo { get; set; }
    public int? Pacote2Ativo { get; set; }
    public int? Pacote3Ativo { get; set; }
    public int? Pacote4Ativo { get; set; }
    public string Categoria { get; set; } = null!;
    public string? BoletoInf1 { get; set; }
    public string? BoletoInf2 { get; set; }
    public string? BoletoInf3 { get; set; }
    public string? BoletoInstrucao1 { get; set; }
    public string? BoletoInstrucao2 { get; set; }
    public string? BoletoInstrucao3 { get; set; }
    public string? ObsTela { get; set; }
    public string? TxtEmailCadastro { get; set; }
    public string? TxtEmailBaixa { get; set; }
    public bool AtivaInscricao { get; set; }
    public bool AtivaEvento { get; set; }
    public bool? EventoTipo { get; set; }
    public string? Pacote1V1Pseg { get; set; }
    public string? Pacote1V2Pseg { get; set; }
    public string? Pacote1V3Pseg { get; set; }
    public string? Pacote2V1Pseg { get; set; }
    public string? Pacote2V2Pseg { get; set; }
    public string? Pacote2V3Pseg { get; set; }
    public string? Pacote3V1Pseg { get; set; }
    public string? Pacote3V2Pseg { get; set; }
    public string? Pacote3V3Pseg { get; set; }
    public string? Pacote4V1Pseg { get; set; }
    public string? Pacote4V2Pseg { get; set; }
    public string? Pacote4V3Pseg { get; set; }
    public Evento() {}

    public Evento(EventoRequest eventoRequest)
    {
        (Nome, Descricao, Local, Data, DataIniInscricao, DataFimInscricao,
         DataDesconto, DataValorNormal, Valor1, Valor2, ValorNormal, Pacote2V1,
         Pacote2V2, Pacote2V3, Pacote3V1, Pacote3V2, Pacote3V3, Pacote4V1,
         Pacote4V2, Pacote4V3, Pacote1Desc, Pacote2Desc, Pacote3Desc, Pacote4Desc,
         Pacote1Ativo, Pacote2Ativo, Pacote3Ativo, Pacote4Ativo, Categoria, BoletoInf1,
         BoletoInf2, BoletoInf3, BoletoInstrucao1, BoletoInstrucao2, BoletoInstrucao3,
         ObsTela, TxtEmailCadastro, TxtEmailBaixa, AtivaInscricao, AtivaEvento, EventoTipo,
         Pacote1V1Pseg, Pacote1V2Pseg, Pacote1V3Pseg, Pacote2V1Pseg, Pacote2V2Pseg,
         Pacote2V3Pseg, Pacote3V1Pseg, Pacote3V2Pseg, Pacote3V3Pseg, Pacote4V1Pseg,
         Pacote4V2Pseg, Pacote4V3Pseg) = eventoRequest;
    }
}
