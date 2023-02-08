using System;
using System.Collections.Generic;

namespace RavelliCompete.Domain.Athletes;

public partial class MedicalRecord
{
    public string IdAtleta { get; set; } = null!;
    public bool? Plano { get; set; }
    public string? PlanoEmpresa { get; set; }
    public string? PlanoTipo { get; set; }
    public bool Pressaoalta { get; set; }
    public bool Desmaio { get; set; }
    public bool Cardiaco { get; set; }
    public bool Diabetes { get; set; }
    public bool Asma { get; set; }
    public bool Alergia { get; set; }
    public string? AlergiaQual { get; set; }
    public bool Cirurgia { get; set; }
    public string? CirurgiaQual { get; set; }
    public bool Medicacao { get; set; }
    public string? MedicacaoQual { get; set; }
    public string? MedicacaoTempo { get; set; }
    public bool Malestar { get; set; }
    public string? MalestarQual { get; set; }
    public bool Acompanhamento { get; set; }
    public string? AcompanhamentoQual { get; set; }
    public string? Outros { get; set; }
}
