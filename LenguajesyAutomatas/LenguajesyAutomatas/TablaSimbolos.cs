using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LenguajesyAutomatas
{

    public class NodoClase
    {
        public string lexema;
        public NodoClase herencia;
        public Dictionary<object, NodoAtributo> tablaDeSimbolosAtributos = new Dictionary<object, NodoAtributo>();
        public Dictionary<object, NodoMetodo> tablaDeSimbolosMetodos = new Dictionary<object, NodoMetodo>();
    }
    public class NodoAtributo {
        public string lexema;
    }
    public class NodoMetodo {
        public string lexema;
        public Dictionary<object, NodoParametro> TablaParametro = new Dictionary<object, NodoParametro>();
        public Dictionary<object, NodoVariable> TablaVariables = new Dictionary<object, NodoVariable>();
    }
    public class NodoParametro {
        public string lexema;
    }
    public class NodoVariable {
        public string lexema;
    }

    public enum TipoDeDato
    {
        SinEspecificar,
        Entero,
        Decimal,
        Cadena,
        Error,
        Vacio
    }

    public enum TipoDeVariable
    {
        VariableLocal,
        Parametro,
        Atributo
    }

    public enum Estado
    {
        Insertado,
        Duplicado,
        NoExisteClase
    }

    public class TablaSimbolos
    {
        public NodoClase claseActual = new NodoClase();

        public NodoMetodo metodoActual = new NodoMetodo();

        public static Dictionary<object, NodoClase> tablaDeSimbolosClase = new Dictionary<object, NodoClase>();

        public NodoClase ObtenerNodoClase(string lexema)
        {
            if (tablaDeSimbolosClase.ContainsKey(lexema))
                return tablaDeSimbolosClase.SingleOrDefault(x => x.Key.ToString() == lexema).Value;
            else
                throw new Exception("Error Semantico: No existe el nombre de Clase");
        }

        public Estado InsertarNodoClase(NodoClase _nodoclase)
        {
            if (!tablaDeSimbolosClase.ContainsKey(_nodoclase.lexema))
            {
                tablaDeSimbolosClase.Add(_nodoclase.lexema, _nodoclase);
                return Estado.Insertado;
            }
            else
            {
                return Estado.Duplicado;
            }
        }

        public Estado InsertarHerencia(string _herencia)
        {
            NodoClase _herenciainsertar = ObtenerNodoClase(_herencia);
            if (_herenciainsertar.lexema != null)
            {
                claseActual.herencia.lexema = _herenciainsertar.lexema.ToString();
                claseActual.herencia.herencia = _herenciainsertar.herencia;
                claseActual.herencia.tablaDeSimbolosAtributos = _herenciainsertar.tablaDeSimbolosAtributos;
                claseActual.herencia.tablaDeSimbolosMetodos = _herenciainsertar.tablaDeSimbolosMetodos;

                if (tablaDeSimbolosClase.ContainsKey(claseActual.lexema))
                {
                    tablaDeSimbolosClase.Remove(claseActual.lexema);
                    InsertarNodoClase(claseActual);
                    return Estado.Insertado;
                }
                else
                {
                    throw new Exception("Error Semantico: No existe el nombre de Clase");
                }
            }
            else
            {
                return Estado.NoExisteClase;
            }
        }

    }
}
