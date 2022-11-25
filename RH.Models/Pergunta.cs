using System;
using System.Collections.Generic;
using System.Text;

namespace RH.Models
{
    public class Pergunta
    {
        public int?  idpergunta { get; set; }
        public int? idcargo { get; set; }
        public string descricao { get; set; }
        public int? peso { get; set; }
        public int reposta { get; set; }  
     
        public int idReposta { get; set; }
    }
}
