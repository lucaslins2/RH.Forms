using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RH.Models
{
   public class Submissoes
    {
        public int idVaga { get; set; }
        public string NomeVaga { get; set; }
        public int? Status { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DtaCad { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm")]
        public DateTime? HoraCad { get; set; }
        public DateTime? DtaResp { get; set; }
           
    }
}
