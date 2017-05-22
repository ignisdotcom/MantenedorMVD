using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Datos;

namespace Negocio
{
    public class AccesoLogica
    {
        #region Logica para los Clientes

        public static DataTable ObtenerPersonas()
        {
            return AccesoDatosPersona.ObtenerPersonas();
        }

        public int InsertPersona(int Rut, string Nombre, string Apellido, string Calle, int Numero, string Comuna)
        {
            AccesoDatosPersona acceso = new AccesoDatosPersona();
            return acceso.Insert(Rut, Nombre, Apellido, Calle, Numero, Comuna);
        }

        public int ModifcarPersona(int Rut, string Nombre, string Apellido, string Calle, int Numero, string Comuna)
        {
            Datos.AccesoDatosPersona acceso = new Datos.AccesoDatosPersona();
            return acceso.Modificar(Rut,Nombre, Apellido, Calle, Numero, Comuna);
        }


        public static DataTable BuscarPersonas(int buscar)
        {
            return Datos.AccesoDatosPersona.BuscarPersona(buscar);
        }

        #endregion


        #region Logica para los Vehículos
        public static DataTable ObtenerVehiculos()
        {
            return AccesoDatosVehiculo.ObtenerVehiculos();
        }


        public int InsertVehiculo(string Patente, string Marca, string Modelo, int Annio, string Color, int Rut)
        {
            AccesoDatosVehiculo acceso = new AccesoDatosVehiculo();
            return acceso.Insert(Patente, Marca, Modelo, Annio, Color, Rut);
        }

        public int ModifcarVehiculo(string Patente, string Marca, string Modelo, int Annio, string Color, int Rut)
        {
            Datos.AccesoDatosVehiculo acceso = new Datos.AccesoDatosVehiculo();
            return acceso.ModificarVehiculo(Patente, Marca,  Modelo, Annio, Color, Rut);
        }

        public static DataTable BuscarVehiculosDueño(int buscar)
        {
            return Datos.AccesoDatosVehiculo.BuscarVehiculosDueño(buscar);
        }

        public static DataTable BuscarVehiculosPatente(string buscar)
        {
            return Datos.AccesoDatosVehiculo.BuscarVehiculosPatente(buscar);
        }


        #endregion
        
    }
}
