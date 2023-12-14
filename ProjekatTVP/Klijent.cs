using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using ComboBox = System.Windows.Forms.ComboBox;

namespace ProjekatTVP
{
    public partial class Klijent : Form
    {

        int maksimalanBrojPriloga = 3;
        static Form1 loginForma = new Form1();
        ComboBox comboBox = new ComboBox();
        CheckedListBox checkedListBox = new CheckedListBox();
        ComboBox comboBoxTodayOrders = new ComboBox();
        ComboBox jeloPicker = new ComboBox();
        Label labelIznadJela = new Label();
        Label labelObrisiRezervaciju = new Label();
        TextBox textBoxObrisiRezervaciju = new TextBox();

        Label labelOdaberitePriloge = new Label();
        Label labelOdaberiteDodatke = new Label();
        Label labelObavezanPrilog = new Label();
        Label iznadTodayOrdersa = new Label();
        Label labelDatum = new Label();

        int[] checkBoxesIds = new int[300];
        int[] radioButtonsIds = new int[300];

        List<CheckBox> listOfCheckBoxesDodaci = new List<CheckBox>();
        List<RadioButton> listOfRadioButtonsPrilozi = new List<RadioButton>();

        private int[] idEviMogucihJela;
        private int idOdabranogJela;
        int kolikoCekiranih = 0;
        double ukupnaCena = 0;

        Random random2 = new Random();
        Random random3 = new Random();
        int randomNumber2;
        int randomNumber3;

        TrackBar trackbarDateFilter = new TrackBar();

        DateTime selectedDate = DateTime.Now;
        DateTime minimumDate;
        DateTime maximumDate;

