using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _888067.Actividad03
{
    static class PlanDeCuentas
    {
        private static readonly Dictionary<int, Cuenta> DicCuentas;
        const string Path = @"C:\Users\ifigueroa006\Desktop\Personal\FCE\CAI\Actividad 3\Plan de cuentas.txt";

        static PlanDeCuentas()
        {
            DicCuentas = new Dictionary<int, Cuenta>();

            FileInfo FI = new FileInfo(Path);

            if (FI.Exists)
            {
                using (var SR = new StreamReader(Path))
                {
                    while (!SR.EndOfStream)
                    {
                        string Linea = SR.ReadLine();
                        var cuenta = new Cuenta(Linea);
                        DicCuentas.Add(cuenta.Codigo, cuenta);
                    }
                }

            }

        }

        public static void Agregar(Cuenta cuenta)
        {
            DicCuentas.Add(cuenta.Codigo, cuenta);
            GrabarCuenta();

        }

        public static Cuenta SeleccionarCuenta()
        {
            var modelo = Cuenta.ModelodeBusqueda();

            foreach (var cuenta in DicCuentas.Values)
            {
                if (cuenta.CoincideCon(modelo))
                {
                    return cuenta;
                }
            }

            Console.WriteLine("No se encontró la cuenta que desea buscar");
            return null;
        }

        public static void Baja(Cuenta cuenta)
        {
            DicCuentas.Remove(cuenta.Codigo);
            GrabarCuenta();
        }

        public static bool Existe(int codigo)
        {
            return DicCuentas.ContainsKey(codigo);
        }

        public static void GrabarCuenta()
        {
            using (var writer = new StreamWriter(Path, append: false))
            {
                foreach (var cuenta in DicCuentas.Values)
                {
                    var linea = cuenta.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }

        }
    }
}
