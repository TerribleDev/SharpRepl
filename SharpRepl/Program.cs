using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.IO;
using System.Reflection;

namespace SharpRepl
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var line in banner)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
            var state = CSharpScript.RunAsync("2+2").Result; //prime the engine
            while (true)
            {
                try
                {
                    Console.Write('>');
                    var input = Console.ReadLine();
                    state = state.ContinueWithAsync(input).Result;
                    if (state.ReturnValue != null && !string.IsNullOrWhiteSpace(state.ReturnValue.ToString()))
                    {
                        Console.WriteLine(state.ReturnValue);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static string[] banner = File.ReadAllLines(Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName, "Banner.txt"));
    }
}
