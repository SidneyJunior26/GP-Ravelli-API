using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Flunt.Notifications;
using Flunt.Validations;
using RavelliCompete.Controllers.Atletas;

namespace RavelliCompete.Domain.Athletes;

public class Athlete : Notifiable<Notification>
{
    public string Id { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public DateTime Nascimento { get; set; }
    public string Sexo { get; set; } = null!;
    [Key]
    public string Cpf { get; set; } = null!;
    public string? Rg { get; set; }
    public string? Responsavel { get; set; }
    public string Endereco { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string Complemento { get; set; } = null!;
    public string Cep { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Uf { get; set; } = null!;
    public string? Pais { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string Email { get; set; } = null!;
    public string? Profissao { get; set; }
    public string EmergenciaContato { get; set; } = null!;
    public string? EmergenciaFone { get; set; }
    public string? EmergenciaCelular { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public string? Camisa { get; set; }
    public string? CamisaCiclismo { get; set; }
    public string? MktLojaPreferida { get; set; }
    public string? MktBikePreferida { get; set; }
    public string? MktAro { get; set; }
    public string? MktCambio { get; set; }
    public string? MktFreio { get; set; }
    public string? MktSuspensao { get; set; }
    public string? MktMarcapneu { get; set; } = null!;
    public string? NktModelopneu { get; set; } = null!;
    public string? MktTenis { get; set; } = null!;
    public string Acesso { get; set; } = null!;
    public bool Ativo { get; set; }
    public string? Federacao { get; set; }
    public Athlete() { }

    public Athlete(AtletaRequest athlete)
    {
        Id = new Guid().ToString();

        Nome = athlete.Nome;
        Nascimento = athlete.Nascimento;
        Sexo = athlete.Sexo;
        Cpf = athlete.Cpf;
        Rg = athlete.Rg;
        Responsavel = athlete.Responsavel;
        Endereco = athlete.Endereco;
        Numero = athlete.Numero == string.Empty ? "SN" : athlete.Numero;
        Complemento = athlete.Complemento;
        Cep = athlete.Cep;
        Cidade = athlete.Cidade;
        Uf = athlete.Uf;
        Pais = athlete.Pais;
        Telefone = athlete.Telefone;
        Celular = athlete.Celular;
        Email = athlete.Email;
        Profissao = athlete.Profissao;
        EmergenciaContato = athlete.EmergenciaContato;
        EmergenciaFone = athlete.EmergenciaFone;
        EmergenciaCelular = athlete.EmergenciaCelular;
        DataAtualizacao = DateTime.Now;
        Camisa = athlete.Camisa;
        CamisaCiclismo = athlete.CamisaCiclismo;
        MktLojaPreferida = athlete.MktLojaPreferida;
        MktBikePreferida = athlete.MktBikePreferida;
        MktAro = athlete.MktAro;
        MktCambio = athlete.MktCambio;
        MktFreio = athlete.MktFreio;
        MktSuspensao = athlete.MktSuspensao;
        MktMarcapneu = athlete.MktMarcaPneu;
        NktModelopneu = athlete.MktModeloPneu;
        MktTenis = athlete.MktTenis;
        Federacao = athlete.Federacao;

        ValidateAthlete();
    }

    public void EditAthlete(AtletaRequest athlete)
    {
        Nome = athlete.Nome;
        Nascimento = athlete.Nascimento;
        Sexo = athlete.Sexo;
        Responsavel = athlete.Responsavel;
        Endereco = athlete.Endereco;
        Numero = athlete.Numero == string.Empty ? "SN" : athlete.Numero;
        Complemento = athlete.Complemento;
        Cep = athlete.Cep;
        Cidade = athlete.Cidade;
        Uf = athlete.Uf;
        Pais = athlete.Pais;
        Telefone = athlete.Telefone;
        Celular = athlete.Celular;
        Email = athlete.Email;
        Profissao = athlete.Profissao;
        EmergenciaContato = athlete.EmergenciaContato;
        EmergenciaFone = athlete.EmergenciaFone;
        EmergenciaCelular = athlete.EmergenciaCelular;
        DataAtualizacao = DateTime.Now;
        Camisa = athlete.Camisa;
        CamisaCiclismo = athlete.CamisaCiclismo;
        MktLojaPreferida = athlete.MktLojaPreferida;
        MktBikePreferida = athlete.MktBikePreferida;
        MktAro = athlete.MktAro;
        MktCambio = athlete.MktCambio;
        MktFreio = athlete.MktFreio;
        MktSuspensao = athlete.MktSuspensao;
        MktMarcapneu = athlete.MktMarcaPneu;
        NktModelopneu = athlete.MktModeloPneu;
        MktTenis = athlete.MktTenis;
        Federacao = athlete.Federacao;

        ValidateAthlete();
    }

    private void ValidateAthlete()
    {
        var contract = new Contract<Athlete>()
            .IsNotNullOrEmpty(this.Nome, "nome")
            .IsNotNullOrEmpty(this.Nascimento.ToString(), "nascimento")
            .IsNotNullOrEmpty(this.Sexo, "sexo")
            .IsNotNullOrEmpty(this.Endereco, "endereco")
            .IsNotNullOrEmpty(this.Numero, "numero")
            .IsNotNullOrEmpty(this.Cep, "cep")
            .IsNotNullOrEmpty(this.Cidade, "cidade")
            .IsNotNullOrEmpty(this.Uf, "uf")
            .IsNotNullOrEmpty(this.Pais, "pais")
            .IsNotNullOrEmpty(this.Telefone, "telefone")
            .IsNotNullOrEmpty(this.Celular, "celular")
            .IsEmail(this.Email, "email")
            .IsNotNullOrEmpty(this.EmergenciaContato, "emergenciaContato")
            .IsNotNullOrEmpty(this.EmergenciaCelular, "emergenciaCelular")
            .IsNotNullOrEmpty(this.Nascimento.ToString(), "nascimento");

        AddNotifications(contract);
    }
}
