using Microsoft.AspNetCore.Mvc;

namespace RH.UI.WEB.Models
{
    public class Login
    {
        public string cpf { get; set; }
        public string nome { get; set; }

        public string senha { get; set; }

        public string email { get; set; }
       
        public string ConfirmarSenha { get; set; }
    }
}
