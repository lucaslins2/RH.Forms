using RH.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using RH.Models;
using System.Reflection.Metadata.Ecma335;

namespace RH.BLL
{
    public  class CargoBLL
    {
        private readonly CargoDAL _CargoDAL;
        public CargoBLL() {
            _CargoDAL = new CargoDAL();

        }

        public List<Cargo> GetCargosBLL(int idUsuario ) {

            return _CargoDAL.GetCargos(idUsuario);
        }
        public Cargo GetCargo(int id) {


            return _CargoDAL.GetCargo(id);
        }


    }




}
