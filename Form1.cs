using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace gyumolcsok1
{
    public partial class Form1 : Form
    {
        MySqlConnection connection = null;
        MySqlCommand command = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "local";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "gyumolcsok";
            connection = new MySqlConnection(builder.ConnectionString);
            try
            {
                command.CommandText = "SELECT `id`,`név`,`ár`,`db`FROM `gyumolcsok` WHERE 1";
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        gyumolcsok22 uj = new gyumolcsok22(dr.GetInt32("id"), dr.GetString("név"), dr.GetDouble("ár"), dr.GetInt32("db"));
                        gyumolcs.Items.Add(uj);
                    }
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message + Environment.NewLine + "A program leáll!!");
                Environment.Exit(0);
            }
            gyumolcsok_lista_update();

        }

        private static void gyumolcsok_lista_update()
        {
            gyumolcs.Items.Clear();
            command.CommandText = "SELECT `id`, `nev`, `ar`, `db` FROM `gyumolcs` WHERE 1;";
            connection.Open();
            using (MySqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {

                    gyumolcsok uj = new gyumolcsok(dr.GetInt32("id"), dr.GetString("nev"), dr.GetDouble("ar"), dr.GetDouble("db"));
                    gyumolcs.Items.Add(uj);

                }

            }
            connection.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_nev.Text))
            {
                MessageBox.Show("Nevezze meg a gyümölcsöt");
                textBox_nev.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox_ar.Text))
            {
                MessageBox.Show("Nem adott meg árat!!!");

                textBox_ar.Focus();
                return;
            }
            if (string.IsNullOrEmpty(numericUpDown1.Text))
            {
                MessageBox.Show("Nem adott meg db számot!!!");
                numericUpDown1.Focus();
                return;


            }
            command.CommandText = "INSERT INTO `gyumolcsok` (`id`, `nev`, `ar`, `db`) VALUES (NULL, @nev, @db, @ar)";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@nev", textBox_nev.Text);
            command.Parameters.AddWithValue("@db", numericUpDown1.Text);
            command.Parameters.AddWithValue("@ar", textBox_ar.Text);
            private void button_uj_click(object sender, EventArgs e)
            {

                if (nincsenadat())
                {

                    return;

                }


                command.CommandText = "INSERT INTO `gyumolcs` (`id`, `nev`, `ar`, `db`) VALUES (NULL, @nev, @egysegar, @mennyiseg)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@nev", textBox_nev.Text);
                command.Parameters.AddWithValue("@egysegar", textBox_ar.Text);
                command.Parameters.AddWithValue("@db", numericUpDown1.Text);
                connection.Open();
                try
                {

                    if (command.ExecuteNonQuery() == 1)
                    {

                        MessageBox.Show("Sikeresen rögzítve!!!");
                        textBox_id.Text = "";
                        textBox_nev.Text = "";
                        textBox_ar.Text = "";
                        numericUpDown1.Text = "";

                    }
                    else
                    {

                        MessageBox.Show("Sikertelen rögzítés!");

                    }

                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message, "Sikertelen!");

                }
                connection.Close();
                gyumolcsok_lista_update();

            }

            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Sikeresen rögzítve!!!");
                    textBox_id.Text = "";
                    numericUpDown1.Value = numericUpDown1.Minimum;
                    textBox_ar.Text = "";

                }

                else
                {
                    MessageBox.Show("Sikertelen rögzités!!!");
                }
            }
            catch (MySqlException ex)
            {


                MessageBox.Show(ex.Message);
            }
        }
        private bool nincsenadat()
        {
            if (string.IsNullOrEmpty(textBox_nev.Text))
            {

                MessageBox.Show("Adjon meg egy Gyümölcs Nevet!");
                textBox_nev.Focus();
                return true;

            }
            if (string.IsNullOrEmpty(textBox_ar.Text))
            {

                MessageBox.Show("Adjon meg egy Egység Árat!");
                textBox_ar.Focus();
                return true;

            }
            if (string.IsNullOrEmpty(numericUpDown1.Text))
            {

                MessageBox.Show("Adjon meg egy Mennyiséget!");
                numericUpDown1.Focus();
                return true;

            }
            return false;
            private void Torolbutton_Click(object sender, EventArgs e)
            {

                if (gyumolcs.SelectedIndex < 0)
                {

                    return;

                }
                command.CommandText = "DELETE FROM `gyumolcs` WHERE `id` = @id;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", textBox_id.Text);
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Törlés sikeres volt!");
                    connection.Close();
                    textBox_id.Text = "";
                    textBox_nev.Text = "";
                    textBox_ar.Text = "";
                    numericUpDown1.Text = "";
                    gyumolcsok_lista_update();

                }
                else
                {

                    MessageBox.Show("Az adatok törlése sikertelen volt!");

                }
                if (connection.State == ConnectionState.Open)
                {

                    connection.Close();

                }
            }
        }
    }
}
