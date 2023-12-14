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
    public partial class AzurirajJela : adminFormForInheritance
    {
        List<TextBox> listOfTextBoxes = new List<TextBox>();

        string[] textBoxesNames = { "idTextBox", "nazivTextBox", "gramazaTextBox", "opisTextBox", "cenaTextBox", "idPrilogTextBox", "idRestoranTextBox", "dodaciTextBox" };
        string[] labelTexts = { "ID", "Naziv", "Gramaza", "Opis", "Cena", "ID Prilog", "ID Restoran", "Dodaci" };

        CheckBox obavezanPrilogCheckBox = new CheckBox();

        Type[] types = { typeof(int), typeof(string), typeof(int), typeof(string), typeof(double), typeof(int), typeof(int), typeof(int[]) };
        public AzurirajJela()
        {
            InitializeComponent();
            updateDelegate updateFunctionDelegate = new updateDelegate(updateData);

            someStartingStuffs(types, Program.dataClass.listaJela);
            //createSaveAndReturnButton(updateFunctionDelegate);
            listOfTextBoxes = createTextBoxes(textBoxesNames);
            createSaveAndReturnButton();
            createLabelsNextToTextBoxes(labelTexts);

            Label checkBoxLabel = new Label();
            checkBoxLabel.Location = new Point(600, 300);
            checkBoxLabel.Text = "Obavezan prilog";

            this.Controls.Add(checkBoxLabel);


            obavezanPrilogCheckBox.Checked = false;
            obavezanPrilogCheckBox.Name = "checkBoxObavezanPrilog";
            obavezanPrilogCheckBox.Location = new Point(checkBoxLabel.Location.X + 100, checkBoxLabel.Location.Y);
            obavezanPrilogCheckBox.Size = new Size(30, 20);
            this.Controls.Add(obavezanPrilogCheckBox);
        }

        void updateData(object sender, EventArgs e)
        {
            MessageBox.Show("Uspesno ste sacuvali podatke");
        }

        private void AzurirajJela_Load(object sender, EventArgs e)
        {

        }
    }
}
