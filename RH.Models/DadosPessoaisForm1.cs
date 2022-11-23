using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.Models
{
    public class DadosPessoaisForm1
    {
        public int? idUsuario { get; set; }
        public int? idCargo { get; set; }
        public string nomecompleto { get; set; }
        public byte[] fotoperfil { get; set; }
        public string nomepai { get; set; }
        public string nomemae { get; set; }
        public int? idestadocivil { get; set; }
        public string nomeesposa { get; set; }
        public DateTime? dtanascimento { get; set; }
        public int? nrodepedentes { get; set; }
        public string naturalidade { get; set; }
        public string nacionalidade { get; set; }
        // public Cargo cargos { get; set; }
        public int? idgraudeestudo { get; set; }

        public string telefonefixo { get; set; }
        public string celular { get; set; }
        public string rg { get; set; }
        public DateTime? dataexpedicao { get; set; }
        public string orgaoexpedidor { get; set; }
        public string certidaodereservista { get; set; }
        public string carteiradetrabalho { get; set; }
        public string carteiradetrabalhoserie { get; set; }
        public string titulo { get; set; }
        public string zona { get; set; }
        public string secao { get; set; }
        public string cpf { get; set; }
        public string cnh { get; set; }
        public int? idUFcnh { get; set; }
        //    public int iCatCNH { get; set; }
        public List<string> categoriasCNH { get; set; }
        public DateTime? validadecnh { get; set; }
        public DateTime? validadeprimeiracnh { get; set; }
        public int idEndereco { get; set; }
        public string logradouro { get; set; }
        public string numeroend { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string categoriaString { get; set; }
        public Endereco endereco { get; set; }
        public int salvar { get; set; }

        public List<string> empresa { get; set; }
        public List<string> telefoneEmpresa { get; set; }
        public List<string> contato { get; set; }
        public List<string> setor { get; set; }
        public List<string> cargoExercido { get; set; }
        public List<string> enderecoEmpresa { get; set; }
        public List<DateTime> dataAdmissao { get; set; }
        public List<DateTime> dataDemissao { get; set; }
        public List<string> motivoSaida { get; set; }
        public List<int> QtdExp { get; set; }
  
    }




    public class Cargo {
        public int idCargo { get; set; }
        public string cargo { get; set; }

    }
    public class GraudeEstudo
    {

        public int idgraudeestudo { get; set; }
        public string graudeestudo { get; set; }



    }
    public class Estado {

        public int idUF { get; set; }
        public string estado { get; set; }
    }

    public class EstadoCivil {

        public int idestadocivil { get; set; }
        public string estadocivil { get; set; }

    }
    public class Endereco {
            
        public int? idEndereco { get; set;}
        public int? idUsuario { get; set; }
        public string rua { get; set; }
        public string numero { get; set;}
        public string bairro { get; set;}
        public string cep { get; set;}
        public string cidade { get; set;}
        public string estado { get; set;}
            

    }
}
