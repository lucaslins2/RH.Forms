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
    public class ExperienciaAnteriorDAL
    {
        private Conexao conexao;
        public int CadastrarExperienciasAnterior(List<ExperienciaAnterior> experienciaAnteriores, int idUsuario)
        {
            int rows = 0;
           

            foreach (var exp in experienciaAnteriores)
            {
   
                string sql = " INSERT INTO  empregosantigos (idUsuario ,empresa ,telefone ,contato ,setor ,cargo ,endereco,dataAdmissao,dataDemissao, motivoDemissao) " +
                 "                 VALUES (@IdUsuario ,@empresa ,@telefone ,@contato ,@setor ,@cargo ,@endereco,@dataAdmissao,@dataDemissao,@motivoDemissao );";
   

                //if (exp.idExperienciaAnterior == null ||  exp.idExperienciaAnterior ==0)
                //{

                //    sql += " INSERT INTO  empregosantigos (idUsuario ,empresa ,telefone ,contato ,setor ,cargo ,endereco,dataAdmissao,dataDemissao, motivoDemissao) " +
                //        "                 VALUES (@IdUsuario ,@empresa ,@telefone ,@contato ,@setor ,@cargo ,@endereco,@dataAdmissao,@dataDemissao,@motivoDemissao );";

                //}
                //else
                //{

                //    sql = " UPDATE empregosantigos SET empresa  = @empresa , telefone  = @telefone , contato  = @contato , setor  = @setor ,cargo = @cargo, endereco = @endereco,dataAdmissao=@dataAdmissao,dataDemissao=@dataDemissao," +
                //        " motivoDemissao=@motivoDemissao WHERE IdUsuario  = @IdUsuario AND id= @id; ";
                //}

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@empresa", exp.empresa);
                cmd.Parameters.AddWithValue("@telefone", exp.telefoneEmpresa);
                cmd.Parameters.AddWithValue("@contato", exp.contato);
                cmd.Parameters.AddWithValue("@setor", exp.setor);
                cmd.Parameters.AddWithValue("@cargo", exp.cargoExercido);
                cmd.Parameters.AddWithValue("@endereco", exp.enderecoEmpresa);
                cmd.Parameters.AddWithValue("@dataAdmissao", exp.dataAdmissao);
                cmd.Parameters.AddWithValue("@dataDemissao", exp.dataDemissao);
                cmd.Parameters.AddWithValue("@motivoDemissao", exp.motivoSaida);
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
        public int DeletarExperienciasAnterior(int idUsuario) {
            string sql = " DELETE  FROM empregosantigos WHERE idUsuario = " + idUsuario + " AND  id>0;";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            using (conexao = new Conexao(null))
            {
              return  conexao.ExecutaComando(cmd);

                // return TransformaReaderEmListaDeObjetosExpAnterior(retornoDr).FirstOrDefault();
            }

        }
        public ExperienciaAnterior GetEnderecoIdUsuario(int idUsuario, int IdExperiencia)
        {
            string sql = "SELECT id, idUsuario, empresa, telefone, contato, setor, cargo, endereco, dataAdmissao, dataDemissao, motivoDemissao FROM empregosantigos WHERE idUsuario = " + idUsuario;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosExpAnterior(retornoDr).FirstOrDefault();
            }


        }

           public List<ExperienciaAnterior> GetExperienciasAnteriores(int idUsuario)
        {
            string sql = "SELECT id, idUsuario, empresa, telefone, contato, setor, cargo, endereco, dataAdmissao, dataDemissao, motivoDemissao FROM empregosantigos WHERE idUsuario = " + idUsuario;

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosExpAnterior(retornoDr);
            }


        }

        public List<ExperienciaAnterior> TransformaReaderEmListaDeObjetosExpAnterior(MySqlDataReader reader)
        {


            var lista = new List<ExperienciaAnterior>();
            while (reader.Read())
            {
                var tmpObjeto = new ExperienciaAnterior()
                {
                    idExperienciaAnterior = StringParaInt(reader["id"].ToString()),
                    idUsuario = StringParaInt(reader["idUsuario"].ToString()),
                    empresa = reader["empresa"].ToString(),
                    telefoneEmpresa = reader["telefone"].ToString(),
                    contato = reader["contato"].ToString(),
                    setor = reader["setor"].ToString(),
                    cargoExercido = reader["cargo"].ToString(),
                    enderecoEmpresa = reader["endereco"].ToString(),
                    dataAdmissao = StringParaDate(reader["dataAdmissao"].ToString()),
                    dataDemissao = StringParaDate(reader["dataDemissao"].ToString()),
                    motivoSaida = reader["motivoDemissao"].ToString(),

                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }
    }
}
