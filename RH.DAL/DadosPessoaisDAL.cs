using MySqlConnector;
using RH.DB;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static RH.DAL.FuncoesDAL;
namespace RH.DAL
{
    public class DadosPessoaisForm1DAL
    {
        private Conexao conexao;

        public DadosPessoaisForm1 GetDadosPessoaisForm1(int idUsuario)
        {

            string sql = "SELECT dp.IdUsuario,dp.idCargo, dp.fotoperfil,dp.telefoneFixo,dp.celular,dp.nomePai,dp.nomeMae, dp.idEstadoCivil, dp.nomeEsposa,dp.dataNascimento,dp.numeroDependentes," +
            "dp.naturalidade,dp.nacionalidade, U.cpf, U.nome,dp.idGrauDeEstudo,dp.rg,dp.dataExpedicao,dp.orgaoExpedidor,dp.certidaoDeReservista, " +
            "dp.carteiraDeTrabalho,dp.carteiraDeTrabalhoSerie, dp.titulo, dp.zona, dp.secao,dp.cnh,dp.idUF,dp.categoriasCNH,dp.validadeCNH,dp.validadePrimeiraCNH," +
            " E.rua, E.numero, E.bairro,E.cep, E.cidade, E.estado FROM dadospessoais dp" +
            " INNER JOIN usuario U ON U.id = dp.IdUsuario " +
            " INNER JOIN endereco E ON E.IdUsuario = dp.IdUsuario ";
            sql += " WHERE dp.IdUsuario = " + idUsuario+";";

            //sql += "SELECT cpf,nome usuario WHERE id = " + idUsuario+"; ";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosDadosPessoaisForm1(retornoDr).FirstOrDefault();
            }

        }

