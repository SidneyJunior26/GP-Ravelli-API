using System;
namespace RavelliCompete.Endpoints.Atletas;

public record AthletePostRequest : AthleteRequest
{
    protected AthletePostRequest(AthleteRequest original) : base(original) {
    }

    public string Cpf { get; set; } = null!;
    public string Rg { get; set; } = null!;
}
