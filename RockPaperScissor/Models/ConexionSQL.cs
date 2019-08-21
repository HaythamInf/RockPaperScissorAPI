using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RockPaperScissor.Models
{
    public class ConexionSQL
    {
        public ConexionSQL()
        {
            parametros = new List<Parametro>();
        }
        public static List<Parametro> parametros { get; set; }


        public static SqlConnection ConectarSQL()
        {
            string conexionString = ConfigurationManager.ConnectionStrings["ConexionSQL"].ConnectionString;
            SqlConnection conn = new SqlConnection(conexionString);
            return conn;
        }

        public DataSet ConsumirProcedimientoAlmacenado(string strNombreProcedimiento)
        {
            DataTable respuesta = new DataTable();
            SqlConnection con = ConectarSQL();


            DataSet dtResultadosSQL = new DataSet();
            try
            {
                SqlCommand comando = new SqlCommand(strNombreProcedimiento, con);
                comando.CommandType = CommandType.StoredProcedure;
                SqlCommandAddParametro(ref comando);

                con.Open();

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                comando.CommandTimeout = 45;

                DataSet dsResultadosSQL = new DataSet();
                adaptador.Fill(dsResultadosSQL);

                con.Close();

                if (dsResultadosSQL.Tables.Count > 0)
                {
                    dtResultadosSQL = dsResultadosSQL;
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error en ejecucion del sp..." + ex.Message);
                con.Close();
            }

            return (dtResultadosSQL);

        }

        public void AddParametros(string strNombreCampoSQL, SqlDbType enumTipo, object Valor)
        {
            Parametro nuevoParametro = new Parametro();

            nuevoParametro.StrNombreCampoSQL = strNombreCampoSQL;
            nuevoParametro.EnumTipo = enumTipo;
            nuevoParametro.Valor = Valor;

            parametros.Add(nuevoParametro);
        }

        private static void SqlCommandAddParametro(ref SqlCommand comando)
        {
            foreach (Parametro parametro in parametros)
            {
                if (parametro.EnumTipo == SqlDbType.DateTime && (DateTime)parametro.Valor == DateTime.MinValue)
                {
                    parametro.Valor = DBNull.Value;
                }

                if (parametro.EnumTipo == SqlDbType.VarChar && (string)parametro.Valor == null)
                {
                    parametro.Valor = string.Empty;
                }
                //else if (parametro.Valor == null)
                //{
                //    parametro.Valor = DBNull.Value;
                //}
                comando.Parameters.Add(parametro.StrNombreCampoSQL, parametro.EnumTipo);
                comando.Parameters[parametro.StrNombreCampoSQL].Value = parametro.Valor;
            }
        }

    }
    public class Parametro
    {
        #region Propiedades

        public string StrNombreCampoSQL { get; set; }
        public object Valor { get; set; }
        public SqlDbType EnumTipo { get; set; }

        #endregion

        #region Constructor

        public Parametro()
        {
            this.Init();
        }

        #endregion

        #region Metodos

        private void Init()
        {
            this.StrNombreCampoSQL = string.Empty;
            this.Valor = null;
            this.EnumTipo = SqlDbType.VarChar;
        }

        #endregion
    }
}