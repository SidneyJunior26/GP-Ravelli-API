using System;
namespace RavelliCompete.Endpoints.Security;

public record LoginRequest(string Cpf, string Password);