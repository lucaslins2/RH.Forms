using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySqlConnector;
using RH.DB;
using RH.Models;
namespace RH.DAL
{
   public  class CanditadoDAL
    {
     

        private Conexao conexao = null;
        public List<Canditado> GetCanditados()
        {

            string sql = "  SELECT DISTINCT  u.id idcanditado, u.nome,u.email ,u.cpf," +
                         "   CASE dp.celular  " +
                         "    WHEN  '' then dp.telefoneFixo " +
                         "    WHEN  null then dp.telefoneFixo" +
                         "   END as telefone " +
                         "  FROM usuario u " +
                         "  INNER JOIN dadospessoais dp ON dp.idUsuario = u.id " +
                         "  INNER JOIN formularios fr ON fr.idUsuario = u.id";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosCanditado(retornoDr);
            }

        }

        public List<Canditado> TransformaReaderEmListaDeObjetosCanditado(MySqlDataReader reader)
        {


            var lista = new List<Canditado>();
            while (reader.Read())
            {

         
                var tmpObjeto = new Canditado()
                {
                    idcantidado = int.Parse(reader["idcanditado"].ToString()),
                    nome = reader["nome"].ToString(),
                    email = reader["email"].ToString(),
                    cpf = reader["cpf"].ToString().Replace(".","").Replace("-",""),
                    telefone = reader["telefone"].ToString(),
                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }


    }
}
