using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindPairs
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Equals(string.Empty))
            {
                MessageBox.Show("Введите имя");
                return;
            }
            Hide();
            MainForm mainForm = new MainForm(this, textBoxName.Text);
            mainForm.ShowDialog();
        }

        internal void OpenTable()
        {
            RatingForm ratingForm = new RatingForm();
            ratingForm.ShowDialog();
        }

        private void buttonRating_Click(object sender, EventArgs e)
        {
            OpenTable();
        }
    }
}
