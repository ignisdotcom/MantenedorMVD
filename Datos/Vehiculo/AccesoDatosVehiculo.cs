using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Datos
{
    public class AccesoDatosVehiculo
    {
        public int Insert(string Patente, string Marca, string Modelo, int Annio, string Color, int Rut)
        {
            SqlCommand _comando = MetodosDatosVehiculo.CrearComandoProc();
            _comando.Parameters.AddWithValue("@patente", Patente);
            _comando.Parameters.AddWithValue("@marca", Marca);
            _comando.Parameters.AddWithValue("@modelo", Modelo);
            _comando.Parameters.AddWithValue("@annio", Annio);
            _comando.Parameters.AddWithValue("@color", Color);
            _comando.Parameters.AddWithValue("@rut", Rut);
            return MetodosDatosVehiculo.EjecutarComandoInsert(_comando);
        }

        public int ModificarVehiculo(string Patente, string Marca, string Modelo, int Annio, string Color, int Rut)
        {
            SqlCommand _comando = MetodosDatosVehiculo.CrearComandoProcModificar();
            _comando.Parameters.AddWithValue("@patente", Patente);
            _comando.Parameters.AddWithValue("@marca", Marca);
            _comando.Parameters.AddWithValue("@modelo", Modelo);
            _comando.Parameters.AddWithValue("@annio", Annio);
            _comando.Parameters.AddWithValue("@color", Color);
            _comando.Parameters.AddWithValue("@rut", Rut);
            return MetodosDatosVehiculo.EjecutarComandoModificar(_comando);
        }

        public static DataTable ObtenerVehiculos()
        {
            SqlCommand _comando = MetodosDatosVehiculo.CrearComando();
            _comando.CommandText = "SELECT * FROM Vehiculo";
            return MetodosDatosVehiculo.EjecutarComandoSelect(_comando);
        }

        public static DataTable BuscarVehiculosDueño(int buscar)
        {
            SqlCommand _comando = MetodosDatosVehiculo.CrearComando();
            _comando.CommandText = "SELECT * FROM Vehiculo where rut = " + buscar + "";
            return MetodosDatosVehiculo.EjecutarComandoSelect(_comando);
        }

        public static DataTable BuscarVehiculosPatente(string buscar)
        {
            SqlCommand _comando = MetodosDatosVehiculo.CrearComando();
            _comando.CommandText = "SELECT * FROM Vehiculo where patente = '" + buscar + "'";
            return MetodosDatosVehiculo.EjecutarComandoSelect(_comando);
        }
    }
}
