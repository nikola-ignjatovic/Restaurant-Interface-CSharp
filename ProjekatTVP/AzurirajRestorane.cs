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
    public partial class AzurirajRestorane : adminFormForInheritance
    {
        List<TextBox> listOfTextBoxes = new List<TextBox>();

        string[] labelTexts = { "ID", "Naziv", "Adresa", "Kontakt Telefon" };
        string[] textBoxesNames = { "idTextBox", "nazivTextBox", "adresaTextBox", "kontaktTelefonTextBox" };

        Type[] types = { typeof(int), typeof(string), typeof(string), typeof(string) };
        public AzurirajRestorane()
        {
            InitializeComponent();
            //updateDelegate updateFunctionDelegate = new updateDelegate(updateData);

            someStartingStuffs(types, Program.dataClass.listaRestorana);
            createSaveAndReturnButton();
            listOfTextBoxes = createTextBoxes(textBoxesNames);
            createLabelsNextToTextBoxes(labelTexts);
        }

        void updateData(object sender, EventArgs e)
        {
            MessageBox.Show("Uspesno ste sacuvali podatke");
        }

        private void AzurirajRestorane_Load(object sender, EventArgs e)
        {

        }
    }
}
