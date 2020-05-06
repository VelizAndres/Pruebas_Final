using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_LabED_Andres.Models;
using Final_LabED_Andres.Herramientas.Almacen;
using Final_LabED_Andres.Herramientas.Estructuras;

namespace Final_LabED_Andres.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(!Caja_BD.Instance.first)
            { 
            Caja_Hospitales.Instance.hospitals[0] = new mHospital { nombre_hospital="Guatemala", cola_infect=new Heap<mPaciente>(), cola_sospech=new Heap<mPaciente>(),camillas=new TablaHash<mPaciente>() };
            Caja_Hospitales.Instance.hospitals[1] = new mHospital { nombre_hospital = "Peten", cola_infect = new Heap<mPaciente>(), cola_sospech = new Heap<mPaciente>(), camillas = new TablaHash<mPaciente>() };
            Caja_Hospitales.Instance.hospitals[2] = new mHospital { nombre_hospital = "Chiquimula", cola_infect = new Heap<mPaciente>(), cola_sospech = new Heap<mPaciente>(), camillas = new TablaHash<mPaciente>() };
            Caja_Hospitales.Instance.hospitals[3] = new mHospital { nombre_hospital = "Escuintla", cola_infect = new Heap<mPaciente>(), cola_sospech = new Heap<mPaciente>(), camillas = new TablaHash<mPaciente>() };
            Caja_Hospitales.Instance.hospitals[4] = new mHospital { nombre_hospital = "Quetzaltenango", cola_infect = new Heap<mPaciente>(), cola_sospech = new Heap<mPaciente>(), camillas = new TablaHash<mPaciente>() };
                Caja_BD.Instance.first = true;
            }
            return View();
        }

        public ActionResult Busquedas()
        {
            List<mPaciente> Lista = new List<mPaciente>();
            return View(Lista);
        }

        public ActionResult Buscar(string Texto, string Tipo)
        {
            List<mPaciente> Encontrados = new List<mPaciente>();
            mPaciente paciente = new mPaciente();
            switch(Tipo)
            {
                case "Nombre":
                    paciente.Nombre = Texto;
                    Encontrados = Caja_BD.Instance.arbol_Nom.Busqueda_Same(paciente, mPaciente.Comparar_Nombre);
                    break;
                case "Apellido":
                    paciente.Apellido = Texto;
                    Encontrados = Caja_BD.Instance.arbol_Nom.Busqueda_Same(paciente, mPaciente.Comparar_Apellido);
                    break;
                case "DPI":
                    paciente.Dpi = Texto;
                    if (Caja_BD.Instance.arbol_Nom.Buscar(paciente, mPaciente.Comparar_DPI) != null)
                    {
                        Encontrados.Add(Caja_BD.Instance.arbol_Nom.Buscar(paciente, mPaciente.Comparar_DPI));
                    }
                   break;
            }
            return View("Busquedas",Encontrados);
           }

        public ActionResult Estadisticas()
        {
            return View();
        }

    }
}