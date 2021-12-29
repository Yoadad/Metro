using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Metro.Models;

namespace Metro
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Path.IsPathFullyQualified(args[0]))
            {
                var path = args[0];
                if (File.Exists(path))
                {
                    var fileContent = File.ReadAllText(path);
                    var metro = new MetroManager(fileContent);
                    if (metro.IsValidNetwork())
                    {
                        var routes = metro.GetShortestRoutes();
                        foreach (List<Station> route in routes)
                        {
                            Console
                            .WriteLine(
                            $"Resultado (Result): {string.Join("->", route.Select(s => s.Name))}");
                        }
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("La red no es valida (The network is not valid)");
                    }
                }
                else
                {
                    Console.WriteLine("El archivo no existe (The file does not exists)");
                }
            }
            else
            {
                Console.WriteLine("La ruta del archivo es incorrecta (The file path is not correct)");
            }
        }
    }
}
