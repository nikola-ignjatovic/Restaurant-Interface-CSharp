using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static ProjekatTVP.adminFormForInheritance;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace ProjekatTVP
{
    public partial class AzurirajPriloge : adminFormForInheritance
    {
        List<TextBox> listOfTextBoxes = new List<TextBox>();

        string[] textBoxesNames = { "idTextBox", "nazivTextBox", "cenaTextBox" };

        string[] labelTexts = { "ID", "Naziv Priloga", "Cena" };

        Type[] types = { typeof(int), typeof(string), typeof(double) };

        public AzurirajPriloge()
        {

            InitializeComponent();
            //updateDelegate updateFunctionDelegate = new updateDelegate(updateData);
            // What list needs to be upated aswell needs to be passed
            someStartingStuffs(types, Program.dataClass.listaPriloga);
            createSaveAndReturnButton();

            // Tehnicki ni ove stvari nisu morale ovde da budu
            listOfTextBoxes = createTextBoxes(textBoxesNames);
            createLabelsNextToTextBoxes(labelTexts);

        }

        private void AzurirajPriloge_Load(object sender, EventArgs e)
        {

        }
    }
}
