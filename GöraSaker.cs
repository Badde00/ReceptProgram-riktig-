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
        List<List<string>> ReceptSamling;
        const string INGRIDIENS_SLUT = "%&#$Ingredien$SlU7#&%";
        const string RECEPT_SLUT = "><Recept$1ut<>";

        public void Start()
        {
            Receptlista = new List<Recept>();
            FilTillReceptlista(); //Läser in tidigare recept

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
                    ReceptlistaTillStringlista(); //Sparar efter varje nytt recept och skriver över tidigare fil då jag redan läst in hela vid programmets start.
                    StringlistaTillFil();
                }
                else
                {
                    Console.WriteLine("Var god skriv L eller S beroende på vad du vill göra");
                }
            }
        }

        private void ReceptlistaTillStringlista() //Kommer att läsa in det i ett .txt doc sen, men vill inte ha en lång string, utan mer organisterat
        {
            ReceptSamling = new List<List<string>>(); //Gör en lista med listor. De inre listorna kommer att innehålla recepten.

            for (int i = 0; i < Receptlista.Count(); i++) //Gör egenom hela receptet där i är vilket recept jag är på
            {
                ReceptSamling.Add(new List<string>()); //Lägger in det i nya string listan
                ReceptSamling[i].Add(Receptlista[i].Namn); //Skriver in namnet först | Båda har i då det första receptet i Receptsamling ska vara det försrta receptet i Receptlista osv.

                for (int a = 0; a < Receptlista[i].Ingredienser.Count(); a++) //Lägger in varje ingrediens. Kommer att vara 3 punkter per ingrediens. mått enhet och vara. 1 per rad. a är vilken ingrediens
                {
                    ReceptSamling[i].Add(Receptlista[i].Ingredienser[a].Mått.ToString()); //Lägger in mått i listan

                    ReceptSamling[i].Add(Receptlista[i].Ingredienser[a].MåttEnhet);

                    ReceptSamling[i].Add(Receptlista[i].Ingredienser[a].Vara);
                }

                ReceptSamling[i].Add(INGRIDIENS_SLUT); //Så inläsningsprogrammet ska veta när den ska sluta läsa ingredienser och börja läsa instruktionerna. 
                //Har skrivit konstigt så ingen ska skriva det av misstag

                for (int b = 0; b < Receptlista[i].AttGöra.Count(); b++) //Repeterar för varje instruktion
                {
                    ReceptSamling[i].Add(Receptlista[i].AttGöra[b]);
                }

                ReceptSamling[i].Add(RECEPT_SLUT); //Så programmet vet att receptet är slut och ska börja på nästa
            }
        }

        private void StringlistaTillFil()//Läser in listan till en fil
        {
            StreamWriter sw = new StreamWriter("temp.txt");
            foreach(List<string> recept in ReceptSamling)
            {
                foreach(string item in recept)
                {
                    sw.WriteLine(item);
                }
            }
            sw.Close();
        }

        private void FilTillReceptlista()
        {
            StreamReader sr = new StreamReader(new FileStream("temp.txt", FileMode.OpenOrCreate, FileAccess.Read));
            string rad = "";

            //Kommer att göra variabler som placeholders för alla delar i ett recept
            string namn;
            List<Ingrediens> Ingredienser = new List<Ingrediens>();
            string vara;
            int mått;
            string måttEnhet;
            List<string> AttGöra = new List<string>();


            while ((rad = sr.ReadLine()) != null)
            {
                namn = rad;
                while ((rad = sr.ReadLine()) != INGRIDIENS_SLUT) //Kollar så att det itne är slutet på ingrediens-delen
                {
                    mått = int.Parse(rad); //Eftersom rad redan är inläst i while-delen så behövs det inte här
                    måttEnhet = (rad = sr.ReadLine());
                    vara = (rad = sr.ReadLine());
                    Ingredienser.Add(new Ingrediens(vara, mått, måttEnhet));
                }
               

                while((rad = sr.ReadLine()) != RECEPT_SLUT) //Läser in nästa del varje varv och kollar att receptet inte är slut
                {
                    AttGöra.Add(rad);
                }

                Receptlista.Add(new Recept(AttGöra, Ingredienser, namn));

                AttGöra = new List<string>();
                Ingredienser = new List<Ingrediens>();
            }


            sr.Close();
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
            List<Ingrediens> iLista = new List<Ingrediens>();

            while (true)
            {
                //Skriva hur mycket
                mängd = Mängd();
                enhet = Enhet();
                ingrediens = Ingrediens();

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
