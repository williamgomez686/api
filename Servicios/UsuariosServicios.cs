using System;
using System.Collections.Generic;
using System.Data;
using api.Common;
using Oracle.ManagedDataAccess.Client;
using api.Models;

namespace api.Servicios
{
    public class UsuariosServicios
    {
        public string cadena = Parametros.ConnectionString; 
        public static UsuariosServicios _instancia = null;

        private UsuariosServicios()
        {

        }

        public static UsuariosServicios Intancia
         {
            get 
            {
                if (_instancia == null)
                {
                    _instancia = new UsuariosServicios();
                }
                return _instancia;
            }
        }

        public List<Sec_Usuarios> Listar()
        {
            List<Sec_Usuarios> oUsuario = new List<Sec_Usuarios>();
            try
            {
                using (OracleConnection db = new OracleConnection(cadena))
            {
                string consulta = @"SELECT PAICOD, EMPCOD,	USUCOD,	USUNOM,	USUPAS,	GRUCOD, USUSTA, USUNIV, USUFECALTA, USUUSUALTA
                                        FROM SEC_USUARIOS";
                db.Open();
                using (OracleCommand cmd = new OracleCommand(consulta, db))
                {
                    cmd.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {                                
                            oUsuario.Add(new Sec_Usuarios()
                            { 
                                PAICOD = dr.GetString(0),
                                EMPCOD = dr.GetString(1),
                                USUCOD = dr.GetString(2),
                                USUNOM = dr.GetString(3),
                                USUPAS = dr.GetString(4),
                                GRUCOD = dr.GetString(5),
                                USUSTA = dr.GetString(6),
                                USUNIV = dr.GetString(7),
                                USUFECALTA= dr.GetDateTime(8),
                                USUUSUALTA  = dr.GetString(9)
                            });
                        }
                    }
                }
                return oUsuario;
            }
            }
            catch(Exception ex)
            { 
                var error = $"Ocurrio un Error: {ex.Message}";
                return oUsuario;
            }
        }
    }
}