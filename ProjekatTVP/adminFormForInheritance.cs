using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjekatTVP
{
    // Okay so this class is being used in order to save time and prevent errors when creating multiple forms.
    // It is used for derived classes in admin section
    // Because C# does not support goddamn multiple inheritance they will also inherit interface with what i wanted to be abstract
    public class adminFormForInheritance : Form
    {
        protected List<TextBox> listOfTextBoxes;
        protected Type[] types;

        dynamic listToBeUpdated;

        // Creating a function delegate for button click
        public delegate void updateDelegate(object sender, EventArgs e);

        // Creating a static form to prevent other forms craeting more references to the class
        static Admin adminForm = new Admin();

        const string startingLabelText = "Ako zelite da dodate unesite ID i sve ostale podatke.\n" +
            "Ukoliko zelite da obrisete unesite samo ID\n" +
            "Ukoliko zelite da izmenite unesite ID i nove podatke.\n" +
            "Dodatke za jela i porucena Jela za korisnika unosite tako sto kucate ID-eve sa razmakom izmedju ID-eva";

        const int textBoxWidth = 300;
        const int labelOffset = 100;

        // Create a new instance of the listbox
        ListBox listBox1 = new ListBox();

        //Sadly not in use due to lack of support for multiple inheritance
        //protected abstract int id { get; set; }
        //protected abstract string naziv { get; set; }
        //protected abstract string nazivDatoteke { get; set; }

        //It is void beacuse list is reference type.Using dynamic so i can have multiple list returns and dynamic in arguments aswell to pass dynamic list types i really do hope it works
        //Gotta also pass filePath beacuse it will be used multiple times for multiple files within one class

        //No longer used because of the better way found

        //protected void readFile(dynamic lista, string filePath)
        //{
        //    FileStream fs = new FileStream(filePath, FileMode.Open);
        //    // Checking if fileStream is empty to avoid crash
        //    if (fs.Length > 0)
        //    {
        //        BinaryFormatter br = new BinaryFormatter();

        //        Type listaType = lista.GetType().GetGenericArguments()[0];
        //        // Here it deserializes and converts it to the type 
        //        // Deserialize the binary data and cast it to the correct type
        //        // I honestly dont know what does this do
        //        var deserializedData = br.Deserialize(fs);
        //        lista = ((IEnumerable)deserializedData)
        //            .Cast<object>()
        //            .Select(x => Convert.ChangeType(x, listaType))
        //            .ToList();

        //        fs.Dispose();
        //    }
        //}

        protected void someStartingStuffs(Type[] types, dynamic listToBeUpdated)
        {
            Label label = new Label();
            label.Size = new Size(300, 70);
            label.Location = new Point(this.Width / 2 - label.Width / 2, 20);
            label.Text = startingLabelText;
            label.TextAlign = ContentAlignment.MiddleCenter;

            this.Controls.Add(label);

            this.types = types;
            this.listToBeUpdated = listToBeUpdated;

            // Set the location and size of the listbox
            listBox1.Location = new System.Drawing.Point(550, 150);
            listBox1.Size = new System.Drawing.Size(200, 150);
            listBox1.HorizontalScrollbar = true;
            this.Controls.Add(listBox1);
            updateListBoxText();
        }

        protected void updateListBoxText()
        {
            listBox1.Items.Clear();
            // za ovo isto pogledati ono za kastovanje 
            for (int i = 0; i < ((IEnumerable)listToBeUpdated).Cast<dynamic>().Count(); i++)
            {
                // Splitujem string da bih mogao u listbox 1 po 1 da dodajem lepo a splitujem ga po novom redu
                string s = listToBeUpdated[i].ToString();
                string[] tempStringArray = s.Split('\n');
                listBox1.Items.Add(i.ToString() + ".");
                foreach (string tempString in tempStringArray)
                {
                    listBox1.Items.Add(tempString);
                }
                listBox1.Items.Add("\n");
                listBox1.Items.Add("\n");
            }
        }

        // dynamically creating textboxes for derived classes
        protected List<TextBox> createTextBoxes(string[] textBoxesNames)
        {
            int startingPosition = this.Height / 2 - (textBoxesNames.Length / 2 * (30 / 3) * 4) - 30 / 2;

            List<TextBox> listOfTextBoxes = new List<TextBox>();
            for (int i = 0; i < textBoxesNames.Length; i++)
            {
                int yOffset = 30 / 3 * (i + 1) * 3;
                TextBox textBox = new TextBox();
                textBox.Location = new Point(30, startingPosition + yOffset);
                textBox.Size = new Size(textBoxWidth, 20);
                textBox.Name = textBoxesNames[i];

                this.Controls.Add(textBox);
                listOfTextBoxes.Add(textBox);
            }
            this.listOfTextBoxes = listOfTextBoxes;
            return listOfTextBoxes;

        }

        protected void createLabelsNextToTextBoxes(string[] labelTexts)
        {
            int startingPosition = this.Height / 2 - (labelTexts.Length / 2 * (30 / 3) * 4) - 30 / 2;

            for (int i = 0; i < labelTexts.Length; i++)
            {
                int yOffset = 30 / 3 * (i + 1) * 3;
                Label label = new Label();
                label.Location = new Point(textBoxWidth + labelOffset, startingPosition + yOffset);
                label.Text = labelTexts[i];

                this.Controls.Add(label);
            }
        }

        // Creating buttons for those derived forms and positioning them down
        protected void createSaveAndReturnButton()
        {
            int leftAndRightMargin = 30;
            int buttonWidth = 300;
            Button saveButton = new Button();
            saveButton.Text = "Sacuvaj izmene";
            saveButton.Width = buttonWidth;
            saveButton.Location = new Point(leftAndRightMargin, this.Height - 100);
            saveButton.Click += new EventHandler((sender, e) => verifyData(sender, e, listOfTextBoxes, types));
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

        // function that returns us to the admin page
        private void returnBackToAdmin(object sender, EventArgs e)
        {
            adminForm.Show();
            this.Hide();
        }

        // Fuction that reposition elements
        private void formResize(object sender, EventArgs e, Button saveButton, Button returnButton, int leftAndRightMargin)
        {
            saveButton.Location = new Point(leftAndRightMargin, this.Height - 100);
            returnButton.Location = new Point(this.Width - leftAndRightMargin - returnButton.Width, this.Height - 100);
        }


        // Currently void
        // Returns 0 if fails
        // returns 1 if data is being imported to file
        // returns 2 if data is being removed and
        void verifyData(object sender, EventArgs e, List<TextBox> listOfTextBoxes, Type[] types)
        {
            int number;
            if (int.TryParse(listOfTextBoxes[0].Text, out number))
            {
                if (number == 0)
                {
                    MessageBox.Show("Ne smes menjati element sa id-em 0");
                    return;
                }
            }
            for (int i = 0; i < listOfTextBoxes.Count(); i++)
            {
                if (listOfTextBoxes[i].Text.Trim().Length != 0)
                {
                    if (types[i] == typeof(int))
                    {
                        if (int.TryParse(listOfTextBoxes[i].Text, out int value))
                        {
                            // The text could be parsed as an integer, so the value variable contains the parsed value
                        }
                        else
                        {
                            MessageBox.Show("U Pogresnom formatu ste uneli neko od polja");
                            doSomethingWithInput(0);
                            return;
                        }
                    }
                    if (types[i] == typeof(double))
                    {
                        if (double.TryParse(listOfTextBoxes[i].Text, out double value))
                        {
                            // The text could be parsed as an integer, so the value variable contains the parsed value
                        }
                        else
                        {
                            MessageBox.Show("U Pogresnom formatu ste uneli neko od polja");
                            doSomethingWithInput(0);
                            return;
                        }
                    }
                }
                else
                {
                    // Checking to see if only id is written for deletion
                    if (i > 0)
                    {
                        for (int j = 1; j < listOfTextBoxes.Count(); j++)
                        {
                            if (listOfTextBoxes[j].Text.Trim().Length != 0)
                            {
                                MessageBox.Show("Niste uneli sva polja");
                                doSomethingWithInput(0);
                                return;
                            }
                        }
                        // Here need to check if id is 1 because if it is one it cannot be removed
                        if (int.Parse(listOfTextBoxes[0].Text) > 1)
                        {
                            doSomethingWithInput(2);
                        }
                        else
                        {
                            MessageBox.Show("Id mora biti veci od 1");
                        }
                        return;
                    }
                    MessageBox.Show("Niste uneli sva polja");
                    doSomethingWithInput(0);
                    return;
                }

            }
            doSomethingWithInput(1);
            return;
        }

        protected void doSomethingWithInput(int resultOfVerification)
        {
            if (resultOfVerification == 0)
            {

            }
            // Ovde isto treba proveriti da li postoji vec ID ili ne pa tek onda odraditi ovo 
            else if (resultOfVerification == 1)
            {
                int id = int.Parse(listOfTextBoxes[0].Text);

                // Kastujem lambda ekspresion u delegat tipa Func koji prima dynamic parametar i vraca bool
                // Aha .cast<> se poziva na svakom elementu u listi i kastuje ga u pa meni sta treba jer je dynamic
                dynamic objectWithId = ((IEnumerable)listToBeUpdated).Cast<dynamic>().FirstOrDefault((Func<dynamic, bool>)(o => o.Id == id));
                int indexOfObjectWithId = listToBeUpdated.IndexOf(objectWithId);

                // Ovde ce da se uradi replace podatka ako ovo nije null
                // Ovo sve je sigurno moglo bolje
                if (objectWithId != null)
                {
                    if (listToBeUpdated.GetType() == Program.dataClass.listaPriloga.GetType())
                    {
                        listToBeUpdated[indexOfObjectWithId] = new Prilog(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, double.Parse(listOfTextBoxes[2].Text));
                        // listToBeUpdated.Add(new()); cant do it like this no support in c# 7.3
                        MessageBox.Show("Uspesno ste izmenili podatak");
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaDodataka.GetType())
                    {
                        listToBeUpdated[indexOfObjectWithId] = new Dodatak(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, double.Parse(listOfTextBoxes[2].Text), int.Parse(listOfTextBoxes[3].Text));
                        MessageBox.Show("Uspesno ste izmenili podatak");
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaKorisnika.GetType())
                    {
                        if (listOfTextBoxes[4].Text.TrimEnd().Length <= 5)
                        {
                            MessageBox.Show("Lozinka i Kornisnicko ime moraju da imaju bar 6 karaktera");
                        }
                        else
                        {
                            listToBeUpdated[indexOfObjectWithId] = new Korisnik(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, listOfTextBoxes[2].Text, listOfTextBoxes[3].Text, listOfTextBoxes[4].Text, listOfTextBoxes[5].Text);
                            MessageBox.Show("Uspesno ste izmenili podatak");
                        }
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaRestorana.GetType())
                    {
                        if (listOfTextBoxes[3].Text.TrimEnd().Length <= 9)
                        {
                            MessageBox.Show("Broj telefona mora da ima bar 10 karaktera");
                        }
                        else
                        {
                            MessageBox.Show("Uspesno ste izmenili podatak");
                            listToBeUpdated[indexOfObjectWithId] = new Restoran(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, listOfTextBoxes[2].Text, listOfTextBoxes[3].Text);
                        }
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaPorucenihJela.GetType())
                    {
                        string[] splitovanArray = listOfTextBoxes[7].Text.TrimEnd().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] splitovanArrayInt = new int[splitovanArray.Length];
                        for (int i = 0; i < splitovanArray.Length; i++)
                        {
                            splitovanArrayInt[i] = int.Parse(splitovanArray[i]);
                        }

                        // Provera da li prilog postoji u bazi isto ce ispod biti i za dodatka i za Restoran
                        if (Program.dataClass.listaPriloga.FirstOrDefault(idPriloga => idPriloga.Id == int.Parse(listOfTextBoxes[5].Text)) != null)
                        {
                            if (Program.dataClass.listaRestorana.FirstOrDefault(idRestorana => idRestorana.Id == int.Parse(listOfTextBoxes[6].Text)) != null)
                            {
                                foreach (int idDodatka in splitovanArrayInt)
                                {
                                    if (Program.dataClass.listaDodataka.FirstOrDefault(idDodataka => idDodataka.Id == idDodatka) != null)
                                    {

                                    }
                                    else
                                    {
                                        MessageBox.Show("Zadati idevi dodataka ne postoje unesite ispravne id-eve ");
                                        return;
                                    }
                                }
                                listToBeUpdated[indexOfObjectWithId] = new PorucenoJelo(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, int.Parse(listOfTextBoxes[2].Text), listOfTextBoxes[3].Text, double.Parse(listOfTextBoxes[4].Text), int.Parse(listOfTextBoxes[5].Text), int.Parse(listOfTextBoxes[6].Text), splitovanArrayInt);
                                MessageBox.Show("Uspesno ste izmenili podatak");
                            }
                            else
                            {
                                MessageBox.Show("ID restorana ne postoji unesi id restorana koji postoji");
                            }
                        }
                        else
                        {
                            MessageBox.Show("ID Priloga ne postoji unesi id priloga koji postoji");
                        }

                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaRezervacija.GetType())
                    {
                        // Gotta check if user put multiple spaces i verovatno cu morati da proverim dal taj prilog il sta je vec jelo tipa postoji
                        string[] splitovanArray = listOfTextBoxes[4].Text.TrimEnd().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] splitovanArrayInt = new int[splitovanArray.Length];
                        for (int i = 0; i < splitovanArray.Length; i++)
                        {
                            splitovanArrayInt[i] = int.Parse(splitovanArray[i]);
                        }

                        foreach (int idJela in splitovanArrayInt)
                        {
                            if (Program.dataClass.listaPorucenihJela.FirstOrDefault(idJelaa => idJelaa.Id == idJela) != null)
                            {

                            }
                            else
                            {
                                MessageBox.Show("Ne postoji zadati id porucenog jela unesite ispravno pls");
                                return;
                            }
                        }

                        listToBeUpdated[indexOfObjectWithId] = new Rezervacija(int.Parse(listOfTextBoxes[0].Text), int.Parse(listOfTextBoxes[1].Text), listOfTextBoxes[2].Text, double.Parse(listOfTextBoxes[3].Text), splitovanArrayInt, listToBeUpdated[indexOfObjectWithId].Date);
                        MessageBox.Show("Uspesno ste uneli podatak");
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaJela.GetType())
                    {
                        string[] splitovanArray = listOfTextBoxes[7].Text.TrimEnd().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] splitovanArrayInt = new int[splitovanArray.Length];
                        for (int i = 0; i < splitovanArray.Length; i++)
                        {
                            splitovanArrayInt[i] = int.Parse(splitovanArray[i]);
                        }

                        CheckBox tempCheckBox = null;
                        bool isCheckBoxChecked = false;
                        foreach (Control control in this.Controls)
                        {
                            if (control is CheckBox && control.Name == "checkBoxObavezanPrilog")
                            {
                                tempCheckBox = (CheckBox)control;
                                break;
                            }
                        }

                        // Use the myButton object
                        if (tempCheckBox != null)
                        {
                            isCheckBoxChecked = tempCheckBox.Checked;
                        }


                        // Provera da li prilog postoji u bazi isto ce ispod biti i za dodatka i za Restoran
                        if (Program.dataClass.listaPriloga.FirstOrDefault(idPriloga => idPriloga.Id == int.Parse(listOfTextBoxes[5].Text)) != null)
                        {
                            if (Program.dataClass.listaRestorana.FirstOrDefault(idRestorana => idRestorana.Id == int.Parse(listOfTextBoxes[6].Text)) != null)
                            {
                                foreach (int idDodatka in splitovanArrayInt)
                                {
                                    if (Program.dataClass.listaDodataka.FirstOrDefault(idDodataka => idDodataka.Id == idDodatka) != null)
                                    {

                                    }
                                    else
                                    {
                                        MessageBox.Show("Zadati idevi dodataka ne postoje unesite ispravne id-eve ");
                                        return;
                                    }
                                }
                                listToBeUpdated[indexOfObjectWithId] = new Jelo(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, int.Parse(listOfTextBoxes[2].Text), listOfTextBoxes[3].Text, double.Parse(listOfTextBoxes[4].Text), int.Parse(listOfTextBoxes[5].Text), int.Parse(listOfTextBoxes[6].Text), splitovanArrayInt, isCheckBoxChecked);
                                MessageBox.Show("Uspesno ste izmenili podatak");
                            }
                            else
                            {
                                MessageBox.Show("ID restorana ne postoji unesi id restorana koji postoji");
                            }
                        }
                        else
                        {
                            MessageBox.Show("ID Priloga ne postoji unesi id priloga koji postoji");
                        }


                        listToBeUpdated[indexOfObjectWithId] = new Jelo(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, int.Parse(listOfTextBoxes[2].Text), listOfTextBoxes[3].Text, double.Parse(listOfTextBoxes[4].Text), int.Parse(listOfTextBoxes[5].Text), int.Parse(listOfTextBoxes[6].Text), splitovanArrayInt, isCheckBoxChecked);
                    }
                }
                else
                {
                    // Za svaki tip liste mora drugaciji konstruktor sa drugacijim parametrima pa ovako radim
                    if (listToBeUpdated.GetType() == Program.dataClass.listaPriloga.GetType())
                    {
                        listToBeUpdated.Add(new Prilog(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, double.Parse(listOfTextBoxes[2].Text)));
                        MessageBox.Show("Uspesno ste izmenili podatak"); // listToBeUpdated.Add(new()); cant do it like this no support in c# 7.3
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaDodataka.GetType())
                    {
                        listToBeUpdated.Add(new Dodatak(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, double.Parse(listOfTextBoxes[2].Text), int.Parse(listOfTextBoxes[3].Text)));
                        MessageBox.Show("Uspesno ste izmenili podatak");
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaKorisnika.GetType())
                    {
                        if (listOfTextBoxes[4].Text.TrimEnd().Length <= 5)
                        {
                            MessageBox.Show("Lozinka i Kornisnicko ime moraju da imaju bar 6 karaktera");
                        }
                        else
                        {
                            listToBeUpdated.Add(new Korisnik(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, listOfTextBoxes[2].Text, listOfTextBoxes[3].Text, listOfTextBoxes[4].Text, listOfTextBoxes[5].Text));
                            MessageBox.Show("Uspesno ste izmenili podatak");
                        }
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaRestorana.GetType())
                    {
                        if (listOfTextBoxes[3].Text.TrimEnd().Length <= 9)
                        {
                            MessageBox.Show("Broj telefona mora da ima bar 10 karaktera");
                        }
                        else
                        {
                            listToBeUpdated.Add(new Restoran(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, listOfTextBoxes[2].Text, listOfTextBoxes[3].Text));
                            MessageBox.Show("Uspesno ste izmenili podatak");
                        }
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaPorucenihJela.GetType())
                    {
                        string[] splitovanArray = listOfTextBoxes[7].Text.TrimEnd().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] splitovanArrayInt = new int[splitovanArray.Length];
                        for (int i = 0; i < splitovanArray.Length; i++)
                        {
                            splitovanArrayInt[i] = int.Parse(splitovanArray[i]);
                        }
                        // Provera da li prilog postoji u bazi isto ce ispod biti i za dodatka i za Restoran
                        if (Program.dataClass.listaPriloga.FirstOrDefault(idPriloga => idPriloga.Id == int.Parse(listOfTextBoxes[5].Text)) != null)
                        {
                            if (Program.dataClass.listaRestorana.FirstOrDefault(idRestorana => idRestorana.Id == int.Parse(listOfTextBoxes[6].Text)) != null)
                            {
                                foreach (int idDodatka in splitovanArrayInt)
                                {
                                    if (Program.dataClass.listaDodataka.FirstOrDefault(idDodataka => idDodataka.Id == idDodatka) != null)
                                    {

                                    }
                                    else
                                    {
                                        MessageBox.Show("Zadati idevi dodataka ne postoje unesite ispravne id-eve ");
                                        return;
                                    }
                                }
                                listToBeUpdated.Add(new PorucenoJelo(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, int.Parse(listOfTextBoxes[2].Text), listOfTextBoxes[3].Text, double.Parse(listOfTextBoxes[4].Text), int.Parse(listOfTextBoxes[5].Text), int.Parse(listOfTextBoxes[6].Text), splitovanArrayInt));
                                MessageBox.Show("Uspesno ste izmenili podatak");
                            }
                            else
                            {
                                MessageBox.Show("ID restorana ne postoji unesi id restorana koji postoji");
                            }
                        }
                        else
                        {
                            MessageBox.Show("ID Priloga ne postoji unesi id priloga koji postoji");
                        }

                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaRezervacija.GetType())
                    {
                        string[] splitovanArray = listOfTextBoxes[4].Text.TrimEnd().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] splitovanArrayInt = new int[splitovanArray.Length];
                        for (int i = 0; i < splitovanArray.Length; i++)
                        {
                            splitovanArrayInt[i] = int.Parse(splitovanArray[i]);
                        }

                        foreach (int idJela in splitovanArrayInt)
                        {
                            if (Program.dataClass.listaPorucenihJela.FirstOrDefault(idJelaa => idJelaa.Id == idJela) != null)
                            {

                            }
                            else
                            {
                                MessageBox.Show("Ne postoji zadati id porucenog jela unesite ispravno pls");
                                return;
                            }
                        }

                        listToBeUpdated.Add(new Rezervacija(int.Parse(listOfTextBoxes[0].Text), int.Parse(listOfTextBoxes[1].Text), listOfTextBoxes[2].Text, double.Parse(listOfTextBoxes[3].Text), splitovanArrayInt, DateTime.Now));
                        MessageBox.Show("Uspesno ste uneli podatak");
                    }
                    else if (listToBeUpdated.GetType() == Program.dataClass.listaJela.GetType())
                    {
                        string[] splitovanArray = listOfTextBoxes[7].Text.TrimEnd().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] splitovanArrayInt = new int[splitovanArray.Length];
                        for (int i = 0; i < splitovanArray.Length; i++)
                        {
                            splitovanArrayInt[i] = int.Parse(splitovanArray[i]);
                        }

                        CheckBox tempCheckBox = null;
                        bool isCheckBoxChecked = false;
                        foreach (Control control in this.Controls)
                        {
                            if (control is CheckBox && control.Name == "checkBoxObavezanPrilog")
                            {
                                tempCheckBox = (CheckBox)control;
                                break;
                            }
                        }

                        // Use the myButton object
                        if (tempCheckBox != null)
                        {
                            isCheckBoxChecked = tempCheckBox.Checked;
                        }

                        // Provera da li prilog postoji u bazi isto ce ispod biti i za dodatka i za Restoran
                        if (Program.dataClass.listaPriloga.FirstOrDefault(idPriloga => idPriloga.Id == int.Parse(listOfTextBoxes[5].Text)) != null)
                        {
                            if (Program.dataClass.listaDodataka.FirstOrDefault(idDodataka => idDodataka.Id == int.Parse(listOfTextBoxes[6].Text)) != null)
                            {
                                foreach (int idDodatka in splitovanArrayInt)
                                {
                                    if (Program.dataClass.listaRestorana.FirstOrDefault(idRestorana => idRestorana.Id == idDodatka) != null)
                                    {

                                    }
                                    else
                                    {
                                        MessageBox.Show("Zadati idevi dodataka ne postoje unesite ispravne id-eve ");
                                        return;
                                    }
                                }
                                listToBeUpdated.Add(new Jelo(int.Parse(listOfTextBoxes[0].Text), listOfTextBoxes[1].Text, int.Parse(listOfTextBoxes[2].Text), listOfTextBoxes[3].Text, double.Parse(listOfTextBoxes[4].Text), int.Parse(listOfTextBoxes[5].Text), int.Parse(listOfTextBoxes[6].Text), splitovanArrayInt, isCheckBoxChecked));
                                MessageBox.Show("Uspesno ste izmenili podatak");
                            }
                            else
                            {
                                MessageBox.Show("ID restorana ne postoji unesi id restorana koji postoji");
                            }
                        }
                        else
                        {
                            MessageBox.Show("ID Priloga ne postoji unesi id priloga koji postoji");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nekako je doslo do errora nzm ni ja kako");
                    }

                }
            }
            // Treba ispisati ako id postoji ili ako je obrisan 
            // Ukratko proverava da li postoji object sa idem i ako ne postoji ispise da ne postoji a ako postoji obrise ga
            else if (resultOfVerification == 2)
            {
                int id = int.Parse(listOfTextBoxes[0].Text);
                // Kastujem lambda ekspresion u delegat tipa Func koji prima dynamic parametar i vraca bool
                // Aha .cast<> se poziva na svakom elementu u listi i kastuje ga u pa meni sta treba jer je dynamic
                dynamic objectWithId = ((IEnumerable)listToBeUpdated).Cast<dynamic>().FirstOrDefault((Func<dynamic, bool>)(o => o.Id == id));
                if (objectWithId != null)
                {
                    listToBeUpdated.Remove(objectWithId);
                    MessageBox.Show("Uspesno obrisan podatak");
                }
                else
                {
                    MessageBox.Show("Zadati ID ne postoji u fajlu sa podacima");
                }
            }
            // Calling auto save at the end for now commented 
            Program.dataClass.saveFiles();
            updateListBoxText();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // adminFormForInheritance
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "adminFormForInheritance";
            this.Load += new System.EventHandler(this.adminFormForInheritance_Load);
            this.ResumeLayout(false);

        }

        private void adminFormForInheritance_Load(object sender, EventArgs e)
        {

        }
    }

}
