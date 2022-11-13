using System;
using System.ComponentModel.DataAnnotations;

namespace RH.Models
{

    public class Usuario
    {
        public int IdUsuario { get; set; }  
        [Required(ErrorMessage = "O usuario é obrigatório")]
        public string usuario { get; set; }

        public string email { get; set; }
        [Required(ErrorMessage = "a senha é obrigatório")]
        public string senha { get; set; }
     
        public byte admin { get; set; } 
       
            
}
}

