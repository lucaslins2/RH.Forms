using System;
using System.Data;
using MySqlConnector;
using RH.Models;
namespace RH.DB
{
    public class Conexao : IDisposable
    {
        private readonly MySqlConnection Con;

        //private static SqlConnection Con;

        //public Conexao(string servidor, string portaDB, string nomeDB, string usuarioDB, string senhaDB = "")
        public Conexao(SigoCFG sigoCFG)
        {


            //var conexaoDB = (ConexaoDB)_conexaoDB;

            if (sigoCFG == null)
            {
                sigoCFG = new SigoCFG();
                //sigoCFG.Servidor = @"127.0.0.1\MSSQLSERVER2019";
                //sigoCFG.Servidor = @"localhost\MSSQLSERVER2019";
                sigoCFG.Servidor = @"127.0.0.1";
                //sigoCFG.PortaDB = "63314";
                //sigoCFG.Servidor = @"(local)\MSSQLSERVER2019";
                sigoCFG.PortaDB = "3306";

                sigoCFG.NomeDB = "db_rh";
            }

            if (sigoCFG.UsuarioDB == null || sigoCFG.UsuarioDB == "nova2019")
            {
                sigoCFG.UsuarioDB = "Sistema";
                sigoCFG.SenhaDB = "123@Sigma";
            }


            //if (conexaoDB.Servidor == "")
            //{
            //    conexaoDB.NomeDB = "WISESYSTEM_0000001";
            //}            

            Con = new MySqlConnection("Data Source=" + sigoCFG.Servidor + "," + sigoCFG.PortaDB + "; Initial Catalog=" + sigoCFG.NomeDB + "; User ID=" + sigoCFG.UsuarioDB + "; Password=" + sigoCFG.SenhaDB);

            /*
            servidor = "127.0.0.1";
            portaDB = "63314";
            usuarioDB = "SigoW3Wise";
            senhaDB = "wS!+y@2019#.Z";

            if (nomeDB == "")           
            {                
                nomeDB = "WISESYSTEM_0000001";                
            }            

            Con = new SqlConnection("Data Source=" + servidor + "," + portaDB + "; Initial Catalog=" + nomeDB + "; User ID=" + usuarioDB + "; Password=" + senhaDB);
            */
            try
            {
                Con.Open();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// /// Método que executa os comandos de insert, update e delete. 
        /// </summary>
        /// <param name="scm"></param>
        public int ExecutaComando(MySqlCommand scm)
        {
            var resultado = 0;
            // Verifica parâmetros nulos
            foreach (MySqlParameter Parameter in scm.Parameters)
            {
                if (Parameter.Value == null)
                    Parameter.Value = DBNull.Value;
            }
            MySqlTransaction tran = Con.BeginTransaction();
            try
            {
                scm.Connection = Con;
                scm.Transaction = tran;
                resultado = scm.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message);

            }

            return resultado;
        }
        /// <summary>
        /// /// Método que executa SELECT com ExecuteScalar 
        /// </summary>
        /// <param name="scm"></param>
        public int ExecutaScalar(MySqlCommand scm)
        {
            var resultado = 0;
            // Verifica parâmetros nulos
            foreach (MySqlParameter Parameter in scm.Parameters)
            {
                if (Parameter.Value == null)
                    Parameter.Value = DBNull.Value;
            }

            try
            {
                scm.Connection = Con;
                resultado = (int)scm.ExecuteScalar();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

            return resultado;
        }
        /// <summary>
        /// Método que executa os comandos do tipo SELECT e retorna em um objeto do tipo SqlDataReader
        /// </summary>
        /// <param name="scm"></param>
        /// <returns>SqlDataReader</returns>
        public MySqlDataReader ExecutaComandoComRetornoSdr(MySqlCommand scm)
        {
            try
            {
                scm.Connection = Con;
                return scm.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método que executa os comandos do tipo SELECT e retorna em um objeto do tipo DataTable
        /// </summary>
        /// <param name="scm"></param>
        /// <returns>Objeto do tipo DataTable</returns>
        public DataTable ExecutaComandoComRetornoDtb(MySqlCommand scm)
        //public DataTable ExecutaComandoComRetornoDt(SqlCommand scm)
        {
            try
            {
                scm.Connection = Con;
                var dtb = new DataTable();
                dtb.Load(scm.ExecuteReader());
                return dtb;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string SP_GeraID(string strCampo)
        {
            var retorno = "";
            try
            {
                var scm = new MySqlCommand
                {
                    CommandText = "SP_GeraID",
                    CommandType = CommandType.StoredProcedure,
                    Connection = Con
                };
                // ID Empresa
                // scm.Parameters.Add("@ID_Empresa", SqlDbType.Int);
                //  scm.Parameters["@ID_Empresa"].Value = ID_Empresa;
                // Campo
                scm.Parameters.Add("@strCampo", MySqlDbType.VarChar);
                scm.Parameters["@strCampo"].Value = strCampo;
                // ID Gerado - Output
                scm.Parameters.Add("@ID_Gerado", MySqlDbType.Int32);
                scm.Parameters["@ID_Gerado"].Direction = ParameterDirection.Output;
                scm.ExecuteNonQuery();
                retorno = scm.Parameters["@ID_Gerado"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retorno;

        }

        public DataTable ExecutaSPComRetornoDtb(MySqlCommand scm)
        {

            try
            {
                scm.Connection = Con;
                scm.CommandType = CommandType.StoredProcedure;

                var dtb = new DataTable();
                dtb.Load(scm.ExecuteReader());

                return dtb;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public void Dispose()
        {
            if (Con == null) return;
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Dispose();

        }
        public static MySqlCommand CriaCommand(string sql)
        {
            MySqlCommand scm = new MySqlCommand();
            scm.CommandType = CommandType.Text;
            scm.CommandText = sql;
            return scm;

        }

    }
}
