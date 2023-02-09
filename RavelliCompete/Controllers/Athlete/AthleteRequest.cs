using System;
namespace RavelliCompete.Controllers.Athlete;

public record AthleteRequest(string Nome, DateTime Nascimento, string Sexo,
    string Cpf, string Rg, string Responsavel, string Endereco, string Numero, string Complemento,
    string Cep, string Cidade, string Uf, string Pais, string Telefone,
    string Celular, string Email, string Profissao, string EmergenciaContato,
    string EmergenciaFone, string EmergenciaCelular, DateTime DataCadastro,
    string Camisa, string CamisaCiclismo, string MktLojaPreferida,
    string MktBikePreferida, string MktAro, string MktCambio,
    string MktFreio, string MktSuspensao, string MktMarcaPneu,
    string MktModeloPneu, string MktTenis, string Acesso,
    bool Ativo, string Federacao);

