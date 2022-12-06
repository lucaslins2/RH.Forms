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
        public Cargo GetCargo(int id,int idUsuario)
        {


            return _CargoDAL.GetCargo(id, idUsuario);
        }

        public List<Submissoes> GetSubmissoes(int IdUsuario) {

            return _CargoDAL.GetSubmissoes(IdUsuario);
        }
        public int VerSubmissao(int idUsuario) {

        return    _CargoDAL.GetVerSubmissoes(idUsuario);
        }

    }




}
