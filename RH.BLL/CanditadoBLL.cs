using System;
using System.Collections.Generic;
using System.Text;
using RH.Models;
using RH.DAL;
namespace RH.BLL
{

    public class CanditadoBLL
    {


        private readonly CanditadoDAL _CanditadoDAL;
        public CanditadoBLL(){
            _CanditadoDAL = new CanditadoDAL();

               }
        public List<Canditado> GetCanditados() {
            return _CanditadoDAL.GetCanditados();
        }

        public List<Canditado> GetCanditadosPesquisa(Filtros filtros)
        {
            return _CanditadoDAL.GetCanditadosPesquisa(filtros);
        }
        public List<Cidades> GetCidades() {

            return _CanditadoDAL.GetCidades();
        
        }
    }
}
