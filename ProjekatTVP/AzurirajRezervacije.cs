using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjekatTVP
{
    public partial class AzurirajRezervacije : adminFormForInheritance
    {
        List<TextBox> listOfTextBoxes = new List<TextBox>();

        string[] textBoxesNames = { "idTextBox", "idKorisnikaTextBox", "sifraTextBox", "ukupnaCenaTextBox", "porucenaJelaTextBox" };
        string[] labelTexts = { "ID", "ID korisnik", "sifra", "Ukupna Cena", "Porucena Jela" };

        Type[] types = { typeof(int), typeof(int), typeof(string), typeof(double), typeof(int[]) };
        public AzurirajRezervacije()
        {
            // updateDelegate updateFunctionDelegate = new updateDelegate(updateData);
            InitializeComponent();

            someStartingStuffs(types, Program.dataClass.listaRezervacija);
            createSaveAndReturnButton();
            listOfTextBoxes = createTextBoxes(textBoxesNames);
            createLabelsNextToTextBoxes(labelTexts);
        }

        void updateData(object sender, EventArgs e)
        {

            MessageBox.Show("Uspesno ste sacuvali podatke");
        }

        private void AzurirajRezervacije_Load(object sender, EventArgs e)
        {

        }
    }
}
