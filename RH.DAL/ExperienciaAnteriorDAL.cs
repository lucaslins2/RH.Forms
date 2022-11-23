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

            return 0;
        }

        public ExperienciaAnterior GetEnderecoIdUsuario(int idUsuario)
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
