using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTVP
{
    [Serializable]
    internal class Korisnik
    {
        private int id;
        private string ime;
        private string prezime;
        private string korisnickoIme;
        private string lozinka;
        // Should see this or boolean
        private string vrstaKorisnika;

        public Korisnik(int id, string ime, string prezime, string korisnickoIme, string lozinka, string vrstaKorisnika)
        {
            this.Id = id;
            this.Ime = ime;
            this.Prezime = prezime;
            this.KorisnickoIme = korisnickoIme;
            this.Lozinka = lozinka;
            this.VrstaKorisnika = vrstaKorisnika;
        }

        public int Id { get => id; set => id = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string VrstaKorisnika { get => vrstaKorisnika; set => vrstaKorisnika = value; }

        public override string ToString()
        {
            string sifraZvezdice = "";
            for (int i = 0; i < lozinka.Length; i++)
            {
                sifraZvezdice += "*";
            }
            return " ID: " + id.ToString() + "\n" + " Ime: " + ime + "\n" + " Prezime: " + Prezime + "\n" + " Korisnicko ime: " + korisnickoIme + "\n" + " Lozinka: " + sifraZvezdice + "\n" + " Vrsta Korisnika: " + vrstaKorisnika;
        }
    }
}
