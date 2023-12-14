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
    public partial class AzurirajKorisnike : adminFormForInheritance
    {
        List<TextBox> listOfTextBoxes = new List<TextBox>();

        string[] textBoxesNames = { "idTextBox", "imeTextBox", "prezimeTextBox", "korisnickoImeTextBox", "lozinkaTextBox", "vrstaKorisnikaTextBox" };
        string[] labelTexts = { "ID", "Ime", "Prezime", "Korisnicko Ime", "Lozika", "Vrsta koristnika (admin/klijent)" };

        Type[] types = { typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) };
        public AzurirajKorisnike()
        {
            InitializeComponent();
            //updateDelegate updateFunctionDelegate = new updateDelegate(updateData);
            someStartingStuffs(types, Program.dataClass.listaKorisnika);
            createSaveAndReturnButton();
            listOfTextBoxes = createTextBoxes(textBoxesNames);
            createLabelsNextToTextBoxes(labelTexts);
        }

        void updateData(object sender, EventArgs e)
        {
            MessageBox.Show("Uspesno ste sacuvali podatke");
        }

        private void AzurirajKorisnike_Load(object sender, EventArgs e)
        {

        }
    }
}
