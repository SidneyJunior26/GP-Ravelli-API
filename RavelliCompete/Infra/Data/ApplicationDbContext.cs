using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using RavelliCompete.Domain.Atletas;
using RavelliCompete.Domain.Cortesias;
using RavelliCompete.Domain.Eventos;

namespace RavelliCompete.Infra.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RegistroMedico> RegistrosMedicos { get; set; } = null!;
    public virtual DbSet<Atleta> Atletas { get; set; } = null!;
    public virtual DbSet<Cortesia> Cortesias { get; set; } = null!;
    public virtual DbSet<Evento> Eventos { get; set; } = null!;
    public virtual DbSet<Inscricao> Inscricoes { get; set; } = null!;
    public virtual DbSet<Regulamento> Regulamentos { get; set; } = null!;
    public virtual DbSet<Subcategoria> Subcategoria { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("server=localhost;port=8889;database=RavelliCompetes;uid=root;pwd=root;connection timeout=300;default command timeout=300", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.34-mysql"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();

        modelBuilder.UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<RegistroMedico>(entity =>
        {
            entity.HasKey(e => e.IdAtleta)
                .HasName("PRIMARY");

            entity.ToTable("atleta_medico");

            entity.Property(e => e.IdAtleta)
                .HasMaxLength(32)
                .HasColumnName("id_atleta");

            entity.Property(e => e.Acompanhamento).HasColumnName("acompanhamento");

            entity.Property(e => e.AcompanhamentoQual)
                .HasMaxLength(255)
                .HasColumnName("acompanhamento_qual");

            entity.Property(e => e.Alergia).HasColumnName("alergia");

            entity.Property(e => e.AlergiaQual)
                .HasMaxLength(255)
                .HasColumnName("alergia_qual");

            entity.Property(e => e.Asma).HasColumnName("asma");

            entity.Property(e => e.Cardiaco).HasColumnName("cardiaco");

            entity.Property(e => e.Cirurgia).HasColumnName("cirurgia");

            entity.Property(e => e.CirurgiaQual)
                .HasMaxLength(255)
                .HasColumnName("cirurgia_qual");

            entity.Property(e => e.Desmaio).HasColumnName("desmaio");

            entity.Property(e => e.Diabetes).HasColumnName("diabetes");

            entity.Property(e => e.Malestar).HasColumnName("malestar");

            entity.Property(e => e.MalestarQual)
                .HasMaxLength(255)
                .HasColumnName("malestar_qual");

            entity.Property(e => e.Medicacao).HasColumnName("medicacao");

            entity.Property(e => e.MedicacaoQual)
                .HasMaxLength(255)
                .HasColumnName("medicacao_qual");

            entity.Property(e => e.MedicacaoTempo)
                .HasMaxLength(60)
                .HasColumnName("medicacao_tempo");

            entity.Property(e => e.Outros)
                .HasColumnType("text")
                .HasColumnName("outros");

            entity.Property(e => e.Plano).HasColumnName("plano");

            entity.Property(e => e.PlanoEmpresa)
                .HasMaxLength(60)
                .HasColumnName("plano_empresa");

            entity.Property(e => e.PlanoTipo)
                .HasMaxLength(60)
                .HasColumnName("plano_tipo");

            entity.Property(e => e.Pressaoalta).HasColumnName("pressaoalta");
        });

        modelBuilder.Entity<Atleta>(entity =>
        {
            entity.ToTable("atleta");

            entity.HasKey(e => e.Cpf)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.Cpf, "cpf")
                .IsUnique();

            entity.Property(e => e.Acesso)
                .HasMaxLength(128)
                .HasColumnName("acesso");

            entity.Property(e => e.Ativo).HasColumnName("ativo");

            entity.Property(e => e.Camisa)
                .HasMaxLength(25)
                .HasColumnName("camisa");

            entity.Property(e => e.CamisaCiclismo)
                .HasMaxLength(30)
                .HasColumnName("camisa_ciclismo");

            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .HasColumnName("celular");

            entity.Property(e => e.Cep)
                .HasMaxLength(9)
                .HasColumnName("cep");

            entity.Property(e => e.Cidade)
                .HasMaxLength(30)
                .HasColumnName("cidade");

            entity.Property(e => e.Complemento)
                .HasMaxLength(30)
                .HasColumnName("complemento");

            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("cpf");

            entity.Property(e => e.DataAtualizacao).HasColumnName("data_atualizacao");

            entity.Property(e => e.DataCadastro).HasColumnName("data_cadastro");

            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .HasColumnName("email");

            entity.Property(e => e.EmergenciaCelular)
                .HasMaxLength(15)
                .HasColumnName("emergencia_celular");

            entity.Property(e => e.EmergenciaContato)
                .HasMaxLength(60)
                .HasColumnName("emergencia_contato");

            entity.Property(e => e.EmergenciaFone)
                .HasMaxLength(15)
                .HasColumnName("emergencia_fone");

            entity.Property(e => e.Endereco)
                .HasMaxLength(60)
                .HasColumnName("endereco");

            entity.Property(e => e.Federacao)
                .HasMaxLength(7)
                .HasColumnName("federacao");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasColumnName("id");

            entity.Property(e => e.MktAro)
                .HasMaxLength(10)
                .HasColumnName("mkt_aro");

            entity.Property(e => e.MktBikePreferida)
                .HasMaxLength(128)
                .HasColumnName("mkt_bike_preferida");

            entity.Property(e => e.MktCambio)
                .HasMaxLength(128)
                .HasColumnName("mkt_cambio");

            entity.Property(e => e.MktFreio)
                .HasMaxLength(128)
                .HasColumnName("mkt_freio");

            entity.Property(e => e.MktLojaPreferida)
                .HasMaxLength(128)
                .HasColumnName("mkt_loja_preferida");

            entity.Property(e => e.MktMarcapneu)
                .HasMaxLength(120)
                .HasColumnName("mkt_marcapneu");

            entity.Property(e => e.MktSuspensao)
                .HasMaxLength(128)
                .HasColumnName("mkt_suspensao");

            entity.Property(e => e.MktTenis)
                .HasMaxLength(255)
                .HasColumnName("mkt_tenis");

            entity.Property(e => e.Nascimento).HasColumnName("nascimento");

            entity.Property(e => e.NktModelopneu)
                .HasMaxLength(120)
                .HasColumnName("nkt_modelopneu");

            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .HasColumnName("nome");

            entity.Property(e => e.Numero)
                .HasMaxLength(6)
                .HasColumnName("numero");

            entity.Property(e => e.Pais)
                .HasMaxLength(25)
                .HasColumnName("pais");

            entity.Property(e => e.Profissao)
                .HasMaxLength(60)
                .HasColumnName("profissao");

            entity.Property(e => e.Responsavel)
                .HasMaxLength(60)
                .HasColumnName("responsavel");

            entity.Property(e => e.Rg)
                .HasMaxLength(20)
                .HasColumnName("rg");

            entity.Property(e => e.Sexo).HasColumnName("sexo");

            entity.Property(e => e.Telefone)
                .HasMaxLength(15)
                .HasColumnName("telefone");

            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .HasColumnName("uf");

            entity.Property(e => e.Nivel)
                .HasMaxLength(1)
                .HasColumnName("nivel");
        });

        modelBuilder.Entity<Cortesia>(entity =>
        {
            entity.HasKey(e => e.Cupom)
                .HasName("PRIMARY");

            entity.ToTable("cortesia");

            entity.Property(e => e.Cupom)
                .HasMaxLength(20)
                .HasColumnName("cupom");

            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .HasColumnName("email");

            entity.Property(e => e.IdEvento)
                .HasColumnType("int(11)")
                .HasColumnName("id_evento");

            entity.Property(e => e.IdInscricao)
                .HasColumnType("int(11)")
                .HasColumnName("id_inscricao");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.ToTable("evento");

            entity.HasKey(e => e.Id)
                .HasName("id");

            entity.HasIndex(e => e.Id, "id")
                .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.AtivaEvento).HasColumnName("ativa_evento");

            entity.Property(e => e.AtivaInscricao).HasColumnName("ativa_inscricao");

            entity.Property(e => e.BoletoInf1)
                .HasMaxLength(60)
                .HasColumnName("boleto_inf1");

            entity.Property(e => e.BoletoInf2)
                .HasMaxLength(60)
                .HasColumnName("boleto_inf2");

            entity.Property(e => e.BoletoInf3)
                .HasMaxLength(60)
                .HasColumnName("boleto_inf3");

            entity.Property(e => e.BoletoInstrucao1)
                .HasMaxLength(60)
                .HasColumnName("boleto_instrucao1");

            entity.Property(e => e.BoletoInstrucao2)
                .HasMaxLength(60)
                .HasColumnName("boleto_instrucao2");

            entity.Property(e => e.BoletoInstrucao3)
                .HasMaxLength(60)
                .HasColumnName("boleto_instrucao3");

            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");

            entity.Property(e => e.Data).HasColumnName("data");

            entity.Property(e => e.DataDesconto).HasColumnName("data_desconto");

            entity.Property(e => e.DataFimInscricao).HasColumnName("data_fim_inscricao");

            entity.Property(e => e.DataIniInscricao).HasColumnName("data_ini_inscricao");

            entity.Property(e => e.DataValorNormal).HasColumnName("data_valor_normal");

            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");

            entity.Property(e => e.EventoTipo).HasColumnName("evento_tipo");

            entity.Property(e => e.Local)
                .HasMaxLength(255)
                .HasColumnName("local");

            entity.Property(e => e.Nome)
                .HasMaxLength(80)
                .HasColumnName("nome");

            entity.Property(e => e.ObsTela)
                .HasColumnType("text")
                .HasColumnName("obs_tela");

            entity.Property(e => e.Pacote1Ativo).HasColumnName("pacote1_ativo");

            entity.Property(e => e.Pacote1Desc)
                .HasMaxLength(255)
                .HasColumnName("pacote1_desc");

            entity.Property(e => e.Pacote1V1Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote1_v1_pseg");

            entity.Property(e => e.Pacote1V2Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote1_v2_pseg");

            entity.Property(e => e.Pacote1V3Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote1_v3_pseg");

            entity.Property(e => e.Pacote2Ativo)
                .HasColumnType("int(1)")
                .HasColumnName("pacote2_ativo");

            entity.Property(e => e.Pacote2Desc)
                .HasMaxLength(255)
                .HasColumnName("pacote2_desc");

            entity.Property(e => e.Pacote2V1)
                .HasPrecision(6, 2)
                .HasColumnName("pacote2_v1");

            entity.Property(e => e.Pacote2V1Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote2_v1_pseg");

            entity.Property(e => e.Pacote2V2)
                .HasPrecision(6, 2)
                .HasColumnName("pacote2_v2");

            entity.Property(e => e.Pacote2V2Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote2_v2_pseg");

            entity.Property(e => e.Pacote2V3)
                .HasPrecision(6, 2)
                .HasColumnName("pacote2_v3");

            entity.Property(e => e.Pacote2V3Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote2_v3_pseg");

            entity.Property(e => e.Pacote3Ativo)
                .HasColumnType("int(1)")
                .HasColumnName("pacote3_ativo");

            entity.Property(e => e.Pacote3Desc)
                .HasMaxLength(255)
                .HasColumnName("pacote3_desc");

            entity.Property(e => e.Pacote3V1)
                .HasPrecision(6, 2)
                .HasColumnName("pacote3_v1");

            entity.Property(e => e.Pacote3V1Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote3_v1_pseg");

            entity.Property(e => e.Pacote3V2)
                .HasPrecision(6, 2)
                .HasColumnName("pacote3_v2");

            entity.Property(e => e.Pacote3V2Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote3_v2_pseg");

            entity.Property(e => e.Pacote3V3)
                .HasPrecision(6, 2)
                .HasColumnName("pacote3_v3");

            entity.Property(e => e.Pacote3V3Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote3_v3_pseg");

            entity.Property(e => e.Pacote4Ativo)
                .HasColumnType("int(1)")
                .HasColumnName("pacote4_ativo");

            entity.Property(e => e.Pacote4Desc)
                .HasMaxLength(255)
                .HasColumnName("pacote4_desc");

            entity.Property(e => e.Pacote4V1)
                .HasPrecision(6, 2)
                .HasColumnName("pacote4_v1");

            entity.Property(e => e.Pacote4V1Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote4_v1_pseg");

            entity.Property(e => e.Pacote4V2)
                .HasPrecision(6, 2)
                .HasColumnName("pacote4_v2");

            entity.Property(e => e.Pacote4V2Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote4_v2_pseg");

            entity.Property(e => e.Pacote4V3)
                .HasPrecision(6, 2)
                .HasColumnName("pacote4_v3");

            entity.Property(e => e.Pacote4V3Pseg)
                .HasMaxLength(40)
                .HasColumnName("pacote4_v3_pseg");

            entity.Property(e => e.TxtEmailBaixa)
                .HasColumnType("text")
                .HasColumnName("txt_email_baixa");

            entity.Property(e => e.TxtEmailCadastro)
                .HasColumnType("text")
                .HasColumnName("txt_email_cadastro");

            entity.Property(e => e.Valor1)
                .HasPrecision(6, 2)
                .HasColumnName("valor1");

            entity.Property(e => e.Valor2)
                .HasPrecision(6, 2)
                .HasColumnName("valor2");

            entity.Property(e => e.ValorNormal)
                .HasPrecision(6, 2)
                .HasColumnName("valor_normal");
        });

        modelBuilder.Entity<Inscricao>(entity =>
        {
            entity.ToTable("inscricao");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.AceiteRegulamento).HasColumnName("aceite_regulamento");

            entity.Property(e => e.Cancelado).HasColumnName("cancelado");

            entity.Property(e => e.CpfAtleta)
                .HasMaxLength(11)
                .HasColumnName("cpf_atleta");

            entity.Property(e => e.DataEfetivacao).HasColumnName("data_efetivacao");

            entity.Property(e => e.DataInscricao).HasColumnName("data_inscricao");

            entity.Property(e => e.Dupla)
                .HasMaxLength(60)
                .HasColumnName("dupla");

            entity.Property(e => e.Equipe)
                .HasMaxLength(150)
                .HasColumnName("equipe");

            entity.Property(e => e.GnBarcode)
                .HasMaxLength(255)
                .HasColumnName("gn_barcode");

            entity.Property(e => e.GnChargeId)
                .HasColumnType("int(11)")
                .HasColumnName("gn_charge_id");

            entity.Property(e => e.GnExpireAt).HasColumnName("gn_expire_at");

            entity.Property(e => e.GnLink)
                .HasColumnType("text")
                .HasColumnName("gn_link");

            entity.Property(e => e.GnStatus)
                .HasMaxLength(50)
                .HasColumnName("gn_status");

            entity.Property(e => e.GnTotal)
                .HasColumnType("int(11)")
                .HasColumnName("gn_total");

            entity.Property(e => e.IdEvento)
                .HasColumnType("int(11)")
                .HasColumnName("id_evento");

            entity.Property(e => e.IdSubcategoria)
                .HasColumnType("int(11)")
                .HasColumnName("id_subcategoria");

            entity.Property(e => e.Numeral)
                .HasMaxLength(10)
                .HasColumnName("numeral");

            entity.Property(e => e.Pacote)
                .HasColumnType("int(1)")
                .HasColumnName("pacote");

            entity.Property(e => e.Pago).HasColumnName("pago");

            entity.Property(e => e.Quarteto)
                .HasMaxLength(60)
                .HasColumnName("quarteto");

            entity.Property(e => e.ValorPago)
                .HasPrecision(5, 2)
                .HasColumnName("valor_pago");
        });

        modelBuilder.Entity<Regulamento>(entity =>
        {
            entity.HasKey(e => e.IdEvento)
                .HasName("PRIMARY");

            entity.ToTable("regulamento");

            entity.Property(e => e.IdEvento)
                .HasColumnType("int(11)")
                .ValueGeneratedNever()
                .HasColumnName("id_evento");

            entity.Property(e => e.Compromisso)
                .HasColumnType("text")
                .HasColumnName("compromisso");

            entity.Property(e => e.Regulamento1)
                .HasColumnType("text")
                .HasColumnName("regulamento");
        });

        modelBuilder.Entity<Subcategoria>(entity =>
        {
            entity.ToTable("subcategoria");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.Ativo).HasColumnName("ativo");

            entity.Property(e => e.Aviso)
                .HasColumnType("text")
                .HasColumnName("aviso");

            entity.Property(e => e.Categoria)
                .HasColumnType("int(11)")
                .HasColumnName("categoria");

            entity.Property(e => e.FiltroDupla).HasColumnName("filtro_dupla");

            entity.Property(e => e.FiltroSexo).HasColumnName("filtro_sexo");

            entity.Property(e => e.IdEvento)
                .HasColumnType("int(11)")
                .HasColumnName("id_evento");

            entity.Property(e => e.IdadeAte)
                .HasColumnType("tinyint(2)")
                .HasColumnName("idade_ate");

            entity.Property(e => e.IdadeDe)
                .HasColumnType("tinyint(2)")
                .HasColumnName("idade_de");

            entity.Property(e => e.DescSubcategoria)
                .HasMaxLength(50)
                .HasColumnName("subcategoria");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
