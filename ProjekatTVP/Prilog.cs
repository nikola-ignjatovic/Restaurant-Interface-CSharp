using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTVP
{
    [Serializable]
    internal class Prilog
    {
        private int id;
        private string nazivPriloga;
        private double cena;

        public Prilog(int prilog, string nazivPriloga, double cena)
        {
            this.Id = prilog;
            this.NazivPriloga = nazivPriloga;
            this.Cena = cena;
        }

        public int Id { get => id; set => id = value; }
        public string NazivPriloga { get => nazivPriloga; set => nazivPriloga = value; }
        public double Cena { get => cena; set => cena = value; }

        public override string ToString()
        {
            return " ID: " + id.ToString() + "\n" + " Naziv Priloga: " + nazivPriloga + "\n" + " Cena priloga: " + cena.ToString();
        }
        public string klijentToString()
        {
            return " Naziv Priloga: " + nazivPriloga + "\n" + "  Cena priloga: " + cena.ToString();
        }
    }
}
