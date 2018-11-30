using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceptProgram
{
    class Ingrediens
    {
        private string vara;
        private int mått;
        private string måttEnhet;

        public string Vara
        {
            get { return vara; }
        }

        public int Mått
        {
            get { return mått; }
        }

        public string MåttEnhet
        {
            get { return måttEnhet; }
        }

        public Ingrediens(string v, int m, string mE)
        {
            vara = v;
            mått = m;
            måttEnhet = mE;
        }
    }
}
