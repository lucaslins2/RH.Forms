using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySqlConnector;
using RH.DB;
using RH.Models;
namespace RH.DAL
{
    public class CanditadoDAL
    {


        private Conexao conexao = null;
        public List<Canditado> GetCanditados()
        {

            string sql = "  SELECT u.id idcanditado, u.nome,u.email ,u.cpf," +
                         "   IFNULL(IF(dp.celular = " +
                         "    '',dp.telefoneFixo,dp.celular),dp.telefoneFixo) AS telefone " +
                         "    FROM usuario u " +
                         "  INNER JOIN dadospessoais dp ON dp.idUsuario = u.id" +
                         "  INNER JOIN formularios fr ON fr.idUsuario = u.id" +
                         "  group by " +
                         "  u.id"+
                         " ,u.nome"+
                         " ,u.email " +
                         " ,u.cpf" +
                         " ,dp.celular" +
                         ",dp.telefoneFixo" +
                         " ORDER BY" +
                         " (SELECT SUM(repostaAlternativa) FROM perguntausuario " +
                         " WHERE idUsuario = u.id);";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosCanditado(retornoDr);
            }

        }

        public List<Canditado> GetCanditadosPesquisa(Filtros filtros)
        {

            //string sql = "  SELECT DISTINCT  u.id idcanditado, u.nome,u.email ,u.cpf," +
            //             "   CASE dp.celular  " +
            //             "    WHEN  '' then dp.telefoneFixo " +
            //             "    WHEN  null then dp.telefoneFixo" +
            //             "    ELSE dp.celular " +
            //             "   END as telefone " +
            //             "  FROM usuario u " +
            //             "  INNER JOIN dadospessoais dp ON dp.idUsuario = u.id " +
            //             "  INNER JOIN formularios fr ON fr.idUsuario = u.id";
            string sql = " SELECT u.id idcanditado, u.nome,u.email ,u.cpf," +
                         " IFNULL(IF(dp.celular = '', dp.telefoneFixo, dp.celular), dp.telefoneFixo) AS telefone" +
                         " FROM usuario u" +
                         " INNER JOIN dadospessoais dp ON dp.idUsuario = u.id" +
                         " INNER JOIN formularios fr ON fr.idUsuario = u.id" +
                         " LEFT JOIN endereco e ON e.idUsuario = u.id";
            string GroupBy = " group by u.id,u.nome,u.email,u.cpf,dp.celular,dp.telefoneFixo ";
            string OrderBy = " ORDER BY" + " (SELECT SUM(repostaAlternativa) FROM perguntausuario WHERE idUsuario = u.id);";
            string Where = "";
            if (!String.IsNullOrEmpty(filtros.pesquisar))
            {
                if (!String.IsNullOrEmpty(Where))
                {
                    Where += "  AND u.nome LIKE  CONCAT('%', @pesquisar, '%') OR u.cpf LIKE CONCAT('%', @pesquisar, '%') OR u.email LIKE  CONCAT('%', @pesquisar, '%') " +
                             "  OR dp.celular LIKE  CONCAT('%', @pesquisar, '%') OR dp.telefoneFixo LIKE  CONCAT('%', @pesquisar, '%') ";

                }
                else
                {
                    Where += "WHERE u.nome LIKE  CONCAT('%', @pesquisar, '%') OR u.cpf LIKE CONCAT('%', @pesquisar, '%') OR u.email LIKE  CONCAT('%', @pesquisar, '%') " +
                         "  OR dp.celular LIKE  CONCAT('%', @pesquisar, '%') OR dp.telefoneFixo LIKE  CONCAT('%', @pesquisar, '%') ";

                }

            }

            if (filtros.idcargo > 0)
            {
                if (!String.IsNullOrEmpty(Where))
                {
                    Where += " AND fr.idCargo = " +filtros.idcargo;

                }
                else {
                    Where += " WHERE fr.idCargo = " + filtros.idcargo;

                }

            }
            if (filtros.maior18)
            {
                if (!String.IsNullOrEmpty(Where))
                {
                    var dataHj = DateTime.Now;
                    dataHj = dataHj.AddYears(-18);
                    Where += " AND dp.dataNascimento <= '" + dataHj.ToString("yyyy-MM-dd")+"'";

                }
                else {
                    var dataHj = DateTime.Now;
                    dataHj = dataHj.AddYears(-18);
                     Where += " WHERE dp.dataNascimento <= '" + dataHj.ToString("yyyy-MM-dd")+"'";

                }
            }
            if (!String.IsNullOrEmpty(filtros.cidade)) {


                if (!String.IsNullOrEmpty(Where))
                {
                   // Where += " AND e.cidade = "+ filtros.cidade;
                    Where += "  AND e.cidade LIKE  CONCAT('%', @cidade, '%')";
                }
                else
                {
                    Where += " WHERE e.cidade LIKE  CONCAT('%', @cidade, '%')";
                    //Where += "  e.cidade = " + filtros.cidade;

                }



            }
            if (filtros.status != 3)
            {
                if (!String.IsNullOrEmpty(Where))
                {
                    Where += " AND fr.Status = " + filtros.status;

                }
                else
                {
                    Where += " WHERE fr.Status = " + filtros.status;

                }

            }

            sql +=" " +Where + " " + GroupBy + " " + OrderBy;
           
         

            MySqlCommand cmd = new MySqlCommand();
            if(!String.IsNullOrEmpty(filtros.pesquisar))
            cmd.Parameters.AddWithValue("@pesquisar", filtros.pesquisar) ;
            if (!String.IsNullOrEmpty(filtros.cidade))
                cmd.Parameters.AddWithValue("@cidade", filtros.cidade);
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
                    cpf = reader["cpf"].ToString().Replace(".", "").Replace("-", ""),
                    telefone = reader["telefone"].ToString(),
                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }

        public List<Cidades> GetCidades()
        {

            string sql = " SELECT cidade FROM endereco e " +
                         " INNER JOIN formularios fr ON fr.idUsuario = e.idUsuario GROUP BY cidade";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                List<Cidades> cidades = new List<Cidades>();
                int cotador = 0;
                while (retornoDr.Read())
                {
                    cotador++;
                    var tmpObjeto = new Cidades()
                    {
                        idcidade = cotador,
                        cidade = retornoDr["cidade"].ToString(),

                    };
                    cidades.Add(tmpObjeto);

                }
                return cidades;

            }




        }

    }
}
