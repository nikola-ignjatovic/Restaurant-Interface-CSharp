using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTVP
{
    [Serializable]
    internal class Jelo
    {
        private int id;
        private string naziv;
        private int gramaza;
        private string opis;
        private double cena;
        private int idPrilog;
        private int idRestoran;
        // ili ovako ili sa klasom nzm kako traze
        private int[] dodaci;
        private bool obavezanPrilog;

        public Jelo(int id, string naziv, int gramaza, string opis, double cena, int idPrilog, int idRestoran, int[] dodaci, bool obavezanPrilog)
        {
            this.Id = id;
            this.Naziv = naziv;
            this.Gramaza = gramaza;
            this.Opis = opis;
            this.IdPrilog = idPrilog;
            this.IdRestoran = idRestoran;
            this.Dodaci = dodaci;
            this.ObavezanPrilog = obavezanPrilog;
            this.Cena = cena;
        }

        public int Id { get => id; set => id = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public int Gramaza { get => gramaza; set => gramaza = value; }
        public string Opis { get => opis; set => opis = value; }
        public int IdPrilog { get => idPrilog; set => idPrilog = value; }
        public int IdRestoran { get => idRestoran; set => idRestoran = value; }
        public int[] Dodaci { get => dodaci; set => dodaci = value; }
        public bool ObavezanPrilog { get => obavezanPrilog; set => obavezanPrilog = value; }
        public double Cena { get => cena; set => cena = value; }

        public override string ToString()
        {
            string obavezanPrilogText = "Ne";
            if (obavezanPrilog)
            {
                obavezanPrilogText = "Da";
            }
            string dodaciText = "";
            for (int i = 0; i < dodaci.Length; i++)
            {
                dodaciText += dodaci[i].ToString() + " ";
            }

            return " ID: " + id.ToString() + "\n" + " Naziv: " + naziv + "\n" + " Gramaza: " + gramaza.ToString() + "\n" + " Opis: " + opis + "\n" + " Cena: " + cena.ToString() + "\n" + " ID Priloga: " + IdPrilog.ToString() + "\n" + " ID Restorana: " + idRestoran.ToString() + "\n" + " ID-evi dodataka: " + dodaciText + "\n" + " Obavezan prilog: " + obavezanPrilogText;
        }
        public string klijentToString()
        {
            string obavezanPrilogText = "Ne";
            if (obavezanPrilog)
            {
                obavezanPrilogText = "Da";
            }
            string dodaciText = "";
            for (int i = 0; i < dodaci.Length; i++)
            {
                dodaciText += dodaci[i].ToString() + " ";
            }

            return " Naziv: " + naziv + "\n" + " Gramaza: " + gramaza.ToString() + "\n" + " Opis: " + opis + "\n" + " Cena: " + cena.ToString() + "\n" + " Obavezan prilog: " + obavezanPrilogText;
        }

    }
}
