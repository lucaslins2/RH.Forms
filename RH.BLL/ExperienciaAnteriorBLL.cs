using RH.DAL;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.BLL
{
    public class ExperienciaAnteriorBLL
    {

        ExperienciaAnteriorDAL _ExperienciaAnteriorDAL;
        public ExperienciaAnteriorBLL() {
            _ExperienciaAnteriorDAL = new ExperienciaAnteriorDAL();
        }
        public int CadastrarExperienciasAnterior(List<ExperienciaAnterior> experienciaAnteriores, int idUsuario)
        {

            return _ExperienciaAnteriorDAL.CadastrarExperienciasAnterior(experienciaAnteriores, idUsuario);
        }

        public List<ExperienciaAnterior> GetExperienciaAnteriores(int idUsuario) {

            return _ExperienciaAnteriorDAL.GetExperienciasAnteriores(idUsuario);
        }
        public int DeletarExperienciasAnterior(int idUsuario) {


            return _ExperienciaAnteriorDAL.DeletarExperienciasAnterior(idUsuario);


        }
    }
}