        public Endereco GetEnderecoIdUsuario(int idUsuario)
        {
            string sql = "SELECT * FROM endereco WHERE idUsuario = "+ idUsuario;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosEndereco(retornoDr).FirstOrDefault();
            }


        } 
        public int CadastrarEndereco (Endereco endereco2, int idUsuario, Conexao conexao){

            string sql = "";

            //se operacao for igual 2 atualizar
            var enderecos = GetEnderecoIdUsuario(idUsuario);
            if (enderecos == null)
            {

                sql = "INSERT INTO  endereco (IdUsuario ,rua ,numero ,bairro ,cep ,cidade ,estado) VALUES (@IdUsuario ,@rua ,@numero ,@bairro ,@cep ,@cidade ,@estado);";

            }
            else {

                sql = "UPDATE endereco SET  rua  = @rua , numero  = @numero , bairro  = @bairro , cep  = @cep , cidade  = @cidade , estado = @estado WHERE IdUsuario  = @IdUsuario ";
            }
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@rua", endereco2.rua);
            cmd.Parameters.AddWithValue("@numero", endereco2.numero);
            cmd.Parameters.AddWithValue("@bairro", endereco2.bairro);
            cmd.Parameters.AddWithValue("@cep", endereco2.cep);
            cmd.Parameters.AddWithValue("@cidade", endereco2.cidade);
            cmd.Parameters.AddWithValue("@estado", endereco2.estado);
          

                int reposta = conexao.ExecutaComando(cmd);
                return reposta;
        }

        public int CadastrarDadosPessoaisForm1(DadosPessoaisForm1 dadosPessoaisForm1,int idUsuario,  int Operacao) {




            string sql = "INSERT INTO dadospessoais(idUsuario,idCargo, fotoperfil,telefoneFixo,celular,nomePai, nomeMae, idEstadoCivil,nomeEsposa,dataNascimento," +
                " numeroDependentes, naturalidade, nacionalidade,idGrauDeEstudo,rg, dataExpedicao,orgaoExpedidor,certidaoDeReservista,carteiraDeTrabalho,carteiraDeTrabalhoSerie, titulo, zona, secao," +
                " cnh,idUF,categoriasCNH,validadeCNH,validadePrimeiraCNH) " +
                "Values (@idUsuario, @idCargo,@fotoperfil,@telefoneFixo,@celular, @nomePai, @nomeMae,@idEstadoCivil, @nomeEsposa,@dataNascimento, @numeroDependentes, @naturalidade,@nacionalidade," +
                " @idGrauDeEstudo,@rg,@dataExpedicao,@orgaoExpedidor,@certidaoDeReservista,@carteiraDeTrabalho,@carteiraDeTrabalhoSerie, @titulo, @zona, @secao,@cnh,@idUF,@categoriasCNH,@validadeCNH,@validadePrimeiraCNH);";
            //se operacao for igual 2 atualizar
            if (Operacao == 2) {

                sql = " Update dadospessoais Set idCargo = @idCargo, fotoperfil = @fotoperfil,telefoneFixo=@telefoneFixo,celular =@celular, nomePai = @nomePai, nomeMae = @nomeMae, ";
                sql +=  " idEstadoCivil = @idEstadoCivil, nomeEsposa = @nomeEsposa, dataNascimento = @dataNascimento, numeroDependentes = @numeroDependentes, naturalidade "+
                        "  = @naturalidade, nacionalidade = @nacionalidade, idGrauDeEstudo =  @idGrauDeEstudo, rg = @rg, dataExpedicao =@dataExpedicao," +
                        " orgaoExpedidor = @orgaoExpedidor, certidaoDeReservista = @certidaoDeReservista,carteiraDeTrabalho=@carteiraDeTrabalho,carteiraDeTrabalhoSerie=@carteiraDeTrabalhoSerie, titulo=@titulo, zona=@zona, secao=@secao," +
                        " cnh=@cnh,idUF=@idUF,categoriasCNH=@categoriasCNH,validadeCNH=@validadeCNH,validadePrimeiraCNH=@validadePrimeiraCNH  "+
                        " WHERE idUsuario = @idUsuario";



            }


            string sql2 = "UPDATE usuario SET nome = '"+ dadosPessoaisForm1.nomecompleto+ "' WHERE id = " + idUsuario;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@idCargo", dadosPessoaisForm1.idCargo);
            cmd.Parameters.AddWithValue("@fotoperfil", dadosPessoaisForm1.fotoperfil);
            cmd.Parameters.AddWithValue("@telefoneFixo", dadosPessoaisForm1.telefonefixo);
            cmd.Parameters.AddWithValue("@celular", dadosPessoaisForm1.celular);
            cmd.Parameters.AddWithValue("@nomePai", dadosPessoaisForm1.nomepai);
            cmd.Parameters.AddWithValue("@nomeMae", dadosPessoaisForm1.nomemae);
            cmd.Parameters.AddWithValue("@idEstadoCivil", dadosPessoaisForm1.idestadocivil);
            cmd.Parameters.AddWithValue("@nomeEsposa", dadosPessoaisForm1.nomeesposa);
            cmd.Parameters.AddWithValue("@dataNascimento", dadosPessoaisForm1.dtanascimento);
            cmd.Parameters.AddWithValue("@numeroDependentes", dadosPessoaisForm1.nrodepedentes);
            cmd.Parameters.AddWithValue("@naturalidade", dadosPessoaisForm1.naturalidade);
            cmd.Parameters.AddWithValue("@nacionalidade", dadosPessoaisForm1.nacionalidade);
            cmd.Parameters.AddWithValue("@idGrauDeEstudo", dadosPessoaisForm1.idgraudeestudo);
            cmd.Parameters.AddWithValue("@rg", dadosPessoaisForm1.rg);
            cmd.Parameters.AddWithValue("@dataExpedicao", dadosPessoaisForm1.dataexpedicao);
            cmd.Parameters.AddWithValue("@orgaoExpedidor", dadosPessoaisForm1.orgaoexpedidor);
            cmd.Parameters.AddWithValue("@certidaoDeReservista", dadosPessoaisForm1.certidaodereservista);
            cmd.Parameters.AddWithValue("@carteiraDeTrabalho", dadosPessoaisForm1.carteiradetrabalho);
            cmd.Parameters.AddWithValue("@carteiraDeTrabalhoSerie", dadosPessoaisForm1.carteiradetrabalhoserie);
            cmd.Parameters.AddWithValue("@titulo", dadosPessoaisForm1.titulo);
            cmd.Parameters.AddWithValue("@zona", dadosPessoaisForm1.zona);
            cmd.Parameters.AddWithValue("@secao", dadosPessoaisForm1.secao);
            cmd.Parameters.AddWithValue("@cnh", dadosPessoaisForm1.cnh);
            cmd.Parameters.AddWithValue("@idUF", dadosPessoaisForm1.idUFcnh);
            cmd.Parameters.AddWithValue("@categoriasCNH", dadosPessoaisForm1.categoriaString); //string.Join(",",dadosPessoaisForm1.categoriasCNH));
            cmd.Parameters.AddWithValue("@validadeCNH", dadosPessoaisForm1.validadecnh);
            cmd.Parameters.AddWithValue("@validadePrimeiraCNH", dadosPessoaisForm1.validadeprimeiracnh);
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;

            using (conexao = new Conexao(null))
            {
               
                int reposta = conexao.ExecutaComando(cmd);
                int reposta2 = conexao.ExecutaComando(cmd2);
                int reposta3 = CadastrarEndereco(dadosPessoaisForm1.endereco, idUsuario, conexao);
                return reposta+ reposta2+reposta3;
            }


        }

        public List<DadosPessoaisForm1> TransformaReaderEmListaDeObjetosDadosPessoaisForm1(MySqlDataReader reader)
        {


            var lista = new List<DadosPessoaisForm1>();
            while (reader.Read())
            {
                var tmpObjeto = new DadosPessoaisForm1()
                {
                    idUsuario = (int)StringParaInt(reader["IdUsuario"].ToString()),
                    idCargo = StringParaInt(reader["idCargo"].ToString()),
                    fotoperfil = reader["fotoperfil"].ToString() != "" ? (byte[])reader["fotoperfil"] : null,
                    telefonefixo = reader["telefoneFixo"].ToString(),
                    celular = reader["celular"].ToString(),
                    nomepai = reader["nomePai"].ToString(),
                    nomemae = reader["nomeMae"].ToString(),
                    idestadocivil = StringParaInt(reader["idEstadoCivil"].ToString()),
                    nomeesposa = reader["nomeEsposa"].ToString(),
                    dtanascimento = StringParaDate(reader["dataNascimento"].ToString()),
                    nrodepedentes = StringParaInt(reader["numeroDependentes"].ToString()),
                    naturalidade = reader["naturalidade"].ToString(),
                    nacionalidade = reader["nacionalidade"].ToString(),
                    cpf = reader["cpf"].ToString(),
                    nomecompleto = reader["nome"].ToString(),
                    idgraudeestudo = StringParaInt(reader["idGrauDeEstudo"].ToString()),
                    rg = reader["rg"].ToString(),
                    dataexpedicao = StringParaDate(reader["dataExpedicao"].ToString()),
                    orgaoexpedidor = reader["orgaoExpedidor"].ToString(),
                    certidaodereservista = reader["certidaoDeReservista"].ToString(),
                    carteiradetrabalho = reader["carteiraDeTrabalho"].ToString(),
                    carteiradetrabalhoserie = reader["carteiraDeTrabalhoSerie"].ToString(),
                    titulo = reader["titulo"].ToString(),
                    zona = reader["zona"].ToString(),
                    secao = reader["secao"].ToString(),
                    cnh = reader["cnh"].ToString(),
                    idUFcnh = StringParaInt(reader["idUF"].ToString()),
                    categoriasCNH = !String.IsNullOrEmpty(reader["categoriasCNH"].ToString()) ? reader["categoriasCNH"].ToString().Split(',').ToList() : null,
                    validadecnh = StringParaDate(reader["validadeCNH"].ToString()),
                    validadeprimeiracnh = StringParaDate(reader["validadePrimeiraCNH"].ToString()),
                    endereco = new Endereco { 
                      rua = reader["rua"].ToString(),
                      numero = reader["numero"].ToString(),
                      bairro = reader["bairro"].ToString(),
                      cep = reader["cep"].ToString(),
                      cidade = reader["cidade"].ToString(),
                      estado = reader["estado"].ToString(),
                    }
                    //FLATA id uf


                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }


        public List<Endereco> TransformaReaderEmListaDeObjetosEndereco(MySqlDataReader reader)
        {


            var lista = new List<Endereco>();
            while (reader.Read())
            {
                var tmpObjeto = new Endereco()
                {
                    idEndereco = StringParaInt(reader["id"].ToString()),
                    idUsuario =  StringParaInt(reader["IdUsuario"].ToString()),
                    rua =       reader["rua"].ToString(),
                    numero =    reader["numero"].ToString(),
                    bairro = reader["bairro"].ToString(),
                    cep = reader["cep"].ToString(),
                    cidade = reader["cidade"].ToString(),
                    estado = reader["estado"].ToString()
                    //FLATA id uf


                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }

        public int? TransformaReaderEmListaDeObjetosIdUsuario(MySqlDataReader reader)
        {
            int? idUsuario;

            while (reader.Read())
            {
                idUsuario = StringParaInt(reader["IdUsuario"].ToString());
                return idUsuario;
            };

            return 0;

        }



        public int? VerificarDadosPessoais(int idUsuario) {

            string sql = "SELECT idUsuario FROM dadospessoais WHERE idUsuario = " + idUsuario;


            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosIdUsuario(retornoDr);
            }


        }
    }
}
