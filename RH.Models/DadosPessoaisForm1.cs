using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.Models
{
    public class DadosPessoaisForm1
    {
        public int? idUsuario { get; set; }
        public int idCargo { get; set; }
        public string nomecompleto { get; set; }
        public byte[] fotoperfil { get; set; }
        public string nomepai { get; set; }
        public string nomemae { get; set; }
        public int idestadocivil { get; set; }
        public string nomeesposa { get; set; }
        public DateTime dtanascimento { get; set; }
        public int nrodepedentes { get; set; }
        public string naturalidade { get; set; }
        public string nacionalidade { get; set; }
        // public Cargo cargos { get; set; }



    }



    public class Cargo {
    public int idCargo { get; set; }
    public string  cargo { get; set; }

    }
    public class GraudeEstudo 
    {

        public int idgraudeestudo { get; set; }
        public string graudeestudo { get; set; }



    }


    public class EstadoCivil {

        public int idestadocivil { get; set; }
        public string estadocivil { get; set; }

    }
}
