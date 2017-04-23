using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios
{
    class Inventario
    {
        //cabecera de la lista, = null en contructor porque no apunta a nada al iniciar
        private Producto cabecera;
        //registro final de la lista, == null en contructor porque no hay registro final al iniciar
        private Producto final;
        int cont = 0, productos = 0,cont2 = 0;
        /// <summary>
        /// Constructor, cabecera == null, porque no hay nada al iniciar.
        /// </summary>
        public Inventario()
        {
            this.cabecera = null;
            this.final = null;
        }

        //private Producto[] productos = new Producto[20];
        //private int referencia = 0;

        /// <summary>
        /// Agregar Producto al principio de la lista
        /// </summary>
        /// <param name="producto"></param>
        private void agregarPrimero(Producto producto)
        {
            Producto nuevoDato;
            nuevoDato = new Producto(producto);
            if (cabecera == null)
            final = nuevoDato;
            nuevoDato.siguienteDato = cabecera; //el siguente dato del nuevo dato apunta hacia la cabecera original
            cabecera = nuevoDato; // la cabecera apunta al nuevo nodo creado
            
            productos++;
        }

        /// <summary>
        /// Agregar producto al final de la lista
        /// </summary>
        /// <param name="producto"></param>
        private void agregar(Producto producto)
        {
            Producto nuevoDato;
            nuevoDato = new Producto(producto);
            if (cabecera == null) // si no hay datos en la lista, se agrega en la primera posicion.
                cabecera = nuevoDato;
            else
            {
                Producto datoActual = cabecera;
                while (datoActual.siguienteDato != null)
                    datoActual = datoActual.siguienteDato; //el dato actual salta al siguente en la lista (al que apunta)
                datoActual.siguienteDato = nuevoDato;//el ultimo dato que ya existia apunta al nuevo dato que se introdujo
                final = nuevoDato;//control, para saber cual es el dato final
            }
            productos++;
        }

        public void agregarOrdenado(Producto producto)
        {
            cont = 0; cont2 = 0;
            bool agregado = false;
            Producto nuevoDato;
            nuevoDato = new Producto(producto);
            Producto anterior = null, siguiente = null;
            int codigo = producto.codigo;

            if (cabecera == null)
                cabecera = nuevoDato; // si no hay nada, se agrega a la posicion primera.
            else if (cabecera.datos.codigo > codigo)// si el codigo es menor al primero, sera menor a todos, y se agrega a la primera posicion
                agregarPrimero(producto);
            else//en caso de no ser menor que el primero
            {
                Producto datoActual = cabecera;
                while (datoActual != null)//ciclo para obtener la posicion donde debe de entrar
                {
                    cont++;
                    if (datoActual.datos.codigo > producto.codigo)
                    {
                        agregado = true;//es true si encuntra donde debe de ir, si no, significa que es mayor a todos los ya existentes,
                        break;          //y se agregara al final, se evalua en la lines  91
                    }                     
                    datoActual=datoActual.siguienteDato;
                }
                if (agregado == false)//evaluacion de si debe de ir al final o se encotro posicion
                    agregar(producto);//si va al final
                else//si se encotro posicion
                {
                    Producto datoActual2 = cabecera;
                    while (cont2 != cont - 1)//ciclo para llegar a la posicion
                    {
                        anterior = datoActual2;//anterior toma dato actual antes de que cambie al siguente
                        siguiente = datoActual2.siguienteDato;// siguente tomo posicion del siguente dato de la posicion actual
                        datoActual2 = datoActual2.siguienteDato;//dato actual pasa a la siguente posicion
                        cont2++;//control para terminar ciclo al llegar a la psicion correspondiente
                    }
                    anterior.siguienteDato = nuevoDato;//el dato siguiente de la posicion actual apunta al nuevo dato
                    nuevoDato.siguienteDato = siguiente;//el nuevo dato apunta al siguente de la posicion actual que lo apuntaba
                }
            }
        }
        
        /// <summary>
        /// Regresa todos los productos registrados en el inventario
        /// </summary>
        /// <returns></returns>
        public string reporte()
        {
            string infoLista = "";

            Producto datoActual = cabecera;
            while (datoActual != null) //recorre la lista
            {
                infoLista += datoActual.datos+Environment.NewLine; //agrega la info de cada dato a un report
                datoActual = datoActual.siguienteDato;
            }

            return infoLista;
        }

        /// <summary>
        /// Busca un producto existente del inventario
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public Producto buscar(string nombre)
        {
            Producto datoActual = cabecera;
            
            while (datoActual != null)//recorre la lista
            {
                if (datoActual.datos.nombre == nombre)//si el tipo nombre del dato es igual al nombre que se busca, se regresa el dato encotrado.
                {
                    return datoActual.datos;
                }
                else
                    datoActual = datoActual.siguienteDato;//si no es igual, pasamos al siguente dato.
            }
            return null;
        }

        /// <summary>
        /// Elimina producto existente del inventario
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public void eliminar(string nombre)
        {
            Producto datoActual = cabecera;
            Producto anterior = null;

            while (datoActual != null)//recorremos lista
            {
                if (datoActual.datos.nombre == nombre)//si el tipo nombre del dato es igual al nombre que se busca, se toma el dato encotrado
                {
                    if (datoActual == cabecera)// en caso de que el dato a eliminar sea el primero
                    {
                        cabecera = cabecera.siguienteDato;//solo se pone el dato siguente del primero, como primero
                    }
                    else if (datoActual == final)// en caso de eliminar el del final
                    {
                        anterior.siguienteDato = null; // simplemente el anterior de final, lo deja de apuntar
                    }
                    else// en caso de estar entre el primero y el ultimo
                    {
                        anterior.siguienteDato = datoActual.siguienteDato;// el dato siguente del anterior deja de apuntar al actual, y pasa a apuntar
                    }                                                     // al dato siguente del actual
                }
                else 
                    anterior = datoActual; //guarda el dato actual, ates de pasar al siguente
                    datoActual = datoActual.siguienteDato;//el dato actual pasa al siguente dato apuntado
            }
            productos--;
        }



        //    public override string ToString()
        //    {
        //        return Convert.ToString(referencia);
        //    }
    }
}