        public Klijent()
        {
            this.Height += 100;
            randomNumber2 = random2.Next(2, 40001); // generates a random integer between 2 and 40000 (exclusive of 40000)
                                                    // Sad moram da proverim da li taj id vec postoji i ako postoji da opet generisem random broj
            while (Program.dataClass.listaRezervacija.FirstOrDefault(idPorucenog => idPorucenog.Id == randomNumber2) != null)
            {
                randomNumber2 = random2.Next(2, 40001);
            }

            randomNumber3 = random2.Next(2, 40001); // generates a random integer between 2 and 40000 (exclusive of 40000)
                                                    // Sad moram da proverim da li taj id vec postoji i ako postoji da opet generisem random broj
            while (Program.dataClass.listaRezervacija.FirstOrDefault(idPorucenog => idPorucenog.Sifra == randomNumber3.ToString()) != null)
            {
                randomNumber3 = random3.Next(2, 40001);
            }

            InitializeComponent();

            comboBox.Location = new Point(50, 50);
            comboBox.Width = 200;
            comboBox.SelectedIndexChanged += new EventHandler(restoranPicked);
            Label iznadRestorana = new Label();
            iznadRestorana.Location = new Point(comboBox.Location.X, comboBox.Location.Y - 20);
            iznadRestorana.Text = "Izaberite restoran";


            jeloPicker.Location = new Point(50, 150);
            jeloPicker.Width = 200;
            jeloPicker.Height = 25;
            jeloPicker.SelectedIndexChanged += new EventHandler(jeloPicked);
            labelIznadJela.Location = new Point(jeloPicker.Location.X, jeloPicker.Location.Y - 20);
            labelIznadJela.Size = new Size(200, 50);
            labelIznadJela.Text = "Odaberite jelo iz restorana";
            hideJeloPicker();


            labelOdaberitePriloge.Text = "Odaberite priloge:";
            labelOdaberitePriloge.Size = new Size(100, 25);
            labelOdaberitePriloge.Location = new Point(25, 180);
            labelObavezanPrilog.Size = new Size(110, 25);
            labelObavezanPrilog.Location = new Point(labelOdaberitePriloge.Location.X + labelOdaberitePriloge.Width + 10, labelOdaberitePriloge.Location.Y);
            labelObavezanPrilog.Text = "Obavezan prilog: ";
            labelOdaberiteDodatke.Text = "Odaberite dodatke:";
            labelOdaberiteDodatke.Size = new Size(150, 25);
            labelOdaberiteDodatke.Location = new Point(labelObavezanPrilog.Location.X + labelObavezanPrilog.Width + 20, labelOdaberitePriloge.Location.Y);
            hidePrilogAndDodatakPiker();

            // Ovde dodajem funkciju koja se zove kad se checkira i stavljam checkonclick na true da bi radilo lepo
            checkedListBox.Location = new Point(450, 50);
            checkedListBox.Width = 250;
            checkedListBox.CheckOnClick = true;
            checkedListBox.ItemCheck += checkedListBox1_ItemCheck;

            Label iznadCheckedListBox = new Label();
            iznadCheckedListBox.Location = new Point(checkedListBox.Location.X, checkedListBox.Location.Y - 20);
            iznadCheckedListBox.Text = "Filtriraj po naizvu jela";


            comboBoxTodayOrders.Location = new Point(450, 250);
            comboBoxTodayOrders.Size = new Size(300, 50);
            comboBoxTodayOrders.SelectedIndexChanged += new EventHandler(todayOrderJeloPicked);
            iznadTodayOrdersa.Size = new Size(400, 50);
            iznadTodayOrdersa.Location = new Point(comboBoxTodayOrders.Location.X - 30, comboBoxTodayOrders.Location.Y - 20);
            iznadTodayOrdersa.Text = "Lista porudzbina za zadati datum. Pritisni na porudzbinu za vise informacija";


            textBoxObrisiRezervaciju.Location = new Point(450, 350);
            textBoxObrisiRezervaciju.Size = new Size(200, 50);
            labelObrisiRezervaciju.Location = new Point(textBoxObrisiRezervaciju.Location.X, textBoxObrisiRezervaciju.Location.Y - 20);
            labelObrisiRezervaciju.Size = new Size(400, 50);
            labelObrisiRezervaciju.Text = "Unesi sifru rezervacije koju zelis da obrises";

            Button obrisiButton = new Button();
            obrisiButton.Text = "Obrisi";
            obrisiButton.Width = 70;
            obrisiButton.Location = new Point(textBoxObrisiRezervaciju.Location.X + textBoxObrisiRezervaciju.Size.Width + 50, textBoxObrisiRezervaciju.Location.Y);
            obrisiButton.Click += obrisiRezervaciju;
            this.Controls.Add(obrisiButton);

            this.Controls.Add(comboBox);
            this.Controls.Add(checkedListBox);
            this.Controls.Add(iznadRestorana);
            this.Controls.Add(iznadCheckedListBox);
            this.Controls.Add(comboBoxTodayOrders);
            this.Controls.Add(iznadTodayOrdersa);
            this.Controls.Add(jeloPicker);
            this.Controls.Add(labelIznadJela);
            this.Controls.Add(textBoxObrisiRezervaciju);
            this.Controls.Add(labelObrisiRezervaciju);
            this.Controls.Add(labelOdaberitePriloge);
            this.Controls.Add(labelOdaberiteDodatke);
            this.Controls.Add(labelObavezanPrilog);

            addFoodsToCheckedListBox();
            addRestaurantsToComboBox(null);
            createButtons();
        }

