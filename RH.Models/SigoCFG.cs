using System;
using System.Collections.Generic;
using System.Text;

namespace RH.Models
{
    public class SigoCFG
    {
        public SigoCFG()
        {
            Servidor = @"127.0.0.1";
            PortaDB = "3306";
            NomeDB = "db_rh";
            IdClienteWS = 1;
            IdUsuario = 201;
            TipoUsuario = 2; // 2: Usuário Normal  3: Cliente  4: Credenciado
            IdUnidade = 1;
            NomeUsuario = "Suporte";
            NroCpfObrig = 0;
        }

        public object CriaCommand(string sql)
        {
            throw new NotImplementedException();
        }

        // Dados de Acesso DB
        public string Servidor { get; set; }
        public string PortaDB { get; set; }
        public string NomeDB { get; set; }
        public string UsuarioDB { get; set; }
        public string SenhaDB { get; set; }
        public int IdClienteWS { get; set; }
        public int IdUsuario { get; set; }
        public byte TipoUsuario { get; set; }
        public int? IdUnidade { get; set; }
        /// <summary>
        /// ID da empresa que será o filtro para o usuário 3: Cliente
        /// </summary>
        public int? IdEmpresa { get; set; }
        public string NomeUsuario { get; set; }
        // Parâmetros Sigo_Cfg
        public byte? NroCpfObrig { get; set; }

    }
}
