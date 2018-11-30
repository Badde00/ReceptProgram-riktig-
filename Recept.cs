using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptProgram
{
    class Recept
    {
        private List<string> attGöra = new List<string>();
        private List<Ingrediens> ingredienser = new List<Ingrediens>();
        private string namn;

        public List<string> AttGöra
        {
            get { return attGöra; }
        }

        public List<Ingrediens> Ingredienser
        {
            get { return ingredienser; }
        }

        public string Namn
        {
            get { return namn; }
        }


        public Recept(List<string> aG, List<Ingrediens> i, string n)
        {
            attGöra = aG;
            ingredienser = i;
            namn = n;
        }
    }
}
