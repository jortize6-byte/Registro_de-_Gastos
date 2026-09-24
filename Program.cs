using System;
using System.Collections.Generic;
using System.IO;
namespace Registro_de__Gastos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ruta = "gastos.csv";
            List<Gastos> gastos = Cargar(ruta);

            int siguienteId = 1;
            foreach (Gastos g in gastos)
            {
                if (g.Id >= siguienteId)
                    siguienteId = g.Id + 1;
            }
           
            string opcion;
           
            do
            {
                opcion = LeerOpcionMenu();
                switch (opcion)
                {
                    case "1":
                        AgregarGasto(gastos, ref siguienteId);
                        break;
                    case "2":
                        ListarGastos(gastos);
                        break;
                    case "3":
                        BuscarPorCategoria(gastos);
                        break;
                    case "4":
                        Guardar(gastos, ruta);
                        Console.WriteLine("Saliendo del programa......");
                        break;
                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }
            } while (opcion != "4");
        }
             private static  string LeerOpcionMenu()
        {
            Console.WriteLine();
            Console.WriteLine("===REGISTROS DE GATOS===");
            Console.WriteLine("1. Agregar gasto:");
            Console.WriteLine("2. Listar gastos:");
            Console.WriteLine("3. Buscar por categoría:");
            Console.WriteLine("4. Guardar y salir:");
            Console.Write("Elije una opcion:");
            return Console.ReadLine() ?? "";
        }
        private static void AgregarGasto(List<Gastos> gastos, ref int id)
        {
            Console.Write("Descripcion:");
            string descripcion = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Monto:");
            bool montoOk = decimal.TryParse(Console.ReadLine(), out decimal monto);
            Console.Write("Categoria:");
            string categoria = Console.ReadLine()?.Trim() ?? "";
            if (!montoOk || monto <= 0 || string.IsNullOrWhiteSpace(descripcion) || string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }
            gastos.Add(new Gastos 
            { Id = id, Descripcion = descripcion, Monto = monto, Categoria = categoria 
            });
            id++;
            Console.WriteLine("Gasto agregado.");
        }
        private static void ListarGastos(List<Gastos> gastos)
        {
            if (gastos.Count == 0)
            {
                Console.WriteLine("No hay gastos registrados.");
                return;
            }
            decimal totalGastos = 0;

            foreach (Gastos g in gastos)
            {
                Console.WriteLine(g);
                totalGastos += g.Monto;
            }
            Console.WriteLine($"TOTAL GASTADO: Q {totalGastos:N2}");
        }
        private static void BuscarPorCategoria(List<Gastos> gastos)
        {
            Console.Write("Categoria a buscar:");
            string texto = Console.ReadLine()?.Trim().ToUpper() ?? "";
            bool encontrado = false;
            foreach (Gastos g in gastos)
            {
                if (g.Categoria.ToUpper().Contains(texto))
                {
                    Console.WriteLine($"-{g}");
                    encontrado = true;
                }
            }
            if (!encontrado)
            {
                Console.WriteLine("Sin coincidencias.");

            }
        }
       private static void Guardar(List<Gastos>gastos, string ruta)
        {
            var lineas = new List<string>();
            foreach (Gastos g in gastos)
            {
                lineas.Add($"{g.Id};{g.Descripcion};{g.Monto};{g.Categoria}");
            }
            File.WriteAllLines(ruta, lineas);
            Console.WriteLine($"Gastos guardados en {ruta}({gastos.Count} registros).");
        }
         private static List<Gastos> Cargar(string ruta)
        {
            var gastos = new List<Gastos>();
            if (!File.Exists(ruta))
            {
                return gastos;
            }
            foreach (string linea in File.ReadAllLines(ruta))
            {
                string[] campos = linea.Split(";");
                if (campos.Length != 4) continue;
                gastos.Add(new Gastos
                {
                    Id = int.Parse(campos[0]),
                    Descripcion = campos[1],
                    Monto = decimal.Parse(campos[2]),
                    Categoria = campos[3] 
                });
            }
            Console.WriteLine($"Cargados {gastos.Count} gastos desde {ruta}.");
            return gastos;
        }
    }
}
