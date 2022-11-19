using RH.DAL;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.BLL
{
    public  class DadosPessoaisForm1BLL
    {
        private readonly DadosPessoaisForm1DAL _DadosPessoaisForm1DAL;
        public DadosPessoaisForm1BLL() {
            _DadosPessoaisForm1DAL = new DadosPessoaisForm1DAL();   


        }
        public DadosPessoaisForm1 GetDadosPessoaisForm1(int idUsuario)
        {
            return _DadosPessoaisForm1DAL.GetDadosPessoaisForm1(idUsuario);

        }

        public int CadastrarDadosPessoaisForm1(DadosPessoaisForm1 dadosPessoaisForm1,int idUsuario, int Operacao) {

            return _DadosPessoaisForm1DAL.CadastrarDadosPessoaisForm1(dadosPessoaisForm1,idUsuario, Operacao);
        
        }


    }
}
