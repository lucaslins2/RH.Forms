using MySqlConnector;
using RH.DB;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace RH.DAL
{
    public class LoginDAL
    {
        private Conexao conexao;
      
        public Usuario GetLogin ( string  usuario, string senha){

            var sql = "SELECT IdUsuario, NomeUser, Senha, Email " +
        " From usuario" +
        " WHERE NomeUser = @usuario AND Senha =  @senha";

            Usuario usuarios = null;// new Usuario();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar, 11);
            cmd.Parameters["@usuario"].Value = usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar, 11);
            cmd.Parameters["@senha"].Value = senha;

            // Executa Comando => WGSC
            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);

                if (retornoDr.Read())
                {
                    //string nomeUsuario = retornoDr["NomeUsuario"].ToString();
                    usuarios = new Usuario()
                    {
                        IdUsuario = int.Parse(retornoDr["IdUsuario"].ToString()),
                        usuario = retornoDr["NomeUser"].ToString(),
                        senha = retornoDr["Senha"].ToString(),
                        email = retornoDr["Email"].ToString(),
                    //    admin = Byte.Parse(retornoDr["Admin"].ToString()),
                    };
                } 
            }
            return usuarios;
        }

        public int Cadastrar(Usuario usuario) {

        
             
                    // Gera o ID
                    //var Id = conexao.SP_GeraID("ID_Usuario");
                    int retorno = 0;
            


                string sql = "INSERT INTO usuario(NomeUser, Senha, Email, Admin) Values (@Usuario, @Senha, @Email, @Admin);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@Usuario", usuario.usuario);
                cmd.Parameters.AddWithValue("@Senha", usuario.senha);
                cmd.Parameters.AddWithValue("@Email", usuario.email);
                cmd.Parameters.AddWithValue("@Admin", 0);

            using (conexao = new Conexao(null))
            {
                return conexao.ExecutaComando(cmd);
            }
           
             
        
        }


    }
}
