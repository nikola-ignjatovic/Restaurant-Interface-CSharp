using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatTVP
{
    [Serializable]
    internal class DataClass
    {
        static string filePath = "data.pol";

        public List<Prilog> listaPriloga = new List<Prilog>();
        public List<Dodatak> listaDodataka = new List<Dodatak>();
        public List<Jelo> listaJela = new List<Jelo>();
        public List<Restoran> listaRestorana = new List<Restoran>();
        public List<Korisnik> listaKorisnika = new List<Korisnik>();
        public List<Rezervacija> listaRezervacija = new List<Rezervacija>();
        public List<PorucenoJelo> listaPorucenihJela = new List<PorucenoJelo>();


        // Uh moracu i da proverim dal je sve ovo kad se brise poslednje u nizu ili da default ne moze da se obrise da ne bi puklo
        static Korisnik klijentDefault = new Korisnik(2, "Nikola", "Ignjatovic", "NikolaKorisnik", "tvpNikola", "client");
        static Korisnik klijentDefault2 = new Korisnik(3, "Tamara", "Milic", "TamaraKorisnik", "tvpTamara", "client");
        static Korisnik adminDefault = new Korisnik(1, "Nikola", "Ignjatovic", "NikolaAdmin", "tvpNikola", "admin");

        static Prilog prilogDefault = new Prilog(1, "kecap", 20.0);
        static Prilog prilogDefault2 = new Prilog(2, "majonez", 30.0);
        static Dodatak dodatakDefault = new Dodatak(1, "pomfrit", 20.0, 200);
        static Dodatak dodatakDefault2 = new Dodatak(2, "cips", 30.0, 200);

        static Restoran restoranDefault = new Restoran(1, "Bela reka", "Blokovi", "+381643703085");
        static Restoran restoranDefault2 = new Restoran(2, "Frans", "Frans", "+381643703085");
        static int[] nizDodataka = { 1 };
        static Jelo jeloDefault = new Jelo(1, "piletina", 300, "proteinski has", 300, 1, 1, nizDodataka, false);
        static Jelo jeloDefault2 = new Jelo(2, "Sarma", 400, "masan has", 400, 2, 2, nizDodataka, true);
        static int[] porucenaJela = { 1 };
        static int[] porucenaJela2 = { 2 };
        static Rezervacija rezervacijaDefault = new Rezervacija(1, 2, "1", 340.0, porucenaJela, DateTime.Now);
        static Rezervacija rezervacijaDefault2 = new Rezervacija(2, 3, "2", 340.0, porucenaJela, DateTime.Now);
        static Rezervacija jucerasnjaRezervacija = new Rezervacija(3, 2, "3", 340.0, porucenaJela, DateTime.Today.AddDays(-1));
        static Rezervacija preJucerasnjaRezervacija = new Rezervacija(4, 2, "4", 450.0, porucenaJela2, DateTime.Today.AddDays(-2));

        static PorucenoJelo porucenoJeloDefault = new PorucenoJelo(1, "piletina", 300, "proteinski has", 300, 1, 1, nizDodataka);
        static PorucenoJelo porucenoJeloDefault2 = new PorucenoJelo(2, "Sarma", 400, "masan has", 400, 2, 2, nizDodataka);

        static Prilog prazanPrilog = new Prilog(0, "Bez Priloga", 0);
        static Dodatak prazanDodatak = new Dodatak(0, "Bez Dodataka", 0, 0);
        static Restoran prazanRestoran = new Restoran(0, "", "", "");
        static int[] prazanNiz = { 0 };
        static Jelo praznoJelo = new Jelo(0, "", 0, "", 0, 0, 0, prazanNiz, false);
        static PorucenoJelo praznoPorucenoJelo = new PorucenoJelo(0, "", 0, "", 0, 0, 0, prazanNiz);
        static int[] praznaPorucenaJela = { 0 };
        static Rezervacija praznaRezervacija = new Rezervacija(0, 0, "", 0, praznaPorucenaJela, DateTime.Now);

        static Korisnik prazanKorisnik = new Korisnik(0, "", "", "PrazanKorisnik", "tvpPrazan", "admin");

        [NonSerialized]
        public Korisnik aktivanKorisnik;

        public DataClass()
        {
            checkIfFileIsCreatedAndIfNotAssignDefaultValues();
        }

        public void checkIfFileIsCreatedAndIfNotAssignDefaultValues()
        {
            if (!File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();

                // Adding empty stuff
                listaPriloga.Add(prazanPrilog);
                listaDodataka.Add(prazanDodatak);
                listaRestorana.Add(prazanRestoran);
                listaKorisnika.Add(prazanKorisnik);
                listaJela.Add(praznoJelo);
                listaPorucenihJela.Add(praznoPorucenoJelo);
                listaRezervacija.Add(praznaRezervacija);


                listaKorisnika.Add(klijentDefault);
                listaKorisnika.Add(klijentDefault2);
                listaKorisnika.Add(adminDefault);

                listaPriloga.Add(prilogDefault);
                listaPriloga.Add(prilogDefault2);
                listaDodataka.Add(dodatakDefault);
                listaDodataka.Add(dodatakDefault2);
                listaRestorana.Add(restoranDefault);
                listaRestorana.Add(restoranDefault2);
                listaJela.Add(jeloDefault);
                listaJela.Add(jeloDefault2);
                listaRezervacija.Add(rezervacijaDefault);
                listaRezervacija.Add(rezervacijaDefault2);
                listaRezervacija.Add(jucerasnjaRezervacija);
                listaRezervacija.Add(preJucerasnjaRezervacija);

                listaPorucenihJela.Add(porucenoJeloDefault);
                listaPorucenihJela.Add(porucenoJeloDefault2);

                bf.Serialize(fs, this);
                fs.Dispose();
            }
        }

        // Ovde takodje da uradim proveru da li postoji neki id koji vise ne postoji i da ga zamenim sa 0 da n bi dolazilo do errora
        public void saveFiles()
        {
            // Provera za jela
            for (int i = 0; i < listaJela.Count; i++)
            {
                Jelo j = listaJela[i];
                if (Program.dataClass.listaPriloga.FirstOrDefault(idPriloga => idPriloga.Id == j.IdPrilog) == null)
                {
                    listaJela[i] = new Jelo(j.Id, j.Naziv, j.Gramaza, j.Opis, j.Cena, 0, j.IdRestoran, j.Dodaci, j.ObavezanPrilog);
                }
                if (Program.dataClass.listaRestorana.FirstOrDefault(idRestorana => idRestorana.Id == j.IdRestoran) == null)
                {
                    listaJela[i] = new Jelo(j.Id, j.Naziv, j.Gramaza, j.Opis, j.Cena, j.IdPrilog, 0, j.Dodaci, j.ObavezanPrilog);
                }

                foreach (int x in j.Dodaci)
                {
                    if (Program.dataClass.listaDodataka.FirstOrDefault(idDodataka => idDodataka.Id == x) == null)
                    {
                        listaJela[i] = new Jelo(j.Id, j.Naziv, j.Gramaza, j.Opis, j.Cena, j.IdPrilog, j.IdRestoran, prazanNiz, j.ObavezanPrilog);
                        break;
                    }
                }

            }

            // Provera za Porucena jela
            for (int i = 0; i < listaPorucenihJela.Count; i++)
            {
                PorucenoJelo j = listaPorucenihJela[i];
                if (Program.dataClass.listaPriloga.FirstOrDefault(idPriloga => idPriloga.Id == j.IdPrilog) == null)
                {
                    listaPorucenihJela[i] = new PorucenoJelo(j.Id, j.Naziv, j.Gramaza, j.Opis, j.Cena, 0, j.IdRestoran, j.Dodaci);
                }
                if (Program.dataClass.listaRestorana.FirstOrDefault(idRestorana => idRestorana.Id == j.IdRestoran) == null)
                {
                    listaPorucenihJela[i] = new PorucenoJelo(j.Id, j.Naziv, j.Gramaza, j.Opis, j.Cena, j.IdPrilog, 0, j.Dodaci);
                }

                foreach (int x in j.Dodaci)
                {
                    if (Program.dataClass.listaDodataka.FirstOrDefault(idDodataka => idDodataka.Id == x) == null)
                    {
                        listaPorucenihJela[i] = new PorucenoJelo(j.Id, j.Naziv, j.Gramaza, j.Opis, j.Cena, j.IdPrilog, j.IdRestoran, prazanNiz);
                        break;
                    }
                }

            }

            // Provera za rezervacije
            for (int i = 0; i < listaRezervacija.Count; i++)
            {
                Rezervacija r = listaRezervacija[i];
                if (Program.dataClass.listaKorisnika.FirstOrDefault(idKorisnika => idKorisnika.Id == r.IdKorisnika) == null)
                {
                    listaRezervacija[i] = new Rezervacija(r.Id, 0, r.Sifra, r.UkupnaCena, r.PorucenaJela, r.Date);
                }
                foreach (int porucenoJeloId in r.PorucenaJela)
                {
                    if (Program.dataClass.listaPorucenihJela.FirstOrDefault(idPorucenogJela => idPorucenogJela.Id == porucenoJeloId) == null)
                    {
                        listaRezervacija[i] = new Rezervacija(r.Id, r.IdKorisnika, r.Sifra, r.UkupnaCena, praznaPorucenaJela, r.Date);
                        break;
                    }
                }
            }

            FileStream fs = new FileStream(filePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, this);
            fs.Dispose();
        }

        public void readFiles()
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            DataClass data = (DataClass)bf.Deserialize(fs);

            this.listaKorisnika = data.listaKorisnika;
            this.listaPriloga = data.listaPriloga;
            this.listaDodataka = data.listaDodataka;
            this.listaRestorana = data.listaRestorana;
            this.listaJela = data.listaJela;
            this.listaRezervacija = data.listaRezervacija;
            this.listaPorucenihJela = data.listaPorucenihJela;
            fs.Dispose();
        }
    }
}
