using RH.DAL;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.BLL
{
    public  class EstadoBLL
    {


        EstadoDAL _EstadoDAL = null;
        public EstadoBLL()
        {

            _EstadoDAL = new EstadoDAL();

        }

        public List<Estado> GetEstados()
        {

            return _EstadoDAL.GetEstados();
        }


    }
}
