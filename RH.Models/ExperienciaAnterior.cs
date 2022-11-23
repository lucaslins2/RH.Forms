using System;
using System.Collections.Generic;
using System.Text;

namespace RH.Models
{
    public class ExperienciaAnterior
    {
        public int? idExperienciaAnterior { get; set; }
        public int? idUsuario { get; set; }
        public string empresa { get; set; }
        public string telefoneEmpresa { get; set; }
        public string contato { get; set; }
        public string setor { get; set; }
        public string cargoExercido { get; set; }
        public string enderecoEmpresa { get; set; }
        public DateTime? dataAdmissao { get; set; }
        public DateTime? dataDemissao { get; set; }
        public string motivoSaida { get; set; }



    }
}
