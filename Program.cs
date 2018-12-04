using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptProgram
{
    public enum AltEnhet
    {
        Gram,
        Kilo,
        Liter,
        Deciliter,
        Stycken,
        Nypa,
        Kopp,
        Kryddmått,
        Tesked,
        Matsked
    }

    class Program
    {
        static void Main(string[] args)
        {
            GöraSaker göraSaker = new GöraSaker();

            göraSaker.Start();
        }
    }
}
