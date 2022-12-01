using RH.DAL;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.BLL
{
    public class PerguntaBLL
    {
        PerguntaDAL _PerguntaDAL = null;
        public PerguntaBLL() {
            _PerguntaDAL = new PerguntaDAL();
        }

        public List<Pergunta> GetPerguntas(int idCargo) {
            return _PerguntaDAL.GetPerguntas(idCargo);
        }

        public int SalvarReposta(List<Pergunta> list, int idUsuario) { 
        
        
        return _PerguntaDAL.SalvarReposta(list, idUsuario);
        
        }


        public int SalvarVagar(int idUsuario, int idCargo)
        {

            return _PerguntaDAL.SalvarVaga(idUsuario, idCargo);
        }

        public int AtualizarCanditados( int idvaga, int Status) {

            return _PerguntaDAL.AtualizarCanditados(idvaga, Status);
        }


    }
}
