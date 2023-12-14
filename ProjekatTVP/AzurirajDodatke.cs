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
    public partial class AzurirajDodatke : adminFormForInheritance
    {
        List<TextBox> listOfTextBoxes = new List<TextBox>();

        string[] textBoxesNames = { "idTextBox", "nazivTextBox", "cenaTextBox", "gramazaTextBox" };

        string[] labelTexts = { "ID", "Naziv Dodatka", "Cena", "Gramaza" };

        Type[] types = { typeof(int), typeof(string), typeof(double), typeof(int) };
        public AzurirajDodatke()
        {
            InitializeComponent();
            //updateDelegate updateFunctionDelegate = new updateDelegate(updateData);
            someStartingStuffs(types, Program.dataClass.listaDodataka);
            createSaveAndReturnButton();
            listOfTextBoxes = createTextBoxes(textBoxesNames);
            createLabelsNextToTextBoxes(labelTexts);
        }

        void updateData(object sender, EventArgs e)
        {
            MessageBox.Show("Uspesno ste sacuvali podatke");
        }

        private void AzurirajDodatke_Load(object sender, EventArgs e)
        {

        }
    }
}
