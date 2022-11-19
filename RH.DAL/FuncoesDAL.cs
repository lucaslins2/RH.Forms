using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace RH.DAL
{
    public class FuncoesDAL
    {

        public static int? StringParaInt(string strCampo)
        {

            int? valor;
            //if (strCampo.Trim() == "")
            if (String.IsNullOrEmpty(strCampo))
                valor = null;
            else
                valor = int.Parse(strCampo); //Convert.ToInt32(strCampo);

            return valor;
        }
        public static byte? StringParaByte(string strCampo)
        {
            byte? valor;
            //if (strCampo.Trim() == "")
            if (String.IsNullOrEmpty(strCampo))
                valor = null;
            else
                valor = byte.Parse(strCampo); //Convert.ToInt32(strCampo);

            return valor;
        }
        public static short? StringParaShort(string strCampo)
        {
            short? valor;
            //if (strCampo.Trim() == "")
            if (String.IsNullOrEmpty(strCampo))
                valor = null;
            else
                valor = short.Parse(strCampo); //Convert.ToInt32(strCampo);

            return valor;
        }

        public static DateTime? StringParaDate(string strCampo)
        {
            DateTime? valor;
            //if (strCampo.Trim() == "")
            if (String.IsNullOrEmpty(strCampo))
                valor = null;
            else
                valor = DateTime.Parse(strCampo);// Convert.ToInt32(strCampo);

            return valor;
        }
        public static float? StringParaFloat(string strCampo, int tipo = 0)
        {
            float? valor;
            //if (strCampo.Trim() == "")
            if (String.IsNullOrEmpty(strCampo))
            {
                // valor = null;
                if (tipo == 0)
                    valor = null;
                else
                    valor = 0;
            }
            else
                valor = float.Parse(strCampo);// Convert.ToInt32(strCampo);

            return valor;
        }

        public static decimal? StringParaDecimal(string strCampo, int tipo = 0)
        {
            decimal? valor;
            if (String.IsNullOrEmpty(strCampo))
            {
                //valor = null;
                if (tipo == 0)
                    valor = null;
                else
                    valor = 0;
            }

            else
                valor = decimal.Parse(strCampo);

            return valor;
        }
        public static double? StringParaDouble(string strCampo, int tipo = 0)
        {
            double? valor;
            if (String.IsNullOrEmpty(strCampo))
            {
                //valor = null;
                if (tipo == 0)
                    valor = null;
                else
                    valor = 0;
            }
            else
                valor = double.Parse(strCampo);

            return valor;
        }

        public static long? StringParaLong(string strCampo, int tipo = 0)
        {
            long? valor;
            if (String.IsNullOrEmpty(strCampo))
            {
                //valor = null;
                if (tipo == 0)
                    valor = null;
                else
                    valor = 0;
            }

            else
                valor = long.Parse(strCampo);

            return valor;
        }

        public static decimal DecimalNullZero(decimal? Campo)
        {
            return Campo ?? 0;
        }

        public static double DoubleNullZero(double? Campo)
        {
            return Campo ?? 0;
        }

        public static string NumeroParaQuery(string strCampo, int tipo = 0)
        {
            //if (strCampo == "" || strCampo == "0")
            if (String.IsNullOrEmpty(strCampo) || (strCampo == "0"))
                return tipo == 0 ? "NULL" : "0"; // valor: 0: Null 1: 0
            else
                return strCampo.Replace(",", ".");

        }


        public static string DataParaQuery(string strCampo)
        {
            //if (strCampo == "")
            if (String.IsNullOrEmpty(strCampo))
                return "NULL";
            else
                return "'" + strCampo + "'";
        }
        public static string StringParaQuery(string strCampo)
        {
            //if (strCampo == null || strCampo == "")
            if (String.IsNullOrEmpty(strCampo))
                return "NULL";
            else
                return "'" + strCampo + "'";
        }
        /// <summary>
        /// Método para converter dados nulos vindos do banco para dados do .NET
        /// </summary>
        /// <param name="val">Valor de Entrada</param>
        /// <returns>Retorna o valor de saída para cada tido de dados nulos</returns>
        public static object DBNullParaNull(object val)
        {
            return Convert.IsDBNull(val) ? null : val;
        }
        public static object NullParaDBNull(object val)
        {
            //return ((object)strCampo) ?? Convert.DBNull;
            return ((object)val) == null ? DBNull.Value : (object)val;
        }

        public static object NullZeroParaDBNull(object val)
        {
            return (object)val == null || (object)val == (object)0 ? DBNull.Value : (object)val;
        }
        public static object MaskParaDBNull(string val)
        {
            return val == "" || (object)val == null ? DBNull.Value : (object)RemoveCaracteresEspeciais(val);
        }
        public static string RemoveCaracteresEspeciais(string strTexto)
        {
            return Regex.Replace(strTexto, "[^0-9a-zA-Z]+?", "");
        }

        public static string SomenteNumeros(string strTexto)
        {
            if (String.IsNullOrEmpty(strTexto))
            {
                return strTexto;
            }
            else
            {
                return string.Join(null, System.Text.RegularExpressions.Regex.Split(strTexto, "[^\\d]"));
            }
        }


        //public static object DefaultDbNull(this Object value, object defaultValue)
        //{

        //    if (value == Convert.DBNull)

        //        return (object)defaultValue;

        //    return (object)value;

        //}        


       

        /// <summary>
        /// SHA1HashData SHA1 hash
        /// </summary>
        /// <param name="text">input string</param>
        /// <param name="enc">Character encoding</param>
        /// <returns>SHA1 hash</returns>
        public static string SHA1HashData(string text, Encoding enc)
        {
            try
            {
                byte[] buffer = enc.GetBytes(text);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
                return hash;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        // ou se você não quiser usar a encoding como parâmetro.
        public static string SHA1HashData(string text)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
                return hash;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        public static MySqlCommand CriaCommand(string sql)
        {
            MySqlCommand scm = new MySqlCommand();
            scm.CommandType = CommandType.Text;
            scm.CommandText = sql;
            return scm;

        }
    }
}
