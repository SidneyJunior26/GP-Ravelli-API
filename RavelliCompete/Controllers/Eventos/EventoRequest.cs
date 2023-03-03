namespace RavelliCompete.Controllers.Eventos;

public record EventoRequest(string Nome, string Descricao, string Local, DateTime Data, DateTime DataIniInscricao,
DateTime DataFimInscricao, DateTime DataDesconto, DateTime DataValorNormal, decimal Valor1, decimal Valor2, decimal ValorNormal,
decimal Pacote2V1, decimal Pacote2V2, decimal Pacote2V3, decimal Pacote3V1, decimal Pacote3V2, decimal Pacote3V3, decimal Pacote4V1,
decimal Pacote4V2, decimal Pacote4V3, string Pacote1Desc, string Pacote2Desc, string Pacote3Desc, string Pacote4Desc,
int Pacote1Ativo, int Pacote2Ativo, int Pacote3Ativo, int Pacote4Ativo, string Categoria, string BoletoInf1,
string BoletoInf2, string BoletoInf3, string BoletoInstrucao1, string BoletoInstrucao2, string BoletoInstrucao3,
string ObsTela, string TxtEmailCadastro, string TxtEmailBaixa, int AtivaInscricao, int AtivaEvento, int EventoTipo,
string Pacote1V1Pseg, string Pacote1V2Pseg, string Pacote1V3Pseg, string Pacote2V1Pseg, string Pacote2V2Pseg,
string Pacote2V3Pseg, string Pacote3V1Pseg, string Pacote3V2Pseg, string Pacote3V3Pseg, string Pacote4V1Pseg,
string Pacote4V2Pseg, string Pacote4V3Pseg);
