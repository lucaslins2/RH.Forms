using RH.DAL;
using RH.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RH.BLL
{
    public  class GrauDeEstudoBLL
    {

        private readonly GrauDeEstudoDAL _GrauDeEstudoDAL;
        public GrauDeEstudoBLL()
        {
            _GrauDeEstudoDAL = new GrauDeEstudoDAL();

        }

        public List<GraudeEstudo> GetGrauDeEstudos()
        {

            return _GrauDeEstudoDAL.GetGrauDeEstudos();
        }



    }
}
