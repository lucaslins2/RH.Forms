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
    public class CargoDAL
    {
       private Conexao conexao = null;
        public List<Cargo> GetCargos(int Usuario)
        {
            string sql = "";
            if (Usuario > 0)
            {
                sql = " SELECT c.idCargo, c.Descricao NomeCargo, fr.id idvaga  FROM cargo c" +
                      " INNER JOIN formularios fr ON fr.idCargo = c.idCargo " +
                      " WHERE fr.idUsuario = " + Usuario;
            }
            else {

                sql = "  SELECT distinct c.idCargo, c.Descricao NomeCargo, null idvaga  FROM cargo c";
                        // " LEFT JOIN formularios fr ON fr.idCargo = c.idCargo ";

            }
             MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosCargo(retornoDr);
            }
           
        }


        public Cargo GetCargo(int IdCargo, int idUsuario)
        {

            string sql = "   SELECT c.idCargo, c.Descricao NomeCargo, fr.id idvaga  FROM cargo c" +
                         " LEFT JOIN formularios fr ON fr.idCargo = c.idCargo " + " AND fr.idUsuario = " + idUsuario +
                         " WHERE c.idCargo = " + IdCargo;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosCargo(retornoDr).FirstOrDefault();
            }

        }
        public List<Cargo> TransformaReaderEmListaDeObjetosCargo(MySqlDataReader reader)
        {


            var lista = new List<Cargo>();
            while (reader.Read())
            {
                var tmpObjeto = new Cargo()
                {
                    idCargo = int.Parse(reader["idCargo"].ToString()),
                    cargo = reader["NomeCargo"].ToString(),
                    idvaga = StringParaInt(reader["idvaga"].ToString()),
                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }
    }
}
