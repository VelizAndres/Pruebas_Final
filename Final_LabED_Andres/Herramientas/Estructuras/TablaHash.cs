using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_LabED_Andres.Herramientas.Estructuras
{
    public class TablaHash <T>
    {
     //   List<T>[] Tabla_List = new List<T>[10];
        T[] TablaHash_V = new T[10];
        

        public void Guardar_V(string llave, T nuevo_elemento, Delegate CodeHash)
        {
            int posicion = (int)CodeHash.DynamicInvoke(llave);
            if (TablaHash_V[posicion] == null)
            {
                TablaHash_V[posicion] = nuevo_elemento;
            }
            else
            {
                while(TablaHash_V[posicion]!=null)
                {
                    if(posicion==9)
                    {
                        posicion = 0;
                    }
                    else
                    {
                        posicion++;
                    }
                }
                TablaHash_V[posicion] = nuevo_elemento;
            }
        }

        public T Buscar(string llave, Delegate CodeHash, Delegate Obt_ValorInt)
        {
            int ciclo = 0;
            int posicion = (int)CodeHash.DynamicInvoke(llave);
            T Elemento_Buscado = default(T);
            while(llave!= (string)Obt_ValorInt.DynamicInvoke(TablaHash_V[posicion]) && ciclo<10)
            {
                ciclo++;
                if(posicion==9)
                {
                    posicion = 0;
                }
                else
                {
                    posicion++;
                }
            }
            if(llave == (string)Obt_ValorInt.DynamicInvoke(TablaHash_V[posicion]))
            {
                return TablaHash_V[posicion];
            }
            else
            {
                return Elemento_Buscado;
            }
        }

        public T[] Retorna_Tabla()
        {



            return TablaHash_V;
        }

        public void Eliminar(string llave, Delegate Obt_ValorInt, Delegate CodeHash)
        {
            int ciclo = 0;
            int posicion = (int)CodeHash.DynamicInvoke(llave);
            while (llave != (string)Obt_ValorInt.DynamicInvoke(TablaHash_V[posicion]) && ciclo < 10)
            {
                ciclo++;
                if (posicion == 9)
                {
                    posicion = 0;
                }
                else
                {
                    posicion++;
                }
            }
            if (llave == (string)Obt_ValorInt.DynamicInvoke(TablaHash_V[posicion]))
            {
                TablaHash_V[posicion]=default(T);
            }
        }









        /*
                public bool Guardar(string llave, T nuevo_elemento, Delegate Obt_Titulo, Delegate CodeHash)
                {
                    int posicion = (int)CodeHash.DynamicInvoke(llave);
                    if (Tabla_List[posicion] == null)
                    {
                        Tabla_List[posicion] = new List<T>();
                        Tabla_List[posicion].Add(nuevo_elemento);
                        return false;
                    }
                    else
                    {
                        bool existe_E = false;
                        string llave_E = "";
                        foreach (T elemento in Tabla_List[posicion])
                        {
                            llave_E = (string)Obt_Titulo.DynamicInvoke(elemento);
                            if (llave_E == llave) { existe_E = true; }
                        }
                        if (existe_E)
                        {
                            //"Ya existe la tarea "
                            return true;
                        }
                        else
                        {
                            Tabla_List[posicion].Add(nuevo_elemento);
                            return false;
                        }
                    }
                }

                public T Buscar(string llave, Delegate Obt_Titulo, Delegate CodeHash)
                {
                    int posicion = (int)CodeHash.DynamicInvoke(llave);
                    T Elemento_Buscado = default(T);
                    foreach (T Buscado in Tabla_List[posicion])
                    {
                        string llave_buscado = (string)Obt_Titulo.DynamicInvoke(Buscado);
                        if (llave.Equals(llave_buscado))
                        {
                            Elemento_Buscado = Buscado;
                        }
                    }
                    return Elemento_Buscado;
                }

                public List<T>[] Retorna_Tabla()
                {
                    return Tabla_List;
                }

                public void Eliminar(string llave, Delegate Obt_Titulo, Delegate CodeHash)
                {
                    int posicion = (int)CodeHash.DynamicInvoke(llave);
                    Tabla_List[posicion].Remove(Buscar(llave, Obt_Titulo, CodeHash));
                }
          */
    }
}
