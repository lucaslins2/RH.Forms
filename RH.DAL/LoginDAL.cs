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
      
        public Usuario GetLogin ( string usuario, string senha){

            var sql = "SELECT id, cpf,nome,email,  senha, admin " +
         " FROM usuario" +
        "  WHERE senha = @senha AND (cpf = @usuario OR email = @usuario) ";

            Usuario usuarios = null;// new Usuario();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar, 14);
            cmd.Parameters["@usuario"].Value = usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar, 14);
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
                        IdUsuario = int.Parse(retornoDr["id"].ToString()),
                        cpf = retornoDr["cpf"].ToString(),
                        nome = retornoDr["nome"].ToString(),
                        email = retornoDr["email"].ToString(),
                        senha = retornoDr["senha"].ToString(),
                        admin = int.Parse(retornoDr["admin"].ToString()),
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
            


                string sql = "INSERT INTO usuario(cpf,nome, email, senha, admin) Values (@cpf, @nome,@email, @senha, @admin);";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@cpf", usuario.cpf);
                cmd.Parameters.AddWithValue("@nome", usuario.nome);
                cmd.Parameters.AddWithValue("@senha", usuario.senha);
                cmd.Parameters.AddWithValue("@email", usuario.email);
                cmd.Parameters.AddWithValue("@admin", 0);

            using (conexao = new Conexao(null))
            {
                return conexao.ExecutaComando(cmd);
            }
           
             
        
        }

        public Usuario VerificarEmailOuCPF(string email, string cpf)
        {

            var sql = "SELECT id, cpf,nome,email,  senha, admin " +
         " FROM usuario" +
        "  WHERE  cpf = @cpf OR email = @email ";

            Usuario usuarios = null;// new Usuario();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@cpf", MySqlDbType.VarChar, 11);
            cmd.Parameters["@cpf"].Value = cpf;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar, 14);
            cmd.Parameters["@email"].Value = email;

            // Executa Comando => WGSC
            using (conexao = new Conexao(null))
            {
                var retornoDr = conexao.ExecutaComandoComRetornoSdr(cmd);

                if (retornoDr.Read())
                {
                    //string nomeUsuario = retornoDr["NomeUsuario"].ToString();
                    usuarios = new Usuario()
                    {
                        IdUsuario = int.Parse(retornoDr["id"].ToString()),
                        cpf = retornoDr["cpf"].ToString(),
                        nome = retornoDr["nome"].ToString(),
                        email = retornoDr["email"].ToString(),
                        senha = retornoDr["senha"].ToString(),
                        admin = int.Parse(retornoDr["admin"].ToString()),
                        //    admin = Byte.Parse(retornoDr["Admin"].ToString()),
                    };
                }
            }
            return usuarios;
        }
    }
}
