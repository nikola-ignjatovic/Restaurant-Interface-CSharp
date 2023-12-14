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
    public partial class Form1 : Form
    {
        // Create an instance of the new form
        Admin formAdmin = new Admin();
        Klijent formKlijent = new Klijent();
        TextBox textBoxUsername = new TextBox();
        TextBox textBoxPassword = new TextBox();
        public Form1()
        {
            InitializeComponent();
            Button myButton = new Button();
            myButton.Text = "Login";
            myButton.Location = new Point(200, 250);
            myButton.Click += loginButtonClick;

            textBoxUsername.Location = new Point(100, 200);

            textBoxPassword.Location = new Point(300, 200);

            textBoxPassword.UseSystemPasswordChar = true;
            textBoxPassword.PasswordChar = '*';


            Label passwordLabel = new Label();
            passwordLabel.Text = "Password";
            passwordLabel.Location = new Point(textBoxPassword.Location.X, textBoxPassword.Location.Y - 20);

            Label usernameLabel = new Label();
            usernameLabel.Text = "Username";
            usernameLabel.Location = new Point(textBoxUsername.Location.X, textBoxUsername.Location.Y - 20);

            this.Controls.Add(textBoxUsername);
            this.Controls.Add(textBoxPassword);
            this.Controls.Add(passwordLabel);
            this.Controls.Add(usernameLabel);
            // Add the button to your form's Controls collection
            this.Controls.Add(myButton);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void loginButtonClick(object sender, EventArgs e)
        {
            if (textBoxUsername.Text.Trim().Length != 0 && textBoxPassword.Text.Trim().Length != 0)
            {
                Korisnik korisnik = Program.dataClass.listaKorisnika.FirstOrDefault(username => username.KorisnickoIme == textBoxUsername.Text);
                if (korisnik != null)
                {
                    if (korisnik.Lozinka == textBoxPassword.Text)
                    {
                        if (korisnik.VrstaKorisnika == "admin")
                        {
                            Program.dataClass.aktivanKorisnik = new Korisnik(korisnik.Id, korisnik.Ime, korisnik.Prezime, korisnik.KorisnickoIme, korisnik.Lozinka, korisnik.VrstaKorisnika);
                            formAdmin.Show();
                            // Hide the current form (optional)
                            this.Hide();
                        }
                        else if (korisnik.VrstaKorisnika == "client")
                        {
                            Program.dataClass.aktivanKorisnik = new Korisnik(korisnik.Id, korisnik.Ime, korisnik.Prezime, korisnik.KorisnickoIme, korisnik.Lozinka, korisnik.VrstaKorisnika);
                            formKlijent.Show();
                            // Hide the current form (optional)
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Korisnik nije ni admin ni klijent i vi nemate pristup njemu zovite deva");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Pogresna lozinka");
                    }
                }
                else
                {
                    MessageBox.Show("Ne postoji korisnik");
                }
                // Show the new form
            }
            else
            {
                MessageBox.Show("Unesite sva polja");
            }

        }
    }
}
