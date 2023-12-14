using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProjekatTVP.adminFormForInheritance;

namespace ProjekatTVP
{
    public partial class azurirajPorucenaJela : adminFormForInheritance
    {
        List<TextBox> listOfTextBoxes = new List<TextBox>();

        string[] textBoxesNames = { "idTextBox", "nazivTextBox", "gramazaTextBox", "opisTextBox", "cenaTextBox", "idPrilogTextBox", "idRestoranTextBox", "dodaciTextBox" };
        string[] labelTexts = { "ID", "Naziv", "Gramaza", "Opis", "Cena", "ID Prilog", "ID Restoran", "Dodaci" };

        CheckBox obavezanPrilogCheckBox = new CheckBox();

        Type[] types = { typeof(int), typeof(string), typeof(int), typeof(string), typeof(double), typeof(int), typeof(int), typeof(int[]) };
        public azurirajPorucenaJela()
        {
            InitializeComponent();
            // updateDelegate updateFunctionDelegate = new updateDelegate(updateData);

            someStartingStuffs(types, Program.dataClass.listaPorucenihJela);
            //createSaveAndReturnButton(updateFunctionDelegate);
            listOfTextBoxes = createTextBoxes(textBoxesNames);
            createSaveAndReturnButton();
            createLabelsNextToTextBoxes(labelTexts);
        }
    }
}
