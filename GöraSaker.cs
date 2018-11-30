using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptProgram
{
    class GöraSaker
    {
        List<Recept> Receptlista;

        public void Start()
        {
            Receptlista = new List<Recept>();
            while (true)
            {
                Console.WriteLine("Vad vill du göra? (Läsa recept (L) / Skriva recept (S))");

                string s = Console.ReadLine();
                if (s == "L")
                {
                    LäsaRecept();
                }
                else if (s == "S")
                {
                    SkrivaRecept();
                }
                else
                {
                    Console.WriteLine("Var god skriv L eller S beroende på vad du vill göra");
                }
            }
        }

        private void LäsFiler()
        {
            BinaryReader br = new BinaryReader(new FileStream("C:/Users/Badde00/FilerProgrammering/Recept.hej", FileMode.OpenOrCreate, FileAccess.Read));
            

            br.Close();
        }

        private void ReceptTillFil()
        {
            StreamWriter sw = new StreamWriter("C:/Users/Badde00/FilerProgrammering/Recept.hej");
            foreach(Recept item in Receptlista)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }

        private void FilTillRecept()
        {
            StreamReader sr = new StreamReader("C:/Users/Badde00/FilerProgrammering/Recept.hej");
            Recept rad;

            while((rad.ToString() = sr.ReadLine()) != null)
            {
                Receptlista.Add(rad);
            }
        }

        private void LäsaRecept()
        {
            foreach(Recept item in Receptlista)
            {
                Console.WriteLine();
                Console.WriteLine(item.Namn);
                Console.WriteLine();
                Console.WriteLine("Ingredienser");
                foreach(Ingrediens i in item.Ingredienser)
                {
                    Console.WriteLine(i.Mått + " " + i.MåttEnhet + " " + i.Vara);
                }
                Console.WriteLine();
                Console.WriteLine("Steg");
                foreach(string i in item.AttGöra)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine();
            }
        }

        private void SkrivaRecept()
        {
            TextSkrivaRecept();
            List<Ingrediens> iLista = SvarSkrivaRecept();
            List<string> gLista = AttGöraLista();
            string namn = Namn();
            Receptlista.Add(new Recept(gLista, iLista, namn));
        }

        private void TextSkrivaRecept()
        {
            Console.WriteLine("Vilka ingredienser vill du ha? Vänligen skriv i ordningen [hur mycket], [mått-enhet] och sedan [ingrediens].");
            Console.WriteLine("Tryck på enter mellan varje.");
        }

        private string Namn()
        {
            Console.WriteLine("Vad heter receptet?");
            return Console.ReadLine();
        }

        private List<Ingrediens> SvarSkrivaRecept()
        {
            int mängd;
            int enhet;
            string ingrediens;


            while (true)
            {
                //Skriva hur mycket
                mängd = Mängd();
                enhet = Enhet();
                ingrediens = Ingrediens();
                List<Ingrediens> iLista = new List<Ingrediens>();

                iLista.Add(new Ingrediens(ingrediens, mängd, Enum.GetName(typeof(AltEnhet), enhet)));

                Console.WriteLine("Vill du lägga till ännu en ingrediens? Skriv då Ja");
                string s = Console.ReadLine();
                if (!(s == "Ja" || s == "ja"))
                {
                    return iLista;
                }
            }
        }

        private List<string> AttGöraLista()
        {
            List<string> temp = new List<string>();
            for (int i = 1; ; i++)
            {
                Console.WriteLine("Skriv steg " + i + ". Om du är klar skriver du (slut)");
                string steg = Console.ReadLine();
                if (steg == "slut")
                {
                    return temp;
                }
                temp.Add(steg);
            }
        }

        private int Mängd()
        {
            while (true)
            {
                Console.WriteLine("Vilken mängd av din ingrediens ska det vara? Inte enheten den mäts i.");
                if (int.TryParse(Console.ReadLine(), out int i))
                {
                    return i;
                }
            }
        }

        private int Enhet() //Använder enums för att testa hur de fungerar
        {
            int temp;

            Console.WriteLine("Välj enhet (via respektive nummer) av alternativen nedan");
            foreach (AltEnhet altEnhet in Enum.GetValues(typeof(AltEnhet)))
            {
                Console.WriteLine(altEnhet + " = " + (int)altEnhet);
            }

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out temp))
                {
                    return temp;
                }
                else
                {
                    Console.WriteLine("Det var inte ett giltigt svar. Skriv nummret kopplat till den rätta enheten.");
                }
            }
        }

        private string Ingrediens()
        {
            Console.WriteLine("Vilken ingrediens är det?");

            return Console.ReadLine();
        }

        public GöraSaker()
        {

        }
    }
}
