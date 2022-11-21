using MySqlConnector;
using RH.DB;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RH.DAL
{
    public  class EstadoDAL
    {

        private Conexao conexao = null;

        public List<Estado> GetEstados()
        {
            string sql = "SELECT id, uf  FROM uf";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosEstado(retornoDr);
            }
        }


        public List<Estado> TransformaReaderEmListaDeObjetosEstado(MySqlDataReader reader)
        {


            var lista = new List<Estado>();
            while (reader.Read())
            {
                var tmpObjeto = new Estado()
                {
                    idUF = int.Parse(reader["id"].ToString()),
                    estado = reader["uf"].ToString(),

                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }
    }
}
