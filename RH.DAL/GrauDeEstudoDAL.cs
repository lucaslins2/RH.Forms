using MySqlConnector;
using RH.DB;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RH.DAL
{
    public  class GrauDeEstudoDAL
    {

        private Conexao conexao = null;

        public List<GraudeEstudo> GetGrauDeEstudos()
        {

            string sql = "SELECT id, descricaoGrauDeEstudo FROM graudeestudo";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosGrauDeEstudo(retornoDr);
            }

        }


        public List<GraudeEstudo> TransformaReaderEmListaDeObjetosGrauDeEstudo(MySqlDataReader reader)
        {


            var lista = new List<GraudeEstudo>();
            while (reader.Read())
            {
                var tmpObjeto = new GraudeEstudo()
                {
                    idgraudeestudo = int.Parse(reader["id"].ToString()),
                    graudeestudo = reader["descricaoGrauDeEstudo"].ToString(),

                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }

    }

}
