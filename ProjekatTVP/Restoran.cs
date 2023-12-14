using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTVP
{
    [Serializable]
    internal class Restoran
    {
        private int id;
        private string naziv;
        private string adresa;
        private string kontaktTelefon;

        public Restoran(int id, string naziv, string adresa, string kontaktTelefon)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.Adresa = adresa;
            this.KontaktTelefon = kontaktTelefon;
        }

        public int Id { get => id; set => id = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public string Adresa { get => adresa; set => adresa = value; }
        public string KontaktTelefon { get => kontaktTelefon; set => kontaktTelefon = value; }

        public override string ToString()
        {
            return " ID: " + id.ToString() + "\n" + " Naziv: " + naziv + "\n" + " Adresa: " + adresa + "\n" + " Kontakt Telefon: " + KontaktTelefon;
        }
    }
}
