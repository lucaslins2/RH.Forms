using MySqlConnector;
using RH.DB;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RH.DAL
{
    public class CargoDAL
    {
       private Conexao conexao = null;
        public List<Cargo> GetCargos()
        {

            string sql = "SELECT idCargo, Descricao NomeCargo FROM cargo";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);
                return TransformaReaderEmListaDeObjetosCargo(retornoDr);
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

                };
                lista.Add(tmpObjeto);
            }
            return lista;
        }
    }
}
