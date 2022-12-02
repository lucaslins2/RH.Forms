using MySqlConnector;
using RH.DB;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using static RH.DAL.FuncoesDAL;
namespace RH.DAL
{
    public class PerguntaDAL
    {
        Conexao conexao = null;
        public List<Pergunta> GetPerguntas(int idCargo)
        {

            string sql = " SELECT p.idPergunta,p.idCargo,p.descricao,p.peso, pu.id idReposta, pu.repostaAlternativa reposta FROM perguntas p " +
                         " LEFT JOIN perguntausuario pu ON pu.idPergunta = p.idPergunta " +
                         " WHERE p.idCargo = " + idCargo;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosPerguntas(retornoDr);
            }

        }


        public List<Pergunta> TransformaReaderEmListaDeObjetosPerguntas(MySqlDataReader reader)
        {


            var lista = new List<Pergunta>();
            while (reader.Read())
            {
                var tmpObjeto = new Pergunta()
                {
                    idpergunta = int.Parse(reader["idPergunta"].ToString()),
                    idcargo = int.Parse(reader["idCargo"].ToString()),
                    descricao = reader["descricao"].ToString(),
                    peso = int.Parse(reader["peso"].ToString()),
                    idReposta = !String.IsNullOrEmpty(reader["idReposta"].ToString()) ? int.Parse(reader["idReposta"].ToString()): 0,
                    reposta = !String.IsNullOrEmpty(reader["reposta"].ToString()) ? int.Parse(reader["reposta"].ToString()) : 0,
                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }

        public int SalvarReposta(List<Pergunta> lista,int idUsuario) {

         
            int rows = 0;
           foreach(var pergunta in lista)
            {
                string sql = "";

                if (pergunta.idReposta > 0)
                {

                    sql = "UPDATE perguntausuario  SET idPergunta = @idPergunta, idUsuario = @idUsuario, repostaAlternativa = @repostaAlternativa WHERE id = @idReposta";

                }
                else
                {

                    sql = "INSERT INTO perguntausuario  (idPergunta,idUsuario,repostaAlternativa) VALUES (@idPergunta,@idUsuario,@repostaAlternativa)";
                }


                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@idPergunta", pergunta.idpergunta);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@repostaAlternativa", pergunta.reposta);
                if (pergunta.idReposta > 0)
                 cmd.Parameters.AddWithValue("@idReposta", pergunta.idReposta);

                //if (exp.idExperienciaAnterior > 0)
                //    cmd.Parameters.AddWithValue("@id", exp.idExperienciaAnterior);

                using (conexao = new Conexao(null))
                {

                    rows += conexao.ExecutaComando(cmd);
                    // return TransformaReaderEmListaDeObjetosExpAnterior(retornoDr).FirstOrDefault();
                }

            }
       
            return rows;

        }
        public int SalvarVaga(int idUsuario, int IdCargo) {

            string sql = " INSERT INTO formularios (idUsuario, idCargo, Status) VALUES( @idUsuario,@idCargo, @Status) ";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@idCargo", IdCargo);
            cmd.Parameters.AddWithValue("@Status", 0);

            using (conexao = new Conexao(null))
            {

                return conexao.ExecutaComando(cmd);
            }
        }


        public int AtualizarCanditados(int idvaga, int status)
        {

            //string sql = " INSERT INTO formularios (idUsuario, idCargo, Status) VALUES( @idUsuario,@idCargo, @Status) ";
            string sql = "UPDATE formularios SET Status = @Status WHERE id = @id";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@id", idvaga);
           // cmd.Parameters.AddWithValue("@idCargo", IdCargo);
            cmd.Parameters.AddWithValue("@Status", status);

            using (conexao = new Conexao(null))
            {

                return conexao.ExecutaComando(cmd);
            }
        }



    }
}
