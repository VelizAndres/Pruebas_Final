using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_LabED_Andres.Models;
using Final_LabED_Andres.Herramientas.Almacen;

namespace Final_LabED_Andres.Controllers
{
    public class HospitalesController : Controller
    {

        // GET: Hospitales/CrearPaciente
        public ActionResult CrearPaciente()
        {
            return View();
        }

        // POST: Hospitales/Create
        [HttpPost]
        public ActionResult CrearPaciente(FormCollection contenedor)
        {
            try
            {
                
                mPaciente paciente_nuevo = new mPaciente();
                paciente_nuevo.Nombre = contenedor["Nombre"];
                paciente_nuevo.Apellido = contenedor["Apellido"];
                paciente_nuevo.Dpi = contenedor["Dpi"];
                paciente_nuevo.Fecha_Nac = Convert.ToDateTime(contenedor["Fecha_Nac"]);
                paciente_nuevo.Municipio_resi = contenedor["Municipio_resi"];
                paciente_nuevo.Departamento_resi = contenedor["Depart"];
                //Determinar si esta infectado
                if (contenedor["infectado"].Equals("Si"))
                {
                    paciente_nuevo.Infectado = true;
                }
                else
                {
                    paciente_nuevo.Infectado = false;
                }
                paciente_nuevo.Fecha_ingreso = Convert.ToDateTime(contenedor["Fecha_ingreso"]);
                paciente_nuevo.Sintomas = contenedor["Sintomas"];
                paciente_nuevo.Descripcion_contagio = contenedor["Descripcion_contagio"];


                //Verificaciones  
                if (paciente_nuevo.Nombre==null && paciente_nuevo.Apellido == null && paciente_nuevo.Dpi==null && paciente_nuevo.Sintomas==null && paciente_nuevo.Descripcion_contagio==null)
                {
                    ViewBag.Error = "Debe ingresar datos a los campos de nombre, apellido, DPI, sintomas y descripción de contagio";
                    return View("CrearPaciente");
                }
                else if (paciente_nuevo.Nombre == "" && paciente_nuevo.Apellido == "" && paciente_nuevo.Dpi == "" && paciente_nuevo.Sintomas == "" && paciente_nuevo.Descripcion_contagio == "")
                {
                    ViewBag.Error = "Debe llenar datos a los campos de nombre, apellido, DPI, sintomas y descripción de contagio";
                    return View("CrearPaciente");
                }
                if(paciente_nuevo.Dpi.Length==13)
                {
                    ViewBag.Error = "Ingresar DPI correcto";
                    return View("CrearPaciente");
                }
                if (paciente_nuevo.Dpi.Length == 13)
                {
                    ViewBag.Error = "Ingresar DPI correcto";
                    return View("CrearPaciente");
                }
                int fecha = (DateTime.Today.Day - paciente_nuevo.Fecha_Nac.Day) + ((DateTime.Today.Month - paciente_nuevo.Fecha_Nac.Month) * 30) + ((DateTime.Today.Year - paciente_nuevo.Fecha_Nac.Year) * 365);
                if(fecha<0)
                {
                    ViewBag.Error = "Ingresar Fecha de nacimiento  correcta";
                    return View("CrearPaciente");
                }
                fecha = (DateTime.Today.Day - paciente_nuevo.Fecha_ingreso.Day) + ((DateTime.Today.Month - paciente_nuevo.Fecha_ingreso.Month) * 30) + ((DateTime.Today.Year - paciente_nuevo.Fecha_ingreso.Year) * 365);
                if (fecha < 0)
                {
                    ViewBag.Error = "Ingresar Fecha de ingreso correcta";
                    return View("CrearPaciente");
                }
                //Finaliza Verificaciones


                //Determinar a que hospital debe ser enviado
                if (paciente_nuevo.Departamento_resi.Equals("Alta Verapaz")
                    || paciente_nuevo.Departamento_resi.Equals("Petén")
                    || paciente_nuevo.Departamento_resi.Equals("Izabal")
                    || paciente_nuevo.Departamento_resi.Equals("Quiché"))
                {
                    paciente_nuevo.Name_hosp = "Peten";
                }
                else if (paciente_nuevo.Departamento_resi.Equals("Guatemala")
                || paciente_nuevo.Departamento_resi.Equals("Baja Verapaz")
                || paciente_nuevo.Departamento_resi.Equals("El Progreso")
                || paciente_nuevo.Departamento_resi.Equals("Chimaltenango")
                || paciente_nuevo.Departamento_resi.Equals("Sacatepéquez"))
                {
                    paciente_nuevo.Name_hosp = "Guatemala";
                }
                else if (paciente_nuevo.Departamento_resi.Equals("Escuintla")
                || paciente_nuevo.Departamento_resi.Equals("Suchitepéquez")
                || paciente_nuevo.Departamento_resi.Equals("Retalhuleu")
                || paciente_nuevo.Departamento_resi.Equals("Sololá"))
                {
                    paciente_nuevo.Name_hosp = "Escuintla";
                }
                else if (paciente_nuevo.Departamento_resi.Equals("Chiquimula")
                || paciente_nuevo.Departamento_resi.Equals("Jalapa")
                || paciente_nuevo.Departamento_resi.Equals("Jutiapa")
                || paciente_nuevo.Departamento_resi.Equals("Santa Rosa")
                || paciente_nuevo.Departamento_resi.Equals("Zacapa"))
                {
                    paciente_nuevo.Name_hosp = "Chiquimula";
                }
                else if (paciente_nuevo.Departamento_resi.Equals("Quetzaltenango")
                || paciente_nuevo.Departamento_resi.Equals("Quiché")
                || paciente_nuevo.Departamento_resi.Equals("Huehuetenango")
                || paciente_nuevo.Departamento_resi.Equals("Totonicapán")
                || paciente_nuevo.Departamento_resi.Equals("San Marcos"))
                {
                    paciente_nuevo.Name_hosp = "Quetzaltenango";
                }
                double cant_year = ((DateTime.Today.Day - paciente_nuevo.Fecha_Nac.Day) + ((DateTime.Today.Month - paciente_nuevo.Fecha_Nac.Month) * 30) + ((DateTime.Today.Year - paciente_nuevo.Fecha_Nac.Year) * 365))/365;
                //Determinar Prioridad
                if (cant_year<1)
                {
                    if(paciente_nuevo.Infectado)
                    {
                        paciente_nuevo.Prioridad = 2;
                    }
                    else
                    {
                        paciente_nuevo.Prioridad = 6;
                    }

                }
                else if (cant_year < 18)
                {
                    if (paciente_nuevo.Infectado)
                    {
                        paciente_nuevo.Prioridad = 5;
                    }
                    else
                    {
                        paciente_nuevo.Prioridad = 8;
                    }
                }
                else if (cant_year < 60)
                {
                    if (paciente_nuevo.Infectado)
                    {
                        paciente_nuevo.Prioridad = 3;
                    }
                    else
                    {
                        paciente_nuevo.Prioridad = 7;
                    }
                }
                else 
                {
                    if (paciente_nuevo.Infectado)
                    {
                        paciente_nuevo.Prioridad = 1;
                    }
                    else
                    {
                        paciente_nuevo.Prioridad = 4;
                    }
                }
           
                
                //Agregar a los arboles
                Caja_BD.Instance.arbol_Dpi.Agregar(paciente_nuevo, mPaciente.Comparar_DPI);
                Caja_BD.Instance.arbol_Nom.Agregar(paciente_nuevo, mPaciente.Comparar_Nombre);
                Caja_BD.Instance.arbol_Ape.Agregar(paciente_nuevo, mPaciente.Comparar_Apellido);
                //Seleccionar a que hospital ira
                int pos = 0;
                if(paciente_nuevo.Name_hosp.Equals("Peten")) { pos = 1; }
                else if (paciente_nuevo.Name_hosp.Equals("Chiquimula")) { pos = 2; }
                else if (paciente_nuevo.Name_hosp.Equals("Escuintla")) { pos = 3; }
                else if (paciente_nuevo.Name_hosp.Equals("Quetzaltenango")) { pos = 4; }

                //Agregar a la cola
                if(paciente_nuevo.Infectado)
                {
                    Caja_Hospitales.Instance.hospitals[pos].cola_infect.Agregar(paciente_nuevo,mPaciente.EsPrioritario); 
                }
                else
                {
                    Caja_Hospitales.Instance.hospitals[pos].cola_sospech.Agregar(paciente_nuevo, mPaciente.EsPrioritario);
                }
                return RedirectToAction("Index","Home");
            }
            catch
            {
                ViewBag.Error = "Datos Incorrectos";
                return View("CrearPaciente");
            }
        }