        private void odrediMinIMaxDateISetujVrednosti()
        {
            List<Rezervacija> tempListaRezervacija = new List<Rezervacija>();
            tempListaRezervacija = Program.dataClass.listaRezervacija.Where(idKorisnika => idKorisnika.IdKorisnika == Program.dataClass.aktivanKorisnik.Id).ToList();
            trackbarDateFilter.Visible = true;
            if (tempListaRezervacija.Count <= 0)
            {
                trackbarDateFilter.Visible = false;
                trackbarDateFilter.Maximum = (int)(DateTime.Now - DateTime.MinValue).TotalDays;
                trackbarDateFilter.Minimum = (int)(DateTime.Now - DateTime.MinValue).TotalDays;
                return;
            }
            minimumDate = tempListaRezervacija[0].Date;
            maximumDate = tempListaRezervacija[0].Date;
            foreach (Rezervacija r in tempListaRezervacija)
            {
                if (r.Date < minimumDate)
                    minimumDate = r.Date;
                if (r.Date > maximumDate)
                {
                    maximumDate = r.Date;
                }
            }
            trackbarDateFilter.Minimum = (int)(minimumDate - DateTime.MinValue).TotalDays;
            // Treba mi if da proverim da li je maximum datum poslednje rezervacije ili danasnji datum ako je manji setujem max na danasnji date
            if ((int)(maximumDate - DateTime.MinValue).TotalDays < (int)(DateTime.Now - DateTime.MinValue).TotalDays)
            {
                trackbarDateFilter.Maximum = (int)(DateTime.Now - DateTime.MinValue).TotalDays;
            }
            else
            {
                trackbarDateFilter.Maximum = (int)(maximumDate - DateTime.MinValue).TotalDays;
            }

            // Moram ovo da ne bi breakovao da ga vratim na stari
            selectedDate = DateTime.Now;
            trackbarDateFilter.Value = (int)(selectedDate - DateTime.MinValue).TotalDays;
            labelDatum.Text = "Porucena jela za datum: " + selectedDate.Day.ToString() + "/" + selectedDate.Month.ToString() + "/" + selectedDate.Year.ToString();
        }

