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
          
            string sql = "SELECT IdUsuario,idCargo, fotoperfil,nomePai,nomeMae, idEstadoCivil, nomeEsposa,dataNascimento,numeroDependentes,naturalidade,nacionalidade FROM dadospessoais";
            sql += " WHERE IdUsuario = " + idUsuario;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosDadosPessoaisForm1(retornoDr).FirstOrDefault();
            }

        }
        public int CadastrarDadosPessoaisForm1(DadosPessoaisForm1 dadosPessoaisForm1,int idUsuario,  int Operacao) {




            string sql = "INSERT INTO dadospessoais(idUsuario,idCargo, fotoperfil,nomePai, nomeMae, idEstadoCivil,nomeEsposa,dataNascimento, numeroDependentes, naturalidade, nacionalidade) " +
                "Values (@idUsuario, @idCargo,@fotoperfil, @nomePai, @nomeMae,@idEstadoCivil, @nomeEsposa,@dataNascimento, @numeroDependentes, @naturalidade,@nacionalidade);";
            //se operacao for igual 2 atualizar
            if (Operacao == 2) {

                sql =   " Update dadospessoais Set idCargo = @idCargo, fotoperfil = @fotoperfil, nomePai = @nomePai, nomeMae = @nomeMae, ";
                sql +=  " idEstadoCivil = @idEstadoCivil, nomeEsposa = @nomeEsposa, dataNascimento = @dataNascimento, numeroDependentes = @numeroDependentes, naturalidade "+
                        "  = @naturalidade, nacionalidade = @nacionalidade WHERE idUsuario = @idUsuario";



            }


            string sql2 = "UPDATE usuario SET nome = '"+ dadosPessoaisForm1.nomecompleto+ "' WHERE id = " + idUsuario;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@idCargo", dadosPessoaisForm1.idCargo);
            cmd.Parameters.AddWithValue("@fotoperfil", dadosPessoaisForm1.fotoperfil);
            cmd.Parameters.AddWithValue("@nomePai", dadosPessoaisForm1.nomepai);
            cmd.Parameters.AddWithValue("@nomeMae", dadosPessoaisForm1.nomemae);
            cmd.Parameters.AddWithValue("@idEstadoCivil", dadosPessoaisForm1.idestadocivil);
            cmd.Parameters.AddWithValue("@nomeEsposa", dadosPessoaisForm1.nomeesposa);
            cmd.Parameters.AddWithValue("@dataNascimento", dadosPessoaisForm1.dtanascimento);
            cmd.Parameters.AddWithValue("@numeroDependentes", dadosPessoaisForm1.nrodepedentes);
            cmd.Parameters.AddWithValue("@naturalidade", dadosPessoaisForm1.naturalidade);
            cmd.Parameters.AddWithValue("@nacionalidade", dadosPessoaisForm1.nacionalidade);


            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = sql2;

            using (conexao = new Conexao(null))
            {
               
                int reposta = conexao.ExecutaComando(cmd);
                int reposta2 = conexao.ExecutaComando(cmd2);
                return reposta;
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
                    idCargo = (int)StringParaInt(reader["idCargo"].ToString()),
                    fotoperfil = reader["fotoperfil"].ToString() != "" ? (byte[])reader["fotoperfil"] : null,
                    nomepai = reader["nomePai"].ToString(),
                    nomemae = reader["nomeMae"].ToString(),
                    idestadocivil = (int)StringParaInt(reader["idEstadoCivil"].ToString()),
                    nomeesposa = reader["nomeEsposa"].ToString(),
                    dtanascimento = DateTime.Parse(reader["dataNascimento"].ToString()),
                    nrodepedentes = (int)StringParaInt(reader["numeroDependentes"].ToString()),
                    naturalidade = reader["naturalidade"].ToString(),
                    nacionalidade = reader["nacionalidade"].ToString(),
                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }




    }
}
