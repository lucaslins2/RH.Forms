using RH.DAL;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.BLL
{
    public  class EstadoCivilBLL
    {
         EstadoCivilDAL _EstadoCivilDAL = null;
        public EstadoCivilBLL() {

            _EstadoCivilDAL = new EstadoCivilDAL();

        }

        public List<EstadoCivil> GetEstadosCivies() { 
        
        return _EstadoCivilDAL.GetEstadosCivies();
        }



    }
}
