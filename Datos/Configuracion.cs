using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos
{
    public class Configuracion
    {
        
        //Macbook
        //static string cadenaConexion = @"Data Source=DESKTOP-JCTG6F3\SQLEXPRESS;Initial Catalog=RegistroVehiculos; Trusted_Connection=True;";
        //PC
        static string cadenaConexion = @"Data Source=DESKTOP-GQ70PVR\SQLEXPRESS;Initial Catalog=RegistroVehiculos; Trusted_Connection=True;";
        
        public static string CadenaConexion
        {
            get { return cadenaConexion; }
        }
    }
}
