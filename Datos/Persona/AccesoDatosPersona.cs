using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class AccesoDatosPersona
    {
        public int Insert(int Rut, string Nombre, string Apellido, string Calle, int Numero, string Comuna)
        {
            SqlCommand _comando = MetodosDatosPersona.CrearComandoProc();
            _comando.Parameters.AddWithValue("@rut", Rut);
            _comando.Parameters.AddWithValue("@nombre", Nombre);
            _comando.Parameters.AddWithValue("@apellido", Apellido);
            _comando.Parameters.AddWithValue("@calle", Calle);
            _comando.Parameters.AddWithValue("@numero", Numero);
            _comando.Parameters.AddWithValue("@comuna", Comuna);
            return MetodosDatosPersona.EjecutarComandoInsert(_comando);
        }

        public int Modificar(int Rut, string Nombre, string Apellido, string Calle, int Numero, string Comuna)
        {
            SqlCommand _comando = MetodosDatosPersona.CrearComandoProcModificar();
            _comando.Parameters.AddWithValue("@Rut", Rut);
            _comando.Parameters.AddWithValue("@Nombre", Nombre);
            _comando.Parameters.AddWithValue("@apellido", Apellido);
            _comando.Parameters.AddWithValue("@calle", Calle);
            _comando.Parameters.AddWithValue("@numero", Numero);
            _comando.Parameters.AddWithValue("@comuna", Comuna);
            return MetodosDatosPersona.EjecutarComandoModificar(_comando);
        }

        public static DataTable ObtenerPersonas()
        {
            SqlCommand _comando = MetodosDatosPersona.CrearComando();
            _comando.CommandText = "SELECT * FROM Persona";
            return MetodosDatosPersona.EjecutarComandoSelect(_comando);
        }


        public static DataTable BuscarPersona(int buscar)
        {
            SqlCommand _comando = MetodosDatosPersona.CrearComando();
            _comando.CommandText = "SELECT * FROM Persona where rut = " + buscar + "";
            return MetodosDatosPersona.EjecutarComandoSelect(_comando);
        }

    }
}