        private void trackbarValueChaneged(object sender, EventArgs e)
        {
            selectedDate = DateTime.MinValue.AddDays(trackbarDateFilter.Value);
            showTodayOrders();
            labelDatum.Text = "Porucena jela za datum: " + selectedDate.Day.ToString() + "/" + selectedDate.Month.ToString() + "/" + selectedDate.Year.ToString();
        }
        private void todayOrderJeloPicked(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (comboBoxTodayOrders.SelectedIndex >= 0)
                {

                    string rezervacijaString = comboBoxTodayOrders.SelectedItem.ToString();

                    // Trazim u stringu sifru: pa posle ove dve tacke
                    Regex regex = new Regex(@"sifra: (\S+)\s");
                    Match match = regex.Match(rezervacijaString);

                    double ukupnaCena = 0;

                    if (match.Success)
                    {
                        string sifra = match.Groups[1].Value;

                        // Sad ovde treba da nadjem rezervaciju sa tom sifrom
                        Rezervacija rezervacija = Program.dataClass.listaRezervacija.FirstOrDefault(sifraa => sifraa.Sifra == sifra);

                        List<PorucenoJelo> listaPorucenihJelaKlijent = new List<PorucenoJelo>();

                        // Sad treba da nadjem porucena jela u toj rezervaciji
                        foreach (int idPorucenihJela in rezervacija.PorucenaJela)
                        {
                            listaPorucenihJelaKlijent.Add(Program.dataClass.listaPorucenihJela.FirstOrDefault(idJela => idJela.Id == idPorucenihJela));
                        }

                        // Sada za svako poruceno jelo treba da ispisem sve podatke
                        // I cenu ovde dole racunam
                        string sveInformacije = "";
                        foreach (PorucenoJelo porucenoJelasce in listaPorucenihJelaKlijent)
                        {
                            if (porucenoJelasce == null)
                            {
                                MessageBox.Show("Nazalost jelo sa idem je obrisano zovite admina");
                                return;
                            }
                            ukupnaCena += porucenoJelasce.Cena;
                            if (Program.dataClass.listaPriloga.FirstOrDefault(p => p.Id == porucenoJelasce.IdPrilog) != null)
                            {
                                ukupnaCena += Program.dataClass.listaPriloga.FirstOrDefault(p => p.Id == porucenoJelasce.IdPrilog).Cena;
                            }
                            else
                            {
                                MessageBox.Show("Nazalost taj prilog na jelu vise ne postoji kontaktirajte admina");
                                return;
                            }
                            foreach (int idDodatka in porucenoJelasce.Dodaci)
                            {
                                if (Program.dataClass.listaDodataka.FirstOrDefault(d => d.Id == idDodatka) == null)
                                {
                                    MessageBox.Show("Nazalost dodatak na jelu je obrisan kontaktirajte admina");
                                    return;
                                }
                                ukupnaCena += Program.dataClass.listaDodataka.FirstOrDefault(d => d.Id == idDodatka).Cena;
                            }
                            if (sveInformacije.Contains(porucenoJelasce.klijentToString()))
                            {
                                Console.WriteLine(porucenoJelasce.klijentToString());
                                Console.WriteLine(porucenoJelasce.klijentToString());
                                int indexDelaKojiMiTreba = sveInformacije.IndexOf(porucenoJelasce.klijentToString());
                                int plusIndex = sveInformacije.IndexOf("x", indexDelaKojiMiTreba);
                                // Using temp k to extract everything after "x" from string turn it to number and then add +1 to it
                                int tempK = 0;
                                string numberString = sveInformacije.Substring(plusIndex + 1, 1);
                                tempK = int.Parse(numberString);
                                tempK += 1;
                                // Znaci mora da mi replaceuje samo taj odredjeni deo stringa
                                string tempString = sveInformacije.Substring(0, plusIndex + 1);
                                string saveString = "";
                                // Moram da proverim da li postoji jos neki string posle tog x1 da ga ne bih isekao
                                if (sveInformacije.Substring(plusIndex + 2) != null)
                                {
                                    saveString = sveInformacije.Substring(plusIndex + 2);
                                }
                                // asdasodkaosk +1 dpaoskodasodapos
                                sveInformacije = tempString + tempK.ToString() + saveString;

                            }
                            else
                            {
                                Console.WriteLine();
                                sveInformacije += porucenoJelasce.klijentToString() + "\n" + "\n" + " KOLICINA: " + "x1" + "\n" + "\n" + "\n";
                            }
                        }

                        sveInformacije += "\nUkupna cena: " + ukupnaCena.ToString();
                        MessageBox.Show("EVO TI SVE O JELU: \n\n" + sveInformacije);

                    }
                    else
                    {
                        MessageBox.Show("Doslo je do greske");
                    }
                }
            }));
        }
        private void restoranPicked(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex >= 0)
            {
                showJeloPicker();
            }
            else
            {
                hideJeloPicker();
            }
        }

        private void showJeloPicker()
        {
            jeloPicker.SelectedIndex = -1;
            jeloPicker.Items.Clear();
            jeloPicker.Visible = true;
            labelIznadJela.Visible = true;

            int idRestoranaZaKojiGledamJela = Program.dataClass.listaRestorana.FirstOrDefault(imeRestorana => imeRestorana.Naziv == comboBox.SelectedItem.ToString()).Id;

            idEviMogucihJela = Program.dataClass.listaJela.Where(i => i.IdRestoran == idRestoranaZaKojiGledamJela).Select(i => i.Id).ToArray();
            foreach (int jelo in idEviMogucihJela)
            {
                jeloPicker.Items.Add(Program.dataClass.listaJela.FirstOrDefault(idJela => idJela.Id == jelo).klijentToString());
            }
        }

        // Govori koje je jelo odabrano
        private void jeloPicked(object sender, EventArgs e)
        {
            // Govori koje je jelo odabrano
            if (jeloPicker.SelectedIndex >= 0)
            {
                idOdabranogJela = Program.dataClass.listaJela.FirstOrDefault(idJela => idJela.Id == idEviMogucihJela[jeloPicker.SelectedIndex]).Id;
                hidePrilogAndDodatakPiker();
                showPrilogAndDodatakPiker();
            }
            else
            {
                hidePrilogAndDodatakPiker();
            }
        }

        private void hideJeloPicker()
        {
            jeloPicker.SelectedIndex = -1;
            jeloPicker.Items.Clear();
            jeloPicker.Visible = false;
            labelIznadJela.Visible = false;
        }

        private void showPrilogAndDodatakPiker()
        {
            int yOffsetRadioButtons = 40;
            bool firstRadioButton = true;
            foreach (Prilog p in Program.dataClass.listaPriloga)
            {
                if (Program.dataClass.listaJela.FirstOrDefault(idJelasca => idJelasca.Id == idOdabranogJela).ObavezanPrilog)
                    if (firstRadioButton)
                    {
                        firstRadioButton = false;
                        continue;
                    }
                RadioButton radioButton = new RadioButton();
                radioButton.Width = 160;
                radioButton.Height = 40;
                radioButton.Text = p.klijentToString();
                radioButtonsIds[Program.dataClass.listaPriloga.IndexOf(p)] = p.Id;
                radioButton.Location = new Point(labelOdaberitePriloge.Location.X + 25, labelOdaberitePriloge.Location.Y - 20 + yOffsetRadioButtons);
                this.Controls.Add(radioButton);
                listOfRadioButtonsPrilozi.Add(radioButton);
                yOffsetRadioButtons += 40;
            }
            if (Program.dataClass.listaJela.FirstOrDefault(idJelasca => idJelasca.Id == idOdabranogJela).ObavezanPrilog)
            {
                labelObavezanPrilog.Text = "Obavezan prilog: da";
            }
            else
            {
                labelObavezanPrilog.Text = "Obavezan prilog: ne";
            }

            int yOffsetCheckBoxes = 60;
            List<Dodatak> tempListaDodataka = new List<Dodatak>();
            foreach (int idDodatka in Program.dataClass.listaJela.FirstOrDefault(idJelascaaa => idJelascaaa.Id == idOdabranogJela).Dodaci)
            {
                Dodatak tempDodatak = Program.dataClass.listaDodataka.FirstOrDefault(iddodatkaaa => iddodatkaaa.Id == idDodatka);
                if (tempDodatak != null)
                    tempListaDodataka.Add(tempDodatak);
                foreach (Dodatak d in tempListaDodataka)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Width = 160;
                    checkBox.Height = 60;
                    checkBox.Text = d.klijentToString();
                    checkBox.Location = new Point(labelOdaberiteDodatke.Location.X - 20, labelOdaberiteDodatke.Location.Y - 40 + yOffsetCheckBoxes);
                    checkBox.CheckedChanged += checkBoxesChange;
                    this.Controls.Add(checkBox);
                    listOfCheckBoxesDodaci.Add(checkBox);
                    yOffsetCheckBoxes += 40;
                    checkBoxesIds[Program.dataClass.listaDodataka.IndexOf(d)] = d.Id;
                }
            }



            labelOdaberitePriloge.Visible = true;
            labelObavezanPrilog.Visible = true;
            labelOdaberiteDodatke.Visible = true;

        }

        private void checkBoxesChange(object sender, EventArgs e)
        {
            kolikoCekiranih = 0;
            for (int i = 0; i < listOfCheckBoxesDodaci.Count; i++)
            {
                if (listOfCheckBoxesDodaci[i].Checked == true)
                {
                    kolikoCekiranih++;
                }
            }
        }

        private void hidePrilogAndDodatakPiker()
        {
            foreach (RadioButton r in listOfRadioButtonsPrilozi)
            {
                this.Controls.Remove(r);
            }
            listOfRadioButtonsPrilozi.Clear();
            foreach (CheckBox c in listOfCheckBoxesDodaci)
            {
                this.Controls.Remove(c);
            }
            listOfCheckBoxesDodaci.Clear();
            labelOdaberitePriloge.Visible = false;
            labelObavezanPrilog.Visible = false;
            labelOdaberiteDodatke.Visible = false;
        }

        // Ovde treba jos dodati da se samo za tog klijenta pokazuju narudzbine njegove
        private void showTodayOrders()
        {
            comboBoxTodayOrders.SelectedIndex = -1;
            comboBoxTodayOrders.Items.Clear();
            // Ovde se proverava da li postoje rezervacije za aktivnog korisnika i dodaju se u niz da bi se listale posle i takodje proverava i datum ali samo datum ne i vreme zato izgleda cudno i.Date.Date
            int[] idRezervacija = Program.dataClass.listaRezervacija.Where(i => i.IdKorisnika == Program.dataClass.aktivanKorisnik.Id && i.Date.Date == selectedDate.Date).Select(i => i.Id).ToArray();
            Console.WriteLine(selectedDate.ToString());
            Console.WriteLine(selectedDate.ToString());
            foreach (int idRezervacije in idRezervacija)
            {
                comboBoxTodayOrders.Items.Add(Program.dataClass.listaRezervacija.FirstOrDefault(l => l.Id == idRezervacije).ToStringKorisnik());
            }
        }

        private void createButtons()
        {
            int leftAndRightMargin = 30;
            int buttonWidth = 300;
            Button saveButton = new Button();
            saveButton.Text = "Poruci jelo";
            saveButton.Width = buttonWidth;
            saveButton.Location = new Point(leftAndRightMargin, this.Height - 100);
            saveButton.Click += saveButtonPressed;
            this.Controls.Add(saveButton);

            Button returnButton = new Button();
            returnButton.Text = "Povratak nazad";
            returnButton.Width = buttonWidth;
            returnButton.Location = new Point(this.Width - leftAndRightMargin - returnButton.Width, this.Height - 100);
            returnButton.Click += returnBackToAdmin;
            this.Controls.Add(returnButton);

            // Repositioning buttons
            this.Resize += new EventHandler((sender, e) => formResize(sender, e, saveButton, returnButton, leftAndRightMargin));
        }

        // Ne moram da proveravam koji je korisnik jer je sifra unique svakako al sta ako korisnik unese tudju sifru
        private void obrisiRezervaciju(object sender, EventArgs e)
        {
            if (textBoxObrisiRezervaciju.Text.Trim().Length != 0)
            {
                Rezervacija rezervacijaZaBrisanje = Program.dataClass.listaRezervacija.FirstOrDefault(sifraRezervacije => sifraRezervacije.Sifra == textBoxObrisiRezervaciju.Text);
                if (rezervacijaZaBrisanje != null)
                {
                    if (rezervacijaZaBrisanje.IdKorisnika == Program.dataClass.aktivanKorisnik.Id)
                    {
                        Program.dataClass.listaRezervacija.Remove(rezervacijaZaBrisanje);
                        Program.dataClass.saveFiles();
                        showTodayOrders();
                        odrediMinIMaxDateISetujVrednosti();
                        MessageBox.Show("Uspesno obrisana rezervacija");
                    }
                    else
                    {
                        MessageBox.Show("Pogresno ste uneli sifru :) ili mozda niste hehe");
                    }
                }
                else
                {
                    MessageBox.Show("Ne postoji rezervacija sa zadatom sifrom");
                }

            }
            else
            {
                MessageBox.Show("Unesite sifru rezervacije");
            }
        }

        private void returnBackToAdmin(object sender, EventArgs e)
        {
            loginForma.Show();
            this.Hide();
        }

        private void resetujSve()
        {

            hidePrilogAndDodatakPiker();
            hideJeloPicker();
            showTodayOrders();
            comboBox.SelectedIndex = 0;
            kolikoCekiranih = 0;
            Array.Clear(checkBoxesIds, 0, checkBoxesIds.Length);
            Array.Clear(radioButtonsIds, 0, radioButtonsIds.Length);
            odrediMinIMaxDateISetujVrednosti();
        }

        private void saveButtonPressed(object sender, EventArgs e)
        {
            // Dal je restoran odabran
            if (jeloPicker.Visible != false)
            {
                // Dal je jelo odabrano
                if (labelOdaberitePriloge.Visible != false)
                {
                    // Koliko cekiranih dodataka da nije vise od 3
                    if (kolikoCekiranih <= maksimalanBrojPriloga)
                    {
                        // Da li je stikliran obavezan prilog 
                        bool tempBool = false; // Temp bool true po defaultu ne mora nista da bude selektovano od priloga ako nije obavezan postaje false pa se proverava ako je obavezan prilog
                        foreach (RadioButton radioButton in listOfRadioButtonsPrilozi)
                        {
                            if (radioButton.Checked == true)
                            {
                                tempBool = true;
                            }
                        }
                        // Proverava da li je korisnik odabrao prilog ako mora da ga odabere
                        if (tempBool != false)
                        {
                            Random random = new Random();
                            int randomNumber = random.Next(2, 40001); // generates a random integer between 2 and 40000 (exclusive of 40000)
                            // Sad moram da proverim da li taj id vec postoji i ako postoji da opet generisem random broj
                            while (Program.dataClass.listaPorucenihJela.FirstOrDefault(idPorucenog => idPorucenog.Id == randomNumber) != null)
                            {
                                randomNumber = random.Next(2, 40001);
                            }
                            Jelo tempJelo = Program.dataClass.listaJela.FirstOrDefault(idJela => idJela.Id == idOdabranogJela);

                            Prilog tempPrilog = new Prilog(0, "Bez priloga", 0);

                            List<Dodatak> listaOdabranihDodataka = new List<Dodatak>();

                            bool jelBioIjedanDodatak = false;
                            foreach (CheckBox checkbox in listOfCheckBoxesDodaci)
                            {
                                if (checkbox.Checked == true)
                                {
                                    int indexCheckBoxa = listOfCheckBoxesDodaci.IndexOf(checkbox);
                                    Dodatak tempDodatak = Program.dataClass.listaDodataka.FirstOrDefault(idDodatka => idDodatka.Id == checkBoxesIds[indexCheckBoxa]);
                                    listaOdabranihDodataka.Add(tempDodatak);
                                    jelBioIjedanDodatak = true;
                                }
                            }
                            // Ako nije bio dodatak nijedan moram da dodam nesto u listu da ne pukne
                            if (jelBioIjedanDodatak == false)
                            {
                                listaOdabranihDodataka.Add(new Dodatak(0, "Nema dodataka", 0, 0));
                            }
                            int[] idEviDodataka = new int[listaOdabranihDodataka.Count];
                            int tempc = 0;

                            ukupnaCena = 0;
                            foreach (Dodatak d in listaOdabranihDodataka)
                            {
                                idEviDodataka[tempc++] = d.Id;
                                ukupnaCena += d.Cena;
                            }
                            Console.WriteLine(idEviDodataka.ToString());
                            foreach (RadioButton button in listOfRadioButtonsPrilozi)
                            {
                                if (button.Checked == true)
                                {
                                    int indexRadioButtona = listOfRadioButtonsPrilozi.IndexOf(button);
                                    tempPrilog = Program.dataClass.listaPriloga.FirstOrDefault(idPriloga => idPriloga.Id == radioButtonsIds[indexRadioButtona]);
                                }
                            }

                            PorucenoJelo novoJelo = new PorucenoJelo(randomNumber, tempJelo.Naziv, tempJelo.Gramaza, tempJelo.Opis, tempJelo.Cena, tempPrilog.Id, tempJelo.IdRestoran, idEviDodataka);
                            Program.dataClass.listaPorucenihJela.Add(novoJelo);


                            ukupnaCena += tempJelo.Cena;
                            ukupnaCena += tempPrilog.Cena;
                            foreach (Dodatak d in listaOdabranihDodataka)
                            {
                                ukupnaCena += d.Cena;
                            }
                            // Proveravam da li ta rezervacija vec postoji tj da li korisnik vise jela porucuje
                            // Ako jeste moram da izmenim tu rezervaciju
                            if (Program.dataClass.listaRezervacija.FirstOrDefault(idRezervacije => idRezervacije.Id == randomNumber2) != null)
                            {
                                // Ovde proveravam da li ta rezervacija vec postoji i ako postoji moram da je izmenim
                                int indexRezervacije = Program.dataClass.listaRezervacija.IndexOf(Program.dataClass.listaRezervacija.FirstOrDefault(idRezervacije => idRezervacije.Id == randomNumber2));
                                Rezervacija staraRezervacija = Program.dataClass.listaRezervacija[indexRezervacije];

                                int[] novaPorucenaJelaIDevi = staraRezervacija.PorucenaJela.Concat(new[] { novoJelo.Id }).ToArray();

                                Program.dataClass.listaRezervacija[indexRezervacije] = new Rezervacija(staraRezervacija.Id, staraRezervacija.IdKorisnika, staraRezervacija.Sifra, ukupnaCena + staraRezervacija.UkupnaCena, novaPorucenaJelaIDevi, staraRezervacija.Date);

                                MessageBox.Show("Uspesno porucen has");
                            }
                            // Ako nije pravim rezervaciju dodajem je u listu i tjt
                            else
                            {
                                MessageBox.Show("Uspesno porucen hass");
                                int[] a = { novoJelo.Id };
                                Program.dataClass.listaRezervacija.Add(new Rezervacija(randomNumber2, Program.dataClass.aktivanKorisnik.Id, randomNumber3.ToString(), ukupnaCena, a, DateTime.Now));
                            }
                            resetujSve();

                            Program.dataClass.saveFiles();

                        }
                        else
                        {
                            MessageBox.Show("Morate da odaberete prilog");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mozete maksimalno 3 priloga odabrati");
                    }
                }
                else
                {
                    MessageBox.Show("Niste odabrali jelo");
                }
            }
            else
            {
                MessageBox.Show("Niste odabrali restoran");
            }
        }

        // Need to purge duplicate restaurants ids
        private void addRestaurantsToComboBox(int[] ideviRestorana)
        {
            // Vracam kombobox na nepickovano ako se promeni filter
            comboBox.SelectedIndex = -1;
            comboBox.Items.Clear();
            // Listam sve ako je null
            if (ideviRestorana == null)
            {
                bool prviRestoran = true;
                foreach (Restoran restoran in Program.dataClass.listaRestorana)
                {
                    if (prviRestoran)
                    {
                        prviRestoran = false;
                        continue;
                    }
                    comboBox.Items.Add(restoran.Naziv);
                }
            }
            else
            {
                int[] ideviRestoranaBezPonavljanja = ideviRestorana.Distinct().ToArray();
                foreach (int id in ideviRestoranaBezPonavljanja)
                {
                    string nazivRestorana = Program.dataClass.listaRestorana.FirstOrDefault(r => r.Id == id).Naziv;
                    comboBox.Items.Add(nazivRestorana);
                }
            }
        }

        private void addFoodsToCheckedListBox()
        {
            // Krece od keca da se izbaci prazno jelo
            for (int i = 1; i < Program.dataClass.listaJela.Count; i++)
            {
                checkedListBox.Items.Add(Program.dataClass.listaJela[i].Naziv);
            }
        }

        // Govori sta je sve cekirano i onda salje restorane za prikaz comboboxu
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //Do the after check tasks here because CHECKCHANGE GETS VALUES BEFORE IT CHANGES NOT AFTER AAAAAAAAAAAAAAA
            // Sustinski ovo radim da bi se sve iz ove funkcije pozvalo nakon sto se on zapravo cekira super je ovaj C#
            this.BeginInvoke(new Action(() =>
            {
                int kolikoCekiranih = checkedListBox.CheckedItems.Count;

                if (kolikoCekiranih == 0)
                {
                    addRestaurantsToComboBox(null);
                }
                else
                {
                    int[] ideviRestorana = new int[kolikoCekiranih];
                    for (int i = 0; i < kolikoCekiranih; i++)
                    {
                        ideviRestorana[i] = Program.dataClass.listaJela.FirstOrDefault(j => j.Naziv == checkedListBox.CheckedItems[i].ToString()).IdRestoran;
                    }
                    addRestaurantsToComboBox(ideviRestorana);
                }
            }));

        }

        private void formResize(object sender, EventArgs e, Button saveButton, Button returnButton, int leftAndRightMargin)
        {
            saveButton.Location = new Point(leftAndRightMargin, this.Height - 100);
            returnButton.Location = new Point(this.Width - leftAndRightMargin - returnButton.Width, this.Height - 100);
        }

        private void Klijent_Load(object sender, EventArgs e)
        {
            odrediMinIMaxDateISetujVrednosti();
            trackbarDateFilter.Size = new Size(250, 50);
            trackbarDateFilter.Location = new Point(iznadTodayOrdersa.Location.X, iznadTodayOrdersa.Location.Y - 50);
            this.Controls.Add(trackbarDateFilter);

            labelDatum.Text = "Porucena jela za datum: " + selectedDate.Day.ToString() + "/" + selectedDate.Month.ToString() + "/" + selectedDate.Year.ToString();
            labelDatum.Location = new Point(trackbarDateFilter.Location.X, trackbarDateFilter.Location.Y - 30);
            labelDatum.Size = new Size(150, 30);
            this.Controls.Add(labelDatum);
            trackbarDateFilter.Scroll += trackbarValueChaneged;
            showTodayOrders();
        }
    }
}
