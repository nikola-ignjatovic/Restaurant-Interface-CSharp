using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTVP
{
    [Serializable]
    internal class Rezervacija
    {
        private int id;
        private int idKorisnika;
        private string sifra;
        private double ukupnaCena;
        // Ili ovako ili sa klasom nzm sta traze ovde ide id od PorucenihJela
        private int[] porucenaJela;
        private DateTime date;

        public Rezervacija(int id, int idKorisnika, string sifra, double ukupnaCena, int[] porucenaJela, DateTime date)
        {
            this.Id = id;
            this.IdKorisnika = idKorisnika;
            this.Sifra = sifra;
            this.UkupnaCena = ukupnaCena;
            this.PorucenaJela = porucenaJela;
            this.date = date;
        }

        public int Id { get => id; set => id = value; }
        public int IdKorisnika { get => idKorisnika; set => idKorisnika = value; }
        public string Sifra { get => sifra; set => sifra = value; }
        public double UkupnaCena { get => ukupnaCena; set => ukupnaCena = value; }
        public int[] PorucenaJela { get => porucenaJela; set => porucenaJela = value; }

        public DateTime Date { get => date; set => date = value; }
        public override string ToString()
        {
            string porucenaJelaText = "";
            for (int i = 0; i < porucenaJela.Length; i++)
            {
                porucenaJelaText += porucenaJela[i].ToString() + " ";
            }
            return " ID: " + id.ToString() + "\n" + " ID Korisnika: " + idKorisnika.ToString() + "\n" + " sifra: " + sifra + "\n" + " Ukupna Cena: " + ukupnaCena.ToString() + "\n" + " ID-evi Porucenih Jela: " + porucenaJelaText + "\n" + " Datum porucivanja: " + date.ToString();
        }
        // Ovaj se koristi da se sakriju ID-evi od usera
        public string ToStringKorisnik()
        {
            // Ovde cemo bas imena jela ispisati 
            string porucenaJelaText = "";
            for (int i = 0; i < porucenaJela.Length; i++)
            {
                porucenaJelaText += Program.dataClass.listaPorucenihJela.FirstOrDefault(jelo => jelo.Id == porucenaJela[i]).Naziv + " ";
            }

            return " sifra: " + sifra + "\n" + " Ukupna Cena: " + ukupnaCena.ToString() + "\n" + " Porucena Jela: " + porucenaJelaText + "\n" + " Datum porucivanja: " + date.ToString();
        }
    }
}