        // GET: Hospitales/MenuHospital
        public ActionResult MenuHospital(string hosp)
        {
            switch(hosp)
            {
                case "Guatemala":
                    ViewBag.Hospital = "Guatemala";
                    break;
                case "Petén":
                    ViewBag.Hospital = "Petén";
                    break;
                case "Chiquimula":
                    ViewBag.Hospital = "Chiquimula";
                    break;
                case "Escuintla":
                    ViewBag.Hospital = "Escuintla";
                    break;
                case "Quetzaltenango":
                    ViewBag.Hospital = "Quetzaltenango";
                    break;
            }
            return View();
        }

        // GET: Hospitales/MenuHospital/Sospechosos
        public ActionResult Sospechosos(string hosp)
        {
            mPaciente Paciente_Raiz = new mPaciente();
            switch (hosp)
            {
                case "Guatemala":
                    if (Caja_Hospitales.Instance.hospitals[0].cola_sospech.raiz != null)
                    { 
                    Paciente_Raiz = Caja_Hospitales.Instance.hospitals[0].cola_sospech.raiz.Valor;
                    }
                    ViewBag.Hospital = "Guatemala";
                    break;
                case "Petén":
                    if (Caja_Hospitales.Instance.hospitals[1].cola_sospech.raiz != null)
                    {
                        Paciente_Raiz = Caja_Hospitales.Instance.hospitals[1].cola_sospech.raiz.Valor;
                    } ViewBag.Hospital = "Petén";
                    break;
                case "Chiquimula":
                    if (Caja_Hospitales.Instance.hospitals[2].cola_sospech.raiz != null)
                    {
                        Paciente_Raiz = Caja_Hospitales.Instance.hospitals[2].cola_sospech.raiz.Valor;
                    }
                    ViewBag.Hospital = "Chiquimula";
                    break;
                case "Escuintla":
                    if (Caja_Hospitales.Instance.hospitals[3].cola_sospech.raiz != null)
                    {
                        Paciente_Raiz = Caja_Hospitales.Instance.hospitals[3].cola_sospech.raiz.Valor;
                    }
                    ViewBag.Hospital = "Escuintla";
                    break;
                case "Quetzaltenango":
                    if (Caja_Hospitales.Instance.hospitals[4].cola_sospech.raiz != null)
                    {
                        Paciente_Raiz = Caja_Hospitales.Instance.hospitals[4].cola_sospech.raiz.Valor;
                    }
                    ViewBag.Hospital = "Quetzaltenango";
                    break;
            }
            return View(Paciente_Raiz);

        }

