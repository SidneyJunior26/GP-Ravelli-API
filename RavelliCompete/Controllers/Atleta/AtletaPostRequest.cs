using System;
namespace RavelliCompete.Controllers.Atleta;

public record AtletaPostRequest : AtletaRequest
{
    protected AtletaPostRequest(AtletaRequest original) : base(original) {
    }

    public string Cpf { get; set; } = null!;
    public string Rg { get; set; } = null!;
}
