using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTVP
{
    [Serializable]
    internal class Dodatak
    {
        private int id;
        private string nazivDodatka;
        private double cena;
        private int gramaza;

        public Dodatak(int id, string nazivDodatka, double cena, int gramaza)
        {
            this.Id = id;
            this.NazivDodatka = nazivDodatka;
            this.Cena = cena;
            this.Gramaza = gramaza;
        }

        public int Id { get => id; set => id = value; }
        public string NazivDodatka { get => nazivDodatka; set => nazivDodatka = value; }
        public double Cena { get => cena; set => cena = value; }
        public int Gramaza { get => gramaza; set => gramaza = value; }

        public override string ToString()
        {
            return " ID: " + id.ToString() + "\n" + " Naziv Dodatka: " + nazivDodatka + "\n" + " Cena dodatka: " + cena.ToString() + "\n" + " Gramaza: " + gramaza.ToString();
        }
        public string klijentToString()
        {
            return " Naziv Dodatka: " + nazivDodatka + "\n" + "  Cena dodatka: " + cena.ToString() + "\n" + "  Gramaza: " + gramaza.ToString();
        }
    }
}
