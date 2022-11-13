using System;
using RH.Models;
using RH.DAL;
namespace RH.BLL
{
    public class LoginBLL
    {
        private readonly LoginDAL _loginDALL;
        public LoginBLL()
        {
            _loginDALL = new LoginDAL();
        }
        public Usuario Login(string usuario, string senha)
        {
            return _loginDALL.GetLogin(usuario, senha);
        }
        public int Registrar(Usuario usuario) {

            return _loginDALL.Cadastrar(usuario);
        }

    }
}
