using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AB.TsGen.Extensions;

namespace AB.TsGen
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = args[0];
            var outPath = args[1];
            Console.WriteLine($"assembly : {assembly}");
            Console.WriteLine($"out path : {outPath}");
            var dtos = Load(assembly).GetTypes()
                .Where(NotAbstract)
                .Where(IsClass)
                .Where(EndsWithDto);

            var ts = new StringBuilder();
            foreach (var dto in dtos)
            {
                ts.Append($"export class {dto.Name}\r\n");
                ts.Append("{\r\n");

                foreach (var property in dto.GetProperties())
                {
                    ts.Append($"\tpublic {property.Name}: {property.AsTypeScriptType()};\r\n");
                }
                ts.Append("}\r\n");
                ts.Append("\r\n");
            }
            
            File.WriteAllText(outPath, ts.ToString());

            Console.WriteLine("done :D");
        }

        private static Assembly Load(string assemblyPath)
        {
            return Assembly.LoadFrom(assemblyPath);
        }

        private static bool EndsWithDto(Type type)
        {
            return type.Name.EndsWith("Dto");
        }

        private static bool IsClass(Type type)
        {
            return type.IsClass;
        }

        private static bool NotAbstract(Type type)
        {
            return type.IsAbstract == false;
        }
    }
}
