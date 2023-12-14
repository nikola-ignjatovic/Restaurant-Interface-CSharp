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
    public partial class Admin : Form
    {
        // Names for buttons dynamically generating all
        static string[] buttonTexts = { "Azuriraj restorane", "Azuriraj jela", "Azuriraj Porucena Jela", "Azuriraj priloge", "Azuriraj dodatake", "Azuriraj korisnika", "Azuriraj rezervacije", "Povratak nazad" };
        static Form[] forms = { new AzurirajRestorane(), new AzurirajJela(), new azurirajPorucenaJela(), new AzurirajPriloge(), new AzurirajDodatke(), new AzurirajKorisnike(), new AzurirajRezervacije(), new Form1() };

        // list of buttons used for repositioning them with scalling
        List<Button> buttonList = new List<Button>();
        const int heightOfButton = 30;

        public Admin()
        {
            InitializeComponent();
            // To be even honest this is just something complex that i rng-ed to work please do not change thank you very much
            int startingPosition = this.Height / 2 - (buttonTexts.Length / 2 * (heightOfButton / 3) * 5) - heightOfButton / 2;

            for (int i = 0; i < buttonTexts.Length; i++)
            {
                // for now no need
                // int xOffset = 10 * (i + 1) * 2;
                int yOffset = heightOfButton / 3 * (i + 1) * 3;
                Button myButton = new Button();
                myButton.Text = buttonTexts[i];
                myButton.Width = 500;
                myButton.Height = heightOfButton;
                myButton.Location = new Point(this.Width / 2 - myButton.Width / 2, startingPosition + yOffset);
                myButton.Click += adminButtonsClick;

                buttonList.Add(myButton);

                // Add the button to your form's Controls collection
                this.Controls.Add(myButton);
            }
            this.Resize += new EventHandler(formResize);
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }
        private void adminButtonsClick(object sender, EventArgs e)
        {
            Button myButton = sender as Button;
            for (int i = 0; i < buttonTexts.Length; i++)
            {
                if (myButton.Text == buttonTexts[i])
                {
                    forms[i].Show();
                    this.Hide();
                    break;
                }
            }
        }

        private void formResize(object sender, EventArgs e)
        {
            int startingPosition = this.Height / 2 - (buttonTexts.Length / 2 * (heightOfButton / 3) * 5) - heightOfButton / 2;
            for (int i = 0; i < buttonTexts.Length; i++)
            {
                // for now no need
                // int xOffset = 10 * (i + 1) * 2;
                int yOffset = 10 * (i + 1) * 3;
                buttonList[i].Location = new Point(this.Width / 2 - buttonList[i].Width / 2, startingPosition + yOffset);

            }
        }
    }
}