        // GET: Hospitales/MenuHospital/Infectados
        public ActionResult Infectados(string hosp)
        {
            mPaciente Paciente_Raiz = new mPaciente();
            switch (hosp)
            {
                case "Guatemala":
                    if (Caja_Hospitales.Instance.hospitals[0].cola_infect.raiz != null)
                    {
                        Paciente_Raiz = Caja_Hospitales.Instance.hospitals[0].cola_infect.raiz.Valor;
                    }
                    ViewBag.Hospital = "Guatemala";

                    break;
                case "Petén":
                    if (Caja_Hospitales.Instance.hospitals[1].cola_infect.raiz != null)
                    {
                        Paciente_Raiz = Caja_Hospitales.Instance.hospitals[1].cola_infect.raiz.Valor;
                    }
                    ViewBag.Hospital = "Petén";
                    break;
                case "Chiquimula":
                    if (Caja_Hospitales.Instance.hospitals[2].cola_infect.raiz != null)
                    {
                        Paciente_Raiz = Caja_Hospitales.Instance.hospitals[2].cola_infect.raiz.Valor;
                    }
                    ViewBag.Hospital = "Chiquimula";
                    break;
                case "Escuintla":
                    if (Caja_Hospitales.Instance.hospitals[3].cola_infect.raiz != null)
                    {
                        Paciente_Raiz = Caja_Hospitales.Instance.hospitals[3].cola_infect.raiz.Valor;
                    }
                    ViewBag.Hospital = "Escuintla";
                    break;
                case "Quetzaltenango":
                    if (Caja_Hospitales.Instance.hospitals[4].cola_infect.raiz != null)
                    {
                        Paciente_Raiz = Caja_Hospitales.Instance.hospitals[4].cola_infect.raiz.Valor;
                    }
                    ViewBag.Hospital = "Quetzaltenango";
                    break;
            }
            return View(Paciente_Raiz);
        }


        // GET: Hospitales/Simular/
        public ActionResult Simular(string dpi)
        {
            mPaciente paciente = new mPaciente { Dpi = dpi };
            paciente = Caja_BD.Instance.arbol_Dpi.Buscar(paciente, mPaciente.Comparar_DPI);
            int pos = 0;
            switch (paciente.Name_hosp)
            {
                case "Guatemala":
                    pos=0;
                    break;
                case "Petén":
                    pos = 1;
                    break;
                case "Chiquimula":
                    pos = 2;
                    break;
                case "Escuintla":
                    pos = 3;
                    break;
                case "Quetzaltenango":
                    pos = 4;
                    break;
            }

            if (Simulacion(paciente.Descripcion_contagio))
            {
                paciente.Infectado = true;
                Caja_Hospitales.Instance
            }
            else
            {
                Caja_Hospitales.Instance.hospitals[pos].cola_sospech.Eliminar(mPaciente.Comparar_Prioridad, mPaciente.EsPrioritario);
            }
            return View();
        }

        private bool Simulacion(string descripcion)
        {
            Random Rand = new Random();
            int rand=Rand.Next(0, 100);
            int prob_infect = 5;
            string[] Contenedor_descrip = descripcion.Split(Convert.ToChar(" "));
            bool europa = false;
            bool conocido_cont = false;
            bool familiar_cont = false;
            bool reunion_sospech = false;

            foreach(string palabra in Contenedor_descrip)
            {

            }
            for(int i=0; i<Contenedor_descrip.Length;i++)
            {

            }

            return prob_infect > rand ? true : false;
        }




        



        // POST: Hospitales/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hospitales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hospitales/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hospitales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hospitales/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
