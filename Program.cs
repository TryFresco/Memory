using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory2
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "r", "r", "N", "N", ",", ",", "R", "R",
            "W", "W", "q", "q", "C", "C", ";", ";"
        };

        Label firstClicked, secondClicked;

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
            Properties.Settings.Default.Runde = true;
            label19.Text = "Spieler 01 ist dran!";
        }

        private void label_click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
            {
                return;
            }

            Label clickedLabel = sender as Label;

            if (clickedLabel == null )
            {
                return;
            }

            if (clickedLabel.ForeColor == Color.Black)
            {
                return;
            }

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            Winner();

            if (firstClicked.Text == secondClicked.Text)
            {
                if (Properties.Settings.Default.Runde == true)
                {
                    Properties.Settings.Default.punktestand01++;
                }
                else
                {
                    Properties.Settings.Default.punktestand02++;
                }
                punkte01.Text = Convert.ToString(Properties.Settings.Default.punktestand01);
                punkte02.Text = Convert.ToString(Properties.Settings.Default.punktestand02);
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                timer1.Start();

                if (Properties.Settings.Default.Runde == true)
                {
                    Properties.Settings.Default.Runde = false;
                    label19.Text = "Spieler 02 ist dran";
                }
                else
                {
                    Properties.Settings.Default.Runde = true;
                    label19.Text = "Spieler 01 ist dran";
                }

            }
            

        }

        private void Winner()
        {
            Label label;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                label = tableLayoutPanel1.Controls[i] as Label;

                if (label != null && label.ForeColor == label.BackColor)
                {
                    return;
                }
            }

            if (Properties.Settings.Default.punktestand01 < Properties.Settings.Default.punktestand02)
            {
                MessageBox.Show("Spieler 02 hat gewonnen!");
            }
            if (Properties.Settings.Default.punktestand01 > Properties.Settings.Default.punktestand02)
            {
                MessageBox.Show("Spieler 01 hat gewonnen!");
            }
            return;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            punkte01.Text = "0";
            punkte02.Text = "0";
            label19.Text = "Spieler 01 ist dran!";
            Properties.Settings.Default.punktestand01 = 0;
            Properties.Settings.Default.punktestand02 = 0;
            Properties.Settings.Default.Runde = true;
            tableLayoutPanel1.Controls.Clear();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            Label label;
            int randNumber;

            for(int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i];
                }
                else
                {
                    continue;
                }
                randNumber = random.Next(0, icons.Count);
                label.Text = icons[randNumber];
                icons.RemoveAt(randNumber);
            }
        }
    }
}
