using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_LabED_Andres.Herramientas.Almacen
{
    public class Caja_Estadisticas
    {
        private static Caja_Estadisticas _instance = null;

        public static Caja_Estadisticas Instance
        {
            get
            {
                if (_instance == null) _instance = new Caja_Estadisticas();
                return _instance;
            }
        }
        public int ingres_infect;
        public int ingres_sospech;
        public int activos;
        public int recuperados;
        public int No_Infect;
    }
}