using MySqlConnector;
using RH.DB;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RH.DAL
{
    public class EstadoCivilDAL
    {
        private Conexao conexao = null;

        public List<EstadoCivil> GetEstadosCivies()
        {
            string sql = "SELECT id, descricaoEstadoCivil EstadoCivil FROM estadocivil";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosCargo(retornoDr);
            }
        }


        public List<EstadoCivil> TransformaReaderEmListaDeObjetosCargo(MySqlDataReader reader)
        {


            var lista = new List<EstadoCivil>();
            while (reader.Read())
            {
                var tmpObjeto = new EstadoCivil()
                {
                    idestadocivil = int.Parse(reader["id"].ToString()),
                    estadocivil = reader["EstadoCivil"].ToString(),

                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }
    }
}
